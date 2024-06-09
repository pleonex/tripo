using System.Globalization;
using PleOps.Tripo.Traveller;
using Location = PleOps.Tripo.Traveller.Location;

namespace PleOps.Tripo.Integrations;

public class WorldeeMapper
{
    private readonly object instanceLock = new object();

    private WorldeeTravel worldeeTravel = null!; // only entrypoint is Map that set it the first thing.
    private DateTime startDate;
    private int directionIdx;

    public Travel Map(WorldeeTravel worldeeTravel)
    {
        Travel travel;
        lock (instanceLock)
        {
            this.worldeeTravel = worldeeTravel;
            travel = MapTravel();
        }

        return travel;
    }

    private Travel MapTravel()
    {
        var travel = new Travel
        {
            Name = worldeeTravel.name,
            Description = worldeeTravel.description,
            Conclusion = worldeeTravel.descriptionEnd,
        };

        startDate = DateTime.ParseExact(worldeeTravel.dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        directionIdx = 0;

        for (int i = 0; i < worldeeTravel.days.Length; i++)
        {
            var worldeeDay = worldeeTravel.days[i];
            var day = MapDay(worldeeDay, i);
            travel.Days.Add(day);
        }

        return travel;
    }

    private TravelDay MapDay(Day worldeeDay, int relativeDay)
    {
        var day = new TravelDay
        {
            Title = worldeeDay.title,
            RelativeDay = relativeDay,
            Description = worldeeDay.description,
            Date = startDate + TimeSpan.FromDays(relativeDay),
        };

        for (int j = 0; j < worldeeDay.stops.Length; j++)
        {
            if (relativeDay != 1 || j > 0)
            {
                var worldeeDirection = worldeeTravel.directions[directionIdx++];
                var travelActivity = MapDirection(worldeeDirection);
                day.Activities.Add(travelActivity);
            }

            var worldeeStop = worldeeDay.stops[j];
            var activity = MapStop(worldeeStop);
            day.Activities.Add(activity);
        }

        if (worldeeDay.sleepStop is not null)
        {
            var worldeeSleep = worldeeDay.sleepStop;
            var worldeeDirection = worldeeTravel.directions[directionIdx++];
            var hotel = new Accomodation
            {
                Description = worldeeDirection.description + "\n" + worldeeSleep.description,
                Location = MapLocation(worldeeSleep.placeId, worldeeSleep.placeResolution),
                Transportation = MapTransportation(worldeeDirection, worldeeSleep.additional.parkingPlaceId),
            };
            day.Accomodation = hotel;
        }

        return day;
    }

    private Activity MapStop(Stop worldeeStop)
    {
        var step = new Activity
        {
            Type = worldeeStop.type,
            Description = worldeeStop.description,
            ItemPrices = new(worldeeStop.additional.priceList.Select(MapPrice).ToList()),
            Location = MapLocation(worldeeStop.placeId, worldeeStop.placeResolution),
            IsHighlight = worldeeStop.additional.highlight,
            Steps = new (worldeeStop.stops.Select(MapSubStop).ToList()),
        };
        return step;
    }

    private Step MapSubStop(Stop1 worldeeStop)
    {
        return new Step
        {
            Description = worldeeStop.description,
            ItemPrices = new(worldeeStop.additional.priceList.Select(MapPrice).ToList()),
            Location = MapLocation(worldeeStop.placeId, worldeeStop.placeResolution),
            IsHighlight = worldeeStop.additional.highlight,
        };
    }

    private static Activity MapDirection(Direction worldeeDirection)
    {
        var travelActivity = new Activity
        {
            Type = "TRAVEL",
            Description = worldeeDirection.description,
            Transportation = new()
            {
                Kind = worldeeDirection.transportType,
                Distance = worldeeDirection.distance,
                Time = TimeSpan.FromSeconds(worldeeDirection.time),
                Intensity = GetIntensity(worldeeDirection.additional.physicalDemands),
            },
            ItemPrices = new (worldeeDirection.additional.priceList.Select(MapPrice).ToList()),
        };

        return travelActivity;
    }

    private static Transportation MapTransportation(Direction worldeeDirection)
    {
        var transportation = new Transportation
        {
            Kind = worldeeDirection.transportType,
            Distance = worldeeDirection.distance,
            Time = TimeSpan.FromSeconds(worldeeDirection.time),
            Intensity = GetIntensity(worldeeDirection.additional.physicalDemands),
        };

        return transportation;
    }

    private Transportation MapTransportation(Direction worldeeDirection, string parkingId)
    {
        var transportation = MapTransportation(worldeeDirection);
        if (parkingId is not null)
        {
            transportation.Parking = MapLocation(parkingId, "STREET");
        }

        return transportation;
    }

    private Location MapLocation(string locationId, string type)
    {
        var worldeeLocation = worldeeTravel.places.First(x => x.placeId == locationId);
        var location = new Location
        {
            Type = type,
            Address = worldeeLocation.name,
            City = worldeeLocation.localityName,
            Country = worldeeLocation.country,
            Coordinates = new Coordinate
            {
                Latitude = worldeeLocation.point.lat,
                Longitude = worldeeLocation.point.lng,
            }
        };
        return location;
    }

    private static Price MapPrice(PriceList worldeePrice) =>
        new Price
        {
            Name = worldeePrice.name,
            Cost = (long)(float.Parse(worldeePrice.price) * 100),
            Currency = worldeePrice.currency,
        };

    private static int GetIntensity(string worldeeType) =>
        worldeeType switch
        {
            "NONE" => -1,
            "EASY" => 2,
            "MEDIUM" => 5,
            "HARD" => 8,
            _ => 0,
        };
}

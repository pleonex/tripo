using System.Globalization;
using PleOps.Tripo.Traveller;

namespace PleOps.Tripo.Integrations;

public static class WorldeeMapper
{
    public static Travel Map(WorldeeTravel worldee)
    {
        var travel = new Travel
        {
            Name = worldee.name,
            Description = worldee.description,
            Conclusion = worldee.descriptionEnd,
        };

        DateTime startDate = DateTime.ParseExact(worldee.dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        for (int i = 0; i < worldee.days.Length; i++)
        {
            var worldeeDay = worldee.days[i];
            var day = new TravelDay
            {
                RelativeDay = i,
                Description = worldeeDay.description,
                Date = startDate + TimeSpan.FromDays(i),
            };

            for (int j = 0; j < worldeeDay.stops.Length; j++)
            {
                var worldeeStop = worldeeDay.stops[j];
                var step = new Activity
                {
                    Description = worldeeStop.description,
                };
                day.Activities.Add(step);
            }

            travel.Days.Add(day);
        }
        
        return travel;
    }
}

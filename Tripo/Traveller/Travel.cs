using System.Collections.ObjectModel;

namespace PleOps.Tripo.Traveller;

public class Travel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Conclusion { get; set; } = string.Empty;

    public Collection<TravelDay> Days { get; init;  } = new();

    public Collection<string> Lugagge { get; init; } = new(); // add num item, name, kg

    public Collection<string> Gifts { get; init; } = new(); // add to activity/step?? add price

    public string Visa { get; set; } = string.Empty; // how to, conditions, web, price

    public string Vaccines { get; set; } = string.Empty; // list?
}

public class TravelDay
{
    public int RelativeDay { get; set; } = -1;

    public DateTime Date { get; set; } = DateTime.MinValue;

    public string Description { get; set; } = string.Empty;

    public Collection<Activity> Activities { get; init; } = new();

    public Accomodation Accomodation { get; set; } = new();
}

public class Accomodation // add to global and set start/end? then how to transport? link with ID?
{
    public Location Location { get; set; } = new();

    public Transportation Transportation { get; set; } = new();

    public string Description { get; set; } = string.Empty;

    public Price Price { get; set; } = new();
}

public class Activity
{
    public string Type { get; set; } = string.Empty; // run, hike, walk city, travel by plane, long drive, eat, chill-out in hotel

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Location Location { get; set; } = new(); // aprox, optional for plane?

    public Transportation Transportation { get; set; } = new(); // global or define per step

    public Collection<Price> ItemPrices { get; init; } = new();

    public Collection<Step> Steps { get; init; } = new();

    public Collection<Media> Media { get; init; } = new();

    public bool IsHighlight { get; set; }
}

public class Step
{
    public string Description { get; set; } = string.Empty;

    public Location Location { get; set; } = new();

    public Transportation Transportation { get; set; } = new(); // how to get to this step

    public Collection<Price> ItemPrices { get; init; } = new();

    public Collection<Media> Media { get; init; } = new();

    public bool IsHighlight { get; set; }
}

public class Media
{
    public string Type { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;
}

public class Transportation
{
    public string Kind { get; set; } = string.Empty;

    public int Distance { get; set; }

    public TimeSpan Time { get; set; }

    public string GpxFilePath { get; set; } = string.Empty; // run, hike, download from strava/garmin

    public int Intensity { get; set; }

    public Location Parking { get; set; } = new();
}

public class Location
{
    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public Coordinate Coordinates { get; set; } = new();
}

public class Coordinate
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}

public class Price
{
    public string Name { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public long Cost { get; set; }
}

using System.Collections.ObjectModel;

namespace PleOps.Tripo.Traveller;

public class Travel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Collection<TravelDay> Days { get; init;  } = new();
}

public class TravelDay
{
    public int RelativeDay { get; set; } = -1;

    public DateTime Date { get; set; } = DateTime.MinValue;

    public string Description { get; set; } = string.Empty;
}

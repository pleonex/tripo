using CommunityToolkit.Mvvm.ComponentModel;

namespace PleOps.Tripo.Traveller;

public class TravelDayViewModel : ObservableObject, IQueryAttributable
{
    public TravelDayViewModel()
    {
        Day = new TravelDay();
    }

    public TravelDayViewModel(TravelDay day)
    {
        Day = day;
    }
    
    public TravelDay Day { get; private set; }
    
    public string Description => Day.Description;

    public DateTime Date => Day.Date;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Day", out object dayObj) && dayObj is TravelDayViewModel dayVm)
        {
            Day = dayVm.Day;
            RefreshProperties();
        }
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Date));
    }
}

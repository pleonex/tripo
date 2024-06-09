using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace PleOps.Tripo.Traveller;

public partial class TravelDayViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private TravelDay day;

    public TravelDayViewModel()
    {
        Day = new TravelDay();
        Activities = new ObservableCollection<Activity>();
    }

    public TravelDayViewModel(TravelDay day)
    {
        Day = day;
        Activities = new ObservableCollection<Activity>(day.Activities);
    }
    
    public Accomodation Accomodation => Day.Accomodation;

    public string Title => Day.Title;

    public string Description => Day.Description;

    public DateTime Date => Day.Date;

    public int RelativeDay => Day.RelativeDay + 1;

    public ObservableCollection<Activity> Activities { get; private set; }

    public int ActivitiesCount => Activities.Count;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Day", out object? dayObj) && dayObj is TravelDayViewModel dayVm)
        {
            Day = dayVm.Day;
            Activities = new ObservableCollection<Activity>(Day.Activities);
            RefreshProperties();
        }
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Title));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(RelativeDay));
        OnPropertyChanged(nameof(Activities));
        OnPropertyChanged(nameof(ActivitiesCount));
        OnPropertyChanged(nameof(Accomodation));
    }
}

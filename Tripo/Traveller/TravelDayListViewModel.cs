using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PleOps.Tripo.Mvvm;

namespace PleOps.Tripo.Traveller;

public partial class TravelDayListViewModel : ObservableObject
{
    private string? travelPath;
    
    [ObservableProperty]
    private Travel? travel;

    public ObservableCollection<TravelDayViewModel> Days { get; } = new();

    public AsyncInteraction<Exception, bool> DisplayException { get; } = new();

    public AsyncInteraction<string?> SelectTravelFile { get; } = new();

    public AsyncInteraction<string?> AskEditTravelName { get; } = new();

    [RelayCommand]
    private void LoadLastTravel()
    {
        var lastTravelFile = Preferences.Default.Get("last_travel_file", string.Empty);
        if (string.IsNullOrEmpty(lastTravelFile))
        {
            return;
        }

        try
        {
            var lastTravel = TravelFactory.LoadJson(lastTravelFile);
            UpdateLoadedTravel(lastTravelFile, lastTravel);
        }
        catch (Exception)
        {
            // do nothing, just show an empty one.
        }
    }
    
    [RelayCommand]
    private async Task PickLoadTravelAsync()
    {
        var selectedFile = await SelectTravelFile.HandleAsync().ConfigureAwait(false);
        if (selectedFile is null) {
            return;
        }

        try
        {
            var newTravel = await TravelFactory.LoadJsonAsync(selectedFile).ConfigureAwait(false);
            UpdateLoadedTravel(selectedFile, newTravel);
        }
        catch (Exception ex)
        {
            await DisplayException.HandleAsync(ex).ConfigureAwait(false);
        }
    }

    [RelayCommand]
    private async Task PickLoadWorldeeTravelAsync()
    {
        var selectedFile = await SelectTravelFile.HandleAsync().ConfigureAwait(false);
        if (selectedFile is null) {
            return;
        }

        try
        {
            var newTravel = await TravelFactory.LoadWorldeeJsonAsync(selectedFile).ConfigureAwait(false);
            
            string filename = "converted-" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
            string lastFile = Path.Combine(FileSystem.AppDataDirectory, filename);
            await TravelFactory.SaveJsonAsync(newTravel, lastFile);

            UpdateLoadedTravel(lastFile, newTravel);
        }
        catch (Exception ex)
        {
            await DisplayException.HandleAsync(ex).ConfigureAwait(false);
        }
    }
    
    [RelayCommand]
    private async Task EditTravelName()
    {
        string? newName = await AskEditTravelName.HandleAsync().ConfigureAwait(false);
        if (newName is not null)
        {
            Travel.Name = newName;
            await TravelFactory.SaveJsonAsync(Travel, travelPath).ConfigureAwait(false);
            OnPropertyChanged(nameof(Travel));
        }
    }

    [RelayCommand]
    private async Task NewDayAsync()
    {
        await Shell.Current.GoToAsync(nameof(TravelDayView));
    }
    
    [RelayCommand]
    private async Task DaySelectedAsync(TravelDayViewModel day)
    {
        if (day is null)
        {
            return;
        }
        
        var args = new Dictionary<string, object> { { "Day", day } };
        await Shell.Current.GoToAsync(nameof(TravelDayView), args);
    }

    private void UpdateLoadedTravel(string path, Travel travel)
    {
        travelPath = path;
        Preferences.Default.Set("last_travel_file", path);
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Travel = travel;

            Days.Clear();
            var dayViewModels = travel.Days.Select(x => new TravelDayViewModel(x));
            foreach (var day in dayViewModels)
            {
                Days.Add(day);
            }
        });
    }
}

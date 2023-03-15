using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace PleOps.Tripo.Traveller;

public class TravelDayListViewModel : ObservableObject
{
    private Travel? travel;

    public TravelDayListViewModel()
    {
        LoadTravelCommand = new AsyncRelayCommand(PickLoadTravelAsync);
        LoadLastTravelCommand = new RelayCommand(LoadLastTravel);
        LoadWorldeeTravelCommand = new AsyncRelayCommand(PickLoadWorldeeTravelAsync);
        NoteSelectedCommand = new RelayCommand<int>(NoteSelected);
    }

    public Travel? Travel
    {
        get => travel;
        private set => SetProperty(ref travel, value);
    }
    
    public IAsyncRelayCommand LoadTravelCommand { get; }
    
    public IRelayCommand LoadLastTravelCommand { get; }
    
    public IAsyncRelayCommand LoadWorldeeTravelCommand { get; }

    public IRelayCommand<int> NoteSelectedCommand { get; }

    private void LoadLastTravel()
    {
        var lastTravelFile = Preferences.Default.Get<string>("last_travel_file", string.Empty);
        if (string.IsNullOrEmpty(lastTravelFile))
        {
            return;
        }
        
        var travel = TravelFactory.LoadJson(lastTravelFile);
        Travel = travel;
    }
    
    private async Task PickLoadTravelAsync()
    {
        var selectedFile = await PickFileAsync();
        if (selectedFile is null) {
            return;
        }

        var travel = await TravelFactory.LoadJsonAsync(selectedFile.FullPath).ConfigureAwait(false);
        Preferences.Default.Set("last_travel_file", selectedFile.FullPath);
        Travel = travel;
    }

    private async Task PickLoadWorldeeTravelAsync()
    {
        var selectedFile = await PickFileAsync();
        if (selectedFile is null) {
            return;
        }

        var travel = await TravelFactory.LoadWorldeeJsonAsync(selectedFile.FullPath).ConfigureAwait(false);

        string filename = "converted-" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
        string lastFile = Path.Combine(FileSystem.AppDataDirectory, filename);
        await TravelFactory.SaveJsonAsync(travel, lastFile);
        
        Preferences.Default.Set("last_travel_file", lastFile);
        Travel = travel;
    }

    private async Task<FileResult?> PickFileAsync()
    {
        PickOptions options = new()
        {
            PickerTitle = "Open your travel file",
            FileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/json" } },
                    { DevicePlatform.WinUI, new[] { ".json" } },
                }),
        };

        try
        {
            return await FilePicker.Default.PickAsync(options).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    private void NoteSelected(int id)
    {
    }
}

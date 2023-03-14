using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace PleOps.Tripo.Traveller;

public class WorldeeViewModel : ObservableObject
{
    private WorldeeTravel travel;

    public WorldeeViewModel()
    {
        LoadTravelCommand = new AsyncRelayCommand(PickLoadTravelAsync);
        LoadLastTravelCommand = new RelayCommand(LoadLastTravel);
        NoteSelectedCommand = new RelayCommand<int>(NoteSelected);
    }

    public WorldeeTravel Travel
    {
        get => travel;
        private set => SetProperty(ref travel, value);
    }
    
    public IAsyncRelayCommand LoadTravelCommand { get; }
    
    public IRelayCommand LoadLastTravelCommand { get; }

    public IRelayCommand<int> NoteSelectedCommand { get; }

    private void LoadLastTravel()
    {
        var lastTravelFile = Preferences.Default.Get<string>("last_travel_file", string.Empty);
        if (string.IsNullOrEmpty(lastTravelFile))
        {
            return;
        }
        
        
        var travel = LoadTravel(lastTravelFile);
        if (travel is not null)
        {
            Travel = travel;
        }
    }
    
    private async Task PickLoadTravelAsync()
    {
        var selectedFile = await PickFileAsync();
        if (selectedFile is null) {
            return;
        }

        var travel = await LoadTravelAsync(selectedFile.FullPath).ConfigureAwait(false);
        if (travel is not null) {
            Preferences.Default.Set("last_travel_file", selectedFile.FullPath);
            Travel = travel;
        }
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

    private async Task<WorldeeTravel?> LoadTravelAsync(string path)
    {
        try
        {
            await using var jsonStream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<WorldeeTravel>(jsonStream).ConfigureAwait(false);
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    private WorldeeTravel? LoadTravel(string path)
    {
        try
        {
            using var jsonStream = File.OpenRead(path);
            return JsonSerializer.Deserialize<WorldeeTravel>(jsonStream);
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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace PleOps.Tripo.Traveller;

public class WordeeViewModel : ObservableObject
{
    private WordeeTravel travel;

    public WordeeViewModel()
    {
        LoadTravelCommand = new AsyncRelayCommand(LoadTravelAsync);
        NoteSelectedCommand = new RelayCommand<int>(NoteSelected);
    }

    public WordeeTravel Travel
    {
        get => travel;
        private set => SetProperty(ref travel, value);
    }

    public IAsyncRelayCommand LoadTravelCommand { get; }

    public IRelayCommand<int> NoteSelectedCommand { get; }

    private async Task LoadTravelAsync()
    {
        string path = @"C:\Users\benit\source\repos\Tripo\worldee.json";
        var content = await File.ReadAllTextAsync(path).ConfigureAwait(false);
        Travel = JsonSerializer.Deserialize<WordeeTravel>(content);
    }

    private void NoteSelected(int id)
    {
    }
}

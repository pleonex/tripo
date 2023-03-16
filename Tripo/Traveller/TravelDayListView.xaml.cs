namespace PleOps.Tripo.Traveller;

public partial class TravelDayListView : ContentPage
{
	public TravelDayListView()
	{
		InitializeComponent();
		ViewModel.DisplayException.RegisterHandler(DisplayExceptionAsync);
		ViewModel.SelectTravelFile.RegisterHandler(PickFileAsync);
		ViewModel.AskEditTravelName.RegisterHandler(AskEditTravelNameAsync);
	}

	private TravelDayListViewModel ViewModel => (TravelDayListViewModel)BindingContext;

	private void OnAppearing(object? sender, EventArgs e)
	{
		ViewModel.LoadLastTravelCommand.Execute(null);
	}

	private async Task<bool> DisplayExceptionAsync(Exception ex)
	{
		await MainThread.InvokeOnMainThreadAsync(() =>
			DisplayAlert("Unexpected error", ex.ToString(), "Ok"));
		return true;
	}

	private async Task<string?> PickFileAsync()
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
			var result = await FilePicker.Default.PickAsync(options).ConfigureAwait(false);
			return result?.FullPath;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private async Task<string?> AskEditTravelNameAsync()
	{
		return await DisplayPromptAsync(
			"Edit travel",
			"New travel name",
			initialValue: ViewModel.Travel.Name);
	}
}

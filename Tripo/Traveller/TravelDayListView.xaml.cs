namespace PleOps.Tripo.Traveller;

public partial class TravelDayListView : ContentPage
{
	public TravelDayListView()
	{
		InitializeComponent();
	}

	private void OnAppearing(object? sender, EventArgs e)
	{
		if (BindingContext is TravelDayListViewModel viewModel)
		{
			viewModel.LoadLastTravelCommand.Execute(null);
		}
	}
}
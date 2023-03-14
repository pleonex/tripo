namespace PleOps.Tripo.Traveller;

public partial class WorldeeView : ContentPage
{
	public WorldeeView()
	{
		InitializeComponent();
	}

	private void OnAppearing(object? sender, EventArgs e)
	{
		if (BindingContext is WorldeeViewModel viewModel)
		{
			viewModel.LoadLastTravelCommand.Execute(null);
		}
	}
}
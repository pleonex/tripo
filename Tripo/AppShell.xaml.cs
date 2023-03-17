namespace PleOps.Tripo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(Traveller.TravelDayView), typeof(Traveller.TravelDayView));
	}
}

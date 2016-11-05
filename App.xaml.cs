using Xamarin.Forms;

namespace DemoLights
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new CustomNavigationPage(new DemoLightsPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}

	public class CustomNavigationPage : NavigationPage
	{
		public CustomNavigationPage(Page page) : base(page)
		{
			BarTextColor = Color.White;
			BarBackgroundColor = Color.Black;
		}
	}
}

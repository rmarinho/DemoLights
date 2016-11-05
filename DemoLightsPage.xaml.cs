using System;
using Xamarin.Forms;

namespace DemoLights
{
	public partial class DemoLightsPage : ContentPage
	{
		public DemoLightsPage()
		{
			InitializeComponent();
			var service = new MockLightService();
			BindingContext = new LightsViewModel(service);

			MessagingCenter.Send(this, "AddControll", ColorPickerRoot);
			List.ItemSelected += (sender, e) => ((ListView)sender).SelectedItem = null;
		}

		async void LightTapped(object sender, EventArgs e)
		{
			await ColorPicker.TranslateTo(0, 0);
			(BindingContext as LightsViewModel).CurrentLightViewModel = ((Image)sender).BindingContext as LightModel;
		}

		async void DismissClicked(object sender, EventArgs e)
		{
			await ColorPicker.TranslateTo(0, ColorPicker.Height);
			(BindingContext as LightsViewModel).CurrentLightViewModel = null;
		}
	}
}

using System;
using Xamarin.Forms;

namespace DemoLights
{
	public class GradientBackgroundView : ContentView
	{
		public GradientBackgroundView()
		{

		}

		public Color StartColor
		{
			get;
			set;
		}

		public Color EndColor
		{
			get;
			set;
		}
	}
}

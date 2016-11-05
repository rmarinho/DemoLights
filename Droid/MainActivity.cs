using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using DemoLights;
using DemoLights.Droid;
using Xamarin.Forms;
using System.Globalization;

[assembly: ExportRenderer(typeof(GradientBackgroundView), typeof(GradientBackgroundViewRenderer))]
namespace DemoLights.Droid
{
	[Activity(Label = "DemoLights.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

			//MessagingCenter.Subscribe<DemoLightsPage, StackLayout>(this, "AddControll", (page, stack) =>
			//			{
			//				var picker = new ColorPickerView();
			//				picker.SetBinding(
			//					   nameof(ColorPickerView.SelectedColor),
			//					   new Binding("PickerSelectedColor", BindingMode.TwoWay, new DroidColorConverter())
			//					 , nameof(ColorPickerView.ColorPicked)
			//				   );
			//				stack.Children.Add(picker);
			//			});

			LoadApplication(new App());
		}
	}

	public class DroidColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Color)
				return ((Color)value).ToAndroid();

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is global::Android.Graphics.Color)
				return ((global::Android.Graphics.Color)value).ToColor();

			return null;
		}
	}


	public class GradientBackgroundViewRenderer : ViewRenderer
	{
		protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
		{
			var gradientView = Element as GradientBackgroundView;
			var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
				gradientView.StartColor.ToAndroid(),
				gradientView.EndColor.ToAndroid(),
				Android.Graphics.Shader.TileMode.Mirror);

			var paint = new Android.Graphics.Paint()
			{
				Dither = true,
			};
			paint.SetShader(gradient);

			canvas.DrawPaint(paint);

			base.DispatchDraw(canvas);
		}
	}
}

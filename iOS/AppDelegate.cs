using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdvancedColorPicker;
using CoreAnimation;
using CoreGraphics;
using DemoLights;
using DemoLights.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientBackgroundView), typeof(GradientBackgroundViewRenderer))]

namespace DemoLights.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
			{
				Font = UIFont.FromName("Helvetica", 14)
			};

			UIBarButtonItem.Appearance.SetTitleTextAttributes(new UITextAttributes { Font = UIFont.FromName("Helvetica", 14) }, UIControlState.Normal);

			global::Xamarin.Forms.Forms.Init();

			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

			//MessagingCenter.Subscribe<DemoLightsPage, StackLayout>(this, "AddControll", (page, stack) =>
			//{
			//	var picker = new ColorPickerView(new CGRect(0, 0, stack.Width, 400));
			//	picker.SetBinding(
			//		 nameof(ColorPickerView.SelectedColor),
			//		 new Binding("PickerSelectedColor", BindingMode.TwoWay, new UIColorConverter()),
			//		 nameof(ColorPickerView.ColorPicked)
			//	);
			//	stack.Children.Add(picker);
			//});

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}

	public class UIColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Color)
				return ((Color)value).ToUIColor();
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is UIColor)
				return ((UIColor)value).ToColor();
			return value;
		}
	}


	public class GradientBackgroundViewRenderer : ViewRenderer
	{
		public override void LayoutSubviews()
		{
			foreach (var layer in Control?.Layer.Sublayers.Where(layer => layer is CAGradientLayer))
				layer.Frame = Bounds;
			base.LayoutSubviews();
		}

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var gradientBackgroundVIew = e.NewElement as GradientBackgroundView;

					var view = new UIView(Bounds);

					var gradient = new CAGradientLayer();
					gradient.Frame = Bounds;
					gradient.Colors = new CGColor[]
					{
							gradientBackgroundVIew.StartColor.ToCGColor(),
							gradientBackgroundVIew.EndColor.ToCGColor()
					};
					view?.Layer.InsertSublayer(gradient, 0);

					SetNativeControl(view);
				}
			}
		}
	}
}

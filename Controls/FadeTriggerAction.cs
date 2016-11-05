using System;
using Xamarin.Forms;

namespace DemoLights
{
	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		public int StartsFrom { set; get; }

		protected override void Invoke(VisualElement visual)
		{
			visual.Animate("animation", new Animation((d) =>
			{
				var val = StartsFrom == 1 ? d : 1 - d;
				visual.Opacity = val;

			}),
			length: 1000, // milliseconds
			easing: Easing.Linear);
		}
	}
}

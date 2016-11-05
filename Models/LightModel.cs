using System;
using Xamarin.Forms;

namespace DemoLights
{
	public class LightModel : BaseViewModel
	{
		public string Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		string _state;
		public string State
		{
			get { return _state; }
			set
			{
				if (_state == value)
					return;
				_state = value;
				OnPropertyChanged();
			}
		}

		bool _isOn;
		public bool IsOn
		{
			get { return _isOn; }
			set
			{
				if (_isOn == value)
					return;
				_isOn = value;
				OnPropertyChanged();
				if (_isOn && _brightness == 0)
					Brightness = 255;
				if (_isOn && _color == Color.Default)
					Color = Color.White.MultiplyAlpha(0.5);
			}
		}

		int _brightness;
		public int Brightness
		{
			get { return _brightness; }
			set
			{
				if (_brightness == value)
					return;
				_brightness = value;
				OnPropertyChanged();
			}
		}

		int _whiteValue;
		public int WhiteValue
		{
			get { return _whiteValue; }
			set
			{
				if (_whiteValue == value)
					return;
				_whiteValue = value;
				OnPropertyChanged();
			}
		}

		Color _color;
		public Color Color
		{
			get { return _color; }
			set
			{
				if (_color == value)
					return;
				_color = value;
				OnPropertyChanged();
			}
		}
	}
}

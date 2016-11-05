using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoLights
{
	public class LightsViewModel : BaseViewModel
	{
		readonly ILightService _lightService;

		public LightsViewModel(ILightService lightService)
		{
			_lightService = lightService;
			Init();
		}

		async Task Init()
		{
			IsBusy = true;

			var lights = await _lightService.GetLightsAsync();
			foreach (var item in lights)
			{
				Lights.Add(item);
				item.PropertyChanged += Item_PropertyChanged;
			}

			IsBusy = false;
		}

		void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_lightService.UpdateLightState(sender as LightModel);
		}

		ObservableCollection<LightModel> _lights = new ObservableCollection<LightModel>();
		public ObservableCollection<LightModel> Lights
		{
			get
			{
				return _lights;
			}
			set
			{
				if (_lights == value)
					return;
				_lights = value;
				OnPropertyChanged();
			}
		}

		Color _pickerSelectedColor;
		public Color PickerSelectedColor
		{
			get { return _pickerSelectedColor; }
			set
			{
				if (_pickerSelectedColor == value)
					return;
				_pickerSelectedColor = value;
				OnPropertyChanged();

				if (CurrentLightViewModel != null && CurrentLightViewModel.Color != _pickerSelectedColor)
				{
					CurrentLightViewModel.Color = _pickerSelectedColor.MultiplyAlpha(0.5);
				}
			}
		}

		LightModel _currentLightViewModel;
		public LightModel CurrentLightViewModel
		{
			get { return _currentLightViewModel; }
			set
			{
				if (_currentLightViewModel == value)
					return;
				_currentLightViewModel = value;

				OnPropertyChanged();

				if (_currentLightViewModel != null)
					PickerSelectedColor = _currentLightViewModel.Color;
			}
		}
	}
}

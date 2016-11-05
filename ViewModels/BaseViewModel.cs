using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DemoLights
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (_isBusy == value)
					return;
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//	public virtual INavigation Navigation => (App.Current.MainPage as RootPage).Detail.Navigation;
	}
}

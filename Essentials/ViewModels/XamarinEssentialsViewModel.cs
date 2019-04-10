namespace Essentials.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Essentials.Views;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class XamarinEssentialsViewModel : BindableObject
    {
        private string _onSelectedItem { get; set; }
        private ObservableCollection<string> _xamarinEssential;

        public string OnSelectedItem
        {
            get => _onSelectedItem;

            set
            {
                _onSelectedItem = value;
                ShowView();
            }
        }

        public ObservableCollection<string> XamarinEssential
        {
            get => _xamarinEssential;
            set
            {
                _xamarinEssential = value;
                OnPropertyChanged();
            }
        }


        #region App Information

        // Application Name
        public string AppName { get; set; } = AppInfo.Name;

        // Package Name/Application Identifier
        public string PackageName { get; set; } = AppInfo.PackageName;

        // Application Version (1.0.0)
        public string Version { get; set; } = AppInfo.VersionString;

        // Application Build Number (1)
        public string Build { get; set; } = AppInfo.BuildString;

        #endregion  

        #region Battery

        private double level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.
        private BatteryState state = Battery.State;
        private BatteryPowerSource source = Battery.PowerSource;
        private EnergySaverStatus saverStatus = Battery.EnergySaverStatus;

        public ICommand BatteryLevelCheckCommand => new Command(BatteryLevelCheck);
        public ICommand BatteryStateCheckCommand => new Command(BatteryStateCheck);
        public ICommand BatterySourceCommand => new Command(BatterySource);
        public ICommand BatterySaveStatusCommand => new Command(BatterySaveStatus);

        private async void BatteryLevelCheck() => await Application.Current.MainPage.DisplayAlert("ChargeLevel", level.ToString(), "OK");

        private async void BatteryStateCheck()
        {
            string batterySource = string.Empty;

            switch (state)
            {
                case BatteryState.Charging: // Currently charging
                    batterySource = "Charging"; break;
                case BatteryState.Full: // Battery is full
                    batterySource = "Full"; break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging: // Currently discharging battery or not being charged
                    batterySource = "NotCharging"; break;
                case BatteryState.NotPresent: // Battery doesn't exist in device (desktop computer)
                case BatteryState.Unknown: // Unable to detect battery state
                    batterySource = "Unknown"; break;
            }

            await Application.Current.MainPage.DisplayAlert("Battery Source", batterySource, "OK");
        }

        private async void BatterySource()
        {
            string batterySource = string.Empty;

            switch (source)
            {
                case BatteryPowerSource.Battery: // Being powered by the battery
                    batterySource = "Battery"; break;
                case BatteryPowerSource.AC: // Being powered by A/C unit
                    batterySource = "AC"; break;
                case BatteryPowerSource.Usb: // Being powered by USB cable
                    batterySource = "Usb"; break;
                case BatteryPowerSource.Wireless: // Powered via wireless charging
                    batterySource = "Wireless"; break;
                case BatteryPowerSource.Unknown: // Unable to detect power source
                    batterySource = "Unknown"; break;
            }

            await Application.Current.MainPage.DisplayAlert("Battery Source", batterySource, "OK");
        }

        private async void BatterySaveStatus()
        {
            if(saverStatus == EnergySaverStatus.On)
            {
                await Application.Current.MainPage.DisplayAlert("EnergySaverStatus", "On", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("EnergySaverStatus", "Off", "OK");
            }
        }

        #endregion

        public void Initialize()
        {
            List<string> xamarinEssential = new List<string>
            {
                "App Information",
                "Battery"
            };

            XamarinEssential = new ObservableCollection<string>(xamarinEssential);
        }

        private async void ShowView()
        {
            if (OnSelectedItem == null) return;

            if(OnSelectedItem.Contains("App Information"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new AppInformationView());
            }
            else if (OnSelectedItem.Contains("Battery"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new BatteryView());
            }
        }
    }
}

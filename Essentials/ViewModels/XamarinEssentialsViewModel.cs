namespace Essentials.ViewModels
{
    using System;
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
                OnPropertyChanged();
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

        #region Clipboard
        private string _textClip { get; set; }

        public string TextClip
        {
            get => _textClip;

            set
            {
                _textClip = value;
                OnPropertyChanged();
            }

        }

        public ICommand ClipSetTextCommand => new Command(ClipSetText);
        public ICommand ClipGetTextCommand => new Command(ClipGetText);

        public async void ClipSetText() => await Clipboard.SetTextAsync("Hello World");

        public async void ClipGetText() => TextClip = Clipboard.HasText ? await Clipboard.GetTextAsync() : "Clipboard is empty";

        #endregion

        #region Connectivity
        private string _statusconnectivity { get; set; }

        public string StatusConnectivity
        {
            get => _statusconnectivity;

            set
            {
                _statusconnectivity = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckConnectivityCommand => new Command(CheckConnectivity);

        private void CheckConnectivity(object obj)
        {
            var current = Connectivity.NetworkAccess;

            switch (current)
            {
                case NetworkAccess.ConstrainedInternet:
                    StatusConnectivity = "Limited internet access. Indicates captive portal connectivity, where local access to a web portal is provided, but access to the Internet requires that specific credentials are provided via a portal";
                    break;
                case NetworkAccess.Internet:
                    StatusConnectivity = "Connection to internet is available";
                    break;
                case NetworkAccess.Local:
                    StatusConnectivity = "Local network access only";
                    break;
                case NetworkAccess.None:
                    StatusConnectivity = "No connectivity is available";
                    break;
                case NetworkAccess.Unknown:
                    StatusConnectivity = "Unable to determine internet connectivity";
                    break;
            }

            // You can check what type of connection profile the device is actively using:

            //https://docs.microsoft.com/en-us/xamarin/essentials/connectivity?context=xamarin%2Fxamarin-forms&tabs=android
        }
        #endregion

        #region Device Information
        public string Device { get; set; } = $"Device: {DeviceInfo.Model}";

        public string Manufacturer { get; set; } = $"Manufacturer: {DeviceInfo.Manufacturer}";

        public string DeviceName { get; set; } = $"Device Name: {DeviceInfo.Name}";

        public string VersionDeviceInfo { get; set; } = $"Version Device Info: {DeviceInfo.VersionString}";

        public string Platform { get; set; } = $"Platform: {DeviceInfo.Platform.ToString()}";

        public string Idiom { get; set; } = $"Idiom: {DeviceInfo.Idiom.ToString()}";

        public string DeviceType { get; set; } = $"Device Type: {DeviceInfo.DeviceType.ToString()}";
        #endregion

        #region Flashlight
        private string _flash { get; set; }
        private bool _flashlightStatus { get; set; }

        public string Flash
        {
            get => _flash;

            set
            {
                _flash = value;
                OnPropertyChanged();
            }

        }

        public bool FlashlightStatus
        {
            get => _flashlightStatus;

            set
            {
                _flashlightStatus = value;
                OnPropertyChanged();
            }

        }

        public ICommand FlashlightTurnCommand => new Command(FlashlightTurn);

        public async void FlashlightTurn()
        {
            if (!FlashlightStatus)
            {
                await Flashlight.TurnOnAsync();
                Flash = "Flash light turn on";
                FlashlightStatus = true;
            }
            else
            {
                await Flashlight.TurnOffAsync();
                Flash = "Flash light turn off";
                FlashlightStatus = false;
            }
        }
        #endregion

        #region OpenBrowser
        public ICommand OpenBrowserCommand => new Command(OpenBrowser);

        public async void OpenBrowser()
        {
            Uri uri = new Uri("https://docs.microsoft.com/es-es/xamarin/essentials/open-browser?tabs=uwp");

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.Red,
                PreferredControlColor = Color.Wheat
            });
        }
        #endregion

        #region Phone Dialer 
        public ICommand PhoneCallCommand => new Command(PhoneCall);

        public void PhoneCall() => PhoneDialer.Open("933 63 89 50");
        #endregion

        #region Share 
        public ICommand ShareUrlCommand => new Command(ShareUrl);

        public async void ShareUrl()
        {
            Uri uri = new Uri("https://docs.microsoft.com/es-es/xamarin/essentials/open-browser?tabs=uwp");

            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri.ToString(),
                Title = "xamarin essential"
            });
        }
        #endregion

        #region Vibration
        public ICommand VibrationActiveCommand => new Command(VibrationActive);

        public void VibrationActive()
        {
            Vibration.Vibrate();

            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);
        }
        #endregion

        public void Initialize()
        {
            List<string> xamarinEssential = new List<string>
            {
                "App Information",
                "Battery",
                "Clipboard",
                "Connectivity",
                "Device Information",
                "Flashlight",
                "OpenBrowser",
                "Phone Dialer", 
                "Share",
                "Vibration",

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
            else if (OnSelectedItem.Contains("Clipboard"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new ClipboardView());
            }
            else if (OnSelectedItem.Contains("Connectivity"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new ConnectivityView());
            }
            else if (OnSelectedItem.Contains("Device Information"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new DeviceInformationView());
            }
            else if (OnSelectedItem.Contains("Flashlight"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new FlashlightView());
            }
            else if (OnSelectedItem.Contains("OpenBrowser"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new OpenBrowserView());
            }
            else if (OnSelectedItem.Contains("Phone Dialer"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new PhoneDialerView());
            }
            else if (OnSelectedItem.Contains("Share"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new ShareView());
            }
            else if (OnSelectedItem.Contains("Vibration"))
            {
                await (Application.Current.MainPage as NavigationPage).Navigation.PushAsync(new VibrationView());
            }
        }
    }
}

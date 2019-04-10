namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class BatteryView : ContentPage
    {
        public BatteryView()
        {
            InitializeComponent();

            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class PhoneDialerView : ContentPage
    {
        public PhoneDialerView()
        {
            InitializeComponent();
            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

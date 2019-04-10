namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class PreferencesView : ContentPage
    {
        public PreferencesView()
        {
            InitializeComponent();
            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

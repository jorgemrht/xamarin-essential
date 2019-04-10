namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class AppInformationView : ContentPage
    {
        public AppInformationView()
        {
            InitializeComponent();

            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

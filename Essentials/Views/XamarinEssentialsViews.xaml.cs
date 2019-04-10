namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class XamarinEssentialsViews : ContentPage
    {
        public XamarinEssentialsViews()
        {
            InitializeComponent();

            BindingContext = new XamarinEssentialsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as XamarinEssentialsViewModel).Initialize();
        }
    }
}

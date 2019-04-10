namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class SecureStorageView : ContentPage
    {
        public SecureStorageView()
        {
            InitializeComponent();
            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

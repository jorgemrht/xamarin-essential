namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class FlashlightView : ContentPage
    {
        public FlashlightView()
        {
            InitializeComponent();
            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

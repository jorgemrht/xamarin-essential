namespace Essentials.Views
{
    using Essentials.ViewModels;
    using Xamarin.Forms;

    public partial class ClipboardView : ContentPage
    {
        public ClipboardView()
        {
            InitializeComponent();
            BindingContext = new XamarinEssentialsViewModel();
        }
    }
}

using Shop.UIForms.ViewModels;
using Shop.UIForms.Views;
using Xamarin.Forms;

namespace Shop.UIForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Get singleton instance
            MainViewModel.GetInstance().Login = new LoginViewModel();

            // NavigationPage adds a new navigation control page to the main activity
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

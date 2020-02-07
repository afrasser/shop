using GalaSoft.MvvmLight.Command;
using Shop.UIForms.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand { get => new RelayCommand(Login); }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    title: "Error",
                    message: "You must enter an email",
                    cancel: "Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    title: "Error",
                    message: "You must enter a password",
                    cancel: "Accept");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert("Message", "It works", "Ok");

            // Navigate to other page using Sigleton pattern
            // So this way we get the current in memory instance of MainViewModel
            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
    }
}

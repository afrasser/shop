using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
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

            await Application.Current.MainPage.DisplayAlert("Message", "It works", "Ok");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.UIForms.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }

        public MainViewModel()
        {
            //TODO: Change this instance
            Login = new LoginViewModel();
        }
    }
}

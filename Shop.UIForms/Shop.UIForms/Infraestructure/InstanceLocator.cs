using Shop.UIForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.UIForms.Infraestructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        // Locator Pattern for mantain only one ManViewModel instance. 
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}

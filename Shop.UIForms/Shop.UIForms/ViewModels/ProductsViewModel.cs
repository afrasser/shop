using Shop.Common.Models;
using Shop.Common.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Shop.UIForms.ViewModels
{
    // Note: BaseViewModel handle the action to refresh changes on the view.
    // BaseViewModel class not need to be changed, not need to be learned by hearth.
    public class ProductsViewModel : BaseViewModel
    {
        #region Attributes
        private APIServices apiService;
        private bool isRefreshing;
        // This is necessary to do in every viewmodel to refresh the view.
        private ObservableCollection<Product> products;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get => products;
            // Use SetValue by ref to update ViewModel correctly
            set => SetValue(ref products, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }
        #endregion

        #region Methods
        public ProductsViewModel()
        {
            apiService = new APIServices();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            IsRefreshing = true;
            var response = await apiService.GetListAsync<Product>(
                "https://shopafs.azurewebsites.net/",
                "/api",
                "/products");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myProducts = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(myProducts);

            IsRefreshing = false;
        }
        #endregion
    }
}

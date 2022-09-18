using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using wdskills.WebClient.Model;
using wdskills.WebClient.Service;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;
using wdskills.WPF.Services;

namespace wdskills.WPF.ViewModel.Main
{
    public class ProductViewModel : ObservableObject
    {
        private readonly ClientApiService _clientApiService;
        private static Random random = new();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ClientValidationService _validationService;
        private readonly TransferUserService _userService;
        private readonly TransferProductService _productService;
        private readonly TransferIsAddProductService _isAddProductService;

        private User? user;
        private Product? product;
        private ObservableCollection<string>? categories;
        private bool isAddProduct;
        private bool isAdminPanelProduct;
        private string? errorMessage;
        private string? selectedCategory = "Наборы";

        public bool IsUserAdmin { get; private set; }
        public User? User
        {
            get => user;
            set
            {
                user = value;
                IsUserAdmin = (value?.RoleId == 3);
                OnPropertyChanged("User");
            }
        }
        public Product? Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        public ObservableCollection<string>? Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }
        public bool IsAddProduct
        {
            get => isAddProduct;
            set
            {
                isAddProduct = value;
                OnPropertyChanged("IsAddProduct");
            }
        }

        public bool IsAdminPanelProduct
        {
            get => isAdminPanelProduct;
            set
            {
                isAdminPanelProduct = value;
                OnPropertyChanged("IsAdminPanelProduct");
            }
        }
        public string? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public string? ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public ProductViewModel(
            ClientApiService clientApiService,
            TransferUserService userService,
            TransferProductService productService,
            TransferIsAddProductService isAddProductService,
            ClientValidationService validationService)
        {
            _userService = userService;
            _productService = productService;
            _isAddProductService = isAddProductService;
            _validationService = validationService;
            _clientApiService = clientApiService;

            User = _userService.User;
            Product = _productService.Product;
            IsAddProduct = isAddProductService.IsAddProduct;
            IsAdminPanelProduct = !IsAddProduct;
            Task.Run(async () =>
            Categories = new (await _clientApiService.GetAsync<List<string>>("/Products/GetAllCategory")));
        }

        public ICommand AddProduct => new AsyncCommand(async () => {

            FillNoInputDataProduct();
            string answer = await _clientApiService.PostAsync(new PostEntityModel<Product>()
            {
                PostUrl = $"/Products/AddNewProduct?login={User!.UserLogin}&password={User.UserPassword}",
                Content = Product
            });

            if (answer == string.Empty)
            {
                ErrorMessage = "Вы успешно сохранили новый товар, можете закрыть окно";
            }
            else ErrorMessage = answer;
        });

        public ICommand EditProduct => new AsyncCommand(async () => {
            FillNoInputDataProduct();
            string answer = await _clientApiService.PostAsync(new PostEntityModel<Product>()
            {
                PostUrl = $"/Products/UpdateProduct?login={User!.UserLogin}&password={User.UserPassword}",
                Content = Product
            });

            if (answer == string.Empty)
            {
                ErrorMessage = "Вы успешно сохранили новый товар, можете закрыть окно";
            }
            else ErrorMessage = answer;
        });

        public ICommand DeleteProduct => new AsyncCommand(async () => {
            string answer = await _clientApiService.DeleteProductAsync(
                $"/Products/DeleteProduct?login={User!.UserLogin}&password={User.UserPassword}&articleProduct={product!.ProductArticleNumber}");
            if (answer == string.Empty)
            {
                ErrorMessage = "Вы успешно удалили товар, можете закрыть окно";
            }
            else ErrorMessage = answer;
        });

        private void FillNoInputDataProduct()
        {
            Product ??= new();
            Product.ProductMaxDiscount = 0;
            Product.ProductDiscount = 0;
            Product.ProductManufacture = "Неизвестно";
            Product.ProductCategory = SelectedCategory;
        }
    }
}

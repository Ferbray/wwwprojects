using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wdskills.Core;
using wdskills.Model;
using wdskills.Services;

namespace wdskills.ViewModel.Main
{
    public class ProductViewModel : ObservableObject
    {
        private static Random random = new();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ValidationService _validationService;
        private readonly TransferUserService _userService;
        private readonly TransferProductService _productService;
        private readonly TransferIsAddProductService _isAddProductService;
        private readonly AppDbService _appDbService;

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
            TransferUserService userService,
            TransferProductService productService,
            TransferIsAddProductService isAddProductService,
            ValidationService validationService,
            AppDbService appDbService)
        {
            _userService = userService;
            _productService = productService;
            _isAddProductService = isAddProductService;
            _validationService = validationService;
            _appDbService = appDbService;

            User = _userService.User;
            Product = _productService.Product;
            IsAddProduct = isAddProductService.IsAddProduct;
            IsAdminPanelProduct = !IsAddProduct;
            Categories = _appDbService.GetCategoriesList();
        }

        public ICommand AddProduct => new AsyncCommand(async () => {
            await FillNoInputDataProduct();
            string validationMessage = _validationService.IsValidProductModel(Product); 
            if (validationMessage == string.Empty)
            {
                if(await _appDbService.AddProductAsync(Product!))
                {
                    ErrorMessage = "Вы успешно сохранили новый товар, можете закрыть окно";
                }
                else
                {
                    ErrorMessage = "Ошибка: неизвестная";
                }
            }
            else
            {
                Product!.ProductArticleNumber = "";
                ErrorMessage = validationMessage;
            }
        });

        public ICommand EditProduct => new AsyncCommand(async () => {
            string validationMessage = _validationService.IsValidProductModel(Product);
            if (validationMessage == string.Empty)
            {
                if(await _appDbService.UpdateProductToAsync(Product!))
                {
                    ErrorMessage = "Вы успешно сохранили новый товар, можете закрыть окно";
                }
                else
                {
                    ErrorMessage = "Ошибка: похоже товар уже удалили или изменили";
                }
            }
            else
            {
                ErrorMessage = validationMessage;
            }
        });

        public ICommand DeleteProduct => new AsyncCommand(async () => {
            if (Product is not null && !string.IsNullOrEmpty(Product.ProductArticleNumber))
            {
                if (await _appDbService.DeleteProductAsync(Product!.ProductArticleNumber))
                {
                    ErrorMessage = "Вы успешно удалили товар, можете закрыть окно";
                }
                else
                {
                    ErrorMessage = "Ошибка: похоже товар уже удалили или изменили";
                }
            }
            else
            {
                ErrorMessage = "Ошибка: transfer не правильно передал данные";
            }
        });

        private async Task FillNoInputDataProduct()
        {
            if(Product is not null)
            {
                string articleNumber;
                Product? findIdenticalProduct;
                do
                {
                    articleNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                    findIdenticalProduct = await _appDbService.FindProductToArticleAsync(articleNumber);
                }
                while (findIdenticalProduct is not null);
                Product.ProductArticleNumber = articleNumber;
                Product.ProductMaxDiscount = 0;
                Product.ProductDiscount = 0;
                Product.ProductManufacture = "Неизвестно";
                Product.ProductCategory = SelectedCategory;
            }
        }
    }
}

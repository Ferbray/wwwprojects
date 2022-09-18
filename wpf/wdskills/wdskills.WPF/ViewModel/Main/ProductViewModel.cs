using DevExpress.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private string? productImageName;
        private string? productImagePath;
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
        public string? ProductImageName
        {
            get => productImageName;
            set
            {
                productImageName = value;
                OnPropertyChanged("ProductImageName");
            }
        }
        public string? ProductImagePath
        {
            get => productImagePath;
            set
            {
                productImagePath = value;
                OnPropertyChanged("ProductImagePath");
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
            Product = _productService.Product ?? new();
            ProductImageName = (string.IsNullOrEmpty(Product!.ProductImage)) ? "picture.png"  : Product.ProductImage;
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

        public ICommand LoadImage => new AsyncCommand(async () => {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ProductImagePath = openFileDialog.FileName;
                ProductImageName = ProductImagePath.Split('\\')[^1];
                FileStream? fileStream = null!;
                try
                {
                    fileStream = new FileStream(ProductImagePath, FileMode.Open);
                    byte[] buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, buffer.Length);

                    fileStream.Close();
                    fileStream.Dispose();
                    var filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0-windows", "");
                    fileStream = new FileStream($"{filePath}/wwwroot/{ProductImageName}", FileMode.OpenOrCreate);
                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    var message = ex.ToString();

                }
                finally
                {
                    fileStream?.Close();
                }
            }
        });

        private void FillNoInputDataProduct()
        {
            Product ??= new();
            Product.ProductImage = ProductImageName;
            Product.ProductMaxDiscount = 0;
            Product.ProductDiscount = 0;
            Product.ProductManufacture = "Неизвестно";
            Product.ProductCategory = SelectedCategory;
        }
    }
}

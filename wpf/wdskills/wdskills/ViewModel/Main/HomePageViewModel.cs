using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using wdskills.Core;
using wdskills.Model;
using wdskills.Services;
using wdskills.View.Main;
using wdskills.View.StartUp;

namespace wdskills.ViewModel
{
    public class HomePageViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly ValidationService _validationService;
        private readonly TransferUserService _userService;
        private readonly TransferProductService _productService;
        private readonly TransferIsAddProductService _isAddProductService;
        private readonly ResizeMainWindowService _resizeMainWindowService;
        private readonly AppDbService _appDbService;

        private User? user;
        private string? userFIO;
        private bool isUserAdmin;
        private string? searchInput = "";
        private string? errorMessage;
        private int typeSorted = 0;

        private ObservableCollection<Product>? foundSearchProducts;
        private ObservableCollection<Product>? products;
        private ObservableCollection<string>? providers;
        private string? selectedProvider = "Все поставщики";
        private Product? selectedProduct = null;
        public ObservableCollection<Product>? FoundSearchProducts
        {
            get => foundSearchProducts;
            set
            {
                foundSearchProducts = value;
                OnPropertyChanged("FoundSearchProducts");
            }
        }
        public ObservableCollection<Product>? Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged("Products");
            }
        }
        public ObservableCollection<string>? Providers
        {
            get => providers;
            set
            {
                providers = value;
                OnPropertyChanged("Providers");
            }
        }
        public string? UserFIO => userFIO;
        public bool IsUserAdmin => isUserAdmin;

        public User? User {
            get => user;
            set {
                user = value;
                userFIO = value?.ToString();
                isUserAdmin = (value?.RoleId == 3);
                OnPropertyChanged("UserFIO");
                OnPropertyChanged("IsUserAdmin");
                OnPropertyChanged("User");
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

        public string? SearchInput
        {
            get => searchInput;
            set
            {
                searchInput = value;
                typeSorted = 0;
                SelectedProvider = "Все поставщики";
                SortedSearchInputProducts();
                OnPropertyChanged("SearchInput");
            }
        }

        public string? SelectedProvider
        {
            get => selectedProvider;
            set
            {
                selectedProvider = value;
                SortedSelectedProviders();
                OnPropertyChanged("SelectedProvider");
            }
        }

        public Product? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                if(IsUserAdmin && selectedProduct is not null)
                {
                    _productService.ChangeProduct(value);
                    _isAddProductService.ChangeIsAddProduct(false);
                    ProductWindow productWindow = new();
                    productWindow.ShowDialog();
                    selectedProduct = null;
                    Providers = _appDbService.GetProviderList();
                    SortedByCost(true);
                }
                OnPropertyChanged("SelectedProduct");
            }
        }

        public HomePageViewModel(
            PageService pageService, 
            TransferUserService userService,
            TransferProductService productService,
            TransferIsAddProductService isAddProductService,
            ResizeMainWindowService resizeMainWindowService,
            ValidationService validationService,
            AppDbService appDbService)
        {
            _userService = userService;
            _productService = productService;
            _pageService = pageService;
            _validationService = validationService;
            _appDbService = appDbService;
            _isAddProductService = isAddProductService;
            _resizeMainWindowService = resizeMainWindowService;

            User = _userService.User;
            Providers = _appDbService.GetProviderList();
            Products = _appDbService.GetProductsList();
            FoundSearchProducts = Products;
            ErrorMessage = "";
        }

        public ICommand Logout => new DelegateCommand(() => {
            _userService.ChangeUser(new User());
            _resizeMainWindowService.ChangeSizeMainWindow(new(450, 450, 450, 450));
            _pageService.ChangePage(new AuthorizationPage());
        });

        public ICommand SortedProducts => new DelegateCommand(() => {
            SortedByCost(false);
        });

        public ICommand AddProduct => new DelegateCommand(() =>
        {
            _productService.ChangeProduct(new());
            _isAddProductService.ChangeIsAddProduct(true);
            ProductWindow productWindow = new();
            productWindow.ShowDialog();
            SelectedProduct = null;
            Providers = _appDbService.GetProviderList();
            SortedByCost(true);
        });

        private void SortedSearchInputProducts()
        {
            searchInput ??= "";
            Products = _appDbService.GetProductsList();
            if (Products is not null)
            {
                FoundSearchProducts = new(Products.Where(p => _validationService.IsContainedWordInText(searchInput, p.ProductName!)));
            }
        }

        private void SortedSelectedProviders()
        {
            selectedProvider ??= "Все поставщики";
            SortedSearchInputProducts();
            if (FoundSearchProducts is not null && selectedProvider != "Все поставщики")
            {
                FoundSearchProducts = new(FoundSearchProducts.Where(p => p.ProductProvider == selectedProvider));
            }
        }

        private void SortedByCost(bool stetSorted)
        {
            searchInput ??= "";
            SortedSelectedProviders();
            if (FoundSearchProducts != null && Products != null)
            {
                switch (typeSorted)
                {
                    case 2:
                        FoundSearchProducts = new(FoundSearchProducts.OrderBy(p => p.ProductCost));
                        typeSorted = (stetSorted) ? typeSorted : 0;
                        break;
                    case 1:
                        FoundSearchProducts = new(FoundSearchProducts.OrderByDescending(p => p.ProductCost));
                        typeSorted = (stetSorted) ? typeSorted: typeSorted + 1;
                        break;
                    case 0:
                        FoundSearchProducts = new(FoundSearchProducts.Where(p => _validationService.IsContainedWordInText(searchInput, p.ProductName!)));
                        typeSorted = (stetSorted) ? typeSorted : typeSorted + 1;
                        break;
                }
            }
        }
    }
}

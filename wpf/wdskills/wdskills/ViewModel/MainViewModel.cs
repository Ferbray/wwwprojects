using System.Windows;
using System.Windows.Controls;
using wdskills.Core;
using wdskills.Model;
using wdskills.Services;
using wdskills.View.StartUp;

namespace wdskills.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly TransferUserService _userService;
        private readonly TransferProductService _productService;
        private readonly TransferIsAddProductService _isAddProductService;

        private Page? pageSource;
        private User? user;
        private Product? product;
        private bool isAddProduct;
        public Page? PageSource
        {
            get => pageSource;
            set {
                pageSource = value;
                OnPropertyChanged("PageSource");
            }
        }
        public User? User
        {
            get => user;
            set
            {
                user = value;
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
        public bool IsAddProduct
        {
            get => isAddProduct;
            set
            {
                isAddProduct = value;
                OnPropertyChanged("IsAddProduct");
            }
        }

        public MainViewModel(
            PageService pageService,
            TransferUserService userService,
            TransferProductService productService, 
            TransferIsAddProductService isAddProductService)
        {
            _pageService = pageService;
            _userService = userService;
            _productService = productService;
            _isAddProductService = isAddProductService;

            _pageService.OnPageChanged += page => PageSource = page;
            _userService.OnUserChanged += user => User = user;
            _productService.OnProductChanged += product => Product = product;
            _isAddProductService.OnIsAddProductChanged += isAddProduct => IsAddProduct = isAddProduct;

            _pageService.ChangePage(new AuthorizationPage());
        }
    }
}

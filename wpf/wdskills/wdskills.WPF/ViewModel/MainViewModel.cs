using System.Windows.Controls;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;
using wdskills.WPF.Services;
using wdskills.WPF.View.StartUp;

namespace wdskills.WPF.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly TransferUserService _userService;
        private readonly TransferProductService _productService;
        private readonly TransferIsAddProductService _isAddProductService;
        private readonly ResizeMainWindowService _resizeMainWindow;

        private Page? pageSource;
        private User? user;
        private Product? product;
        private SizeMainWindow? sizeMainWindow;
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
        public SizeMainWindow? SizeMainWindow
        {
            get => sizeMainWindow;
            set
            {
                sizeMainWindow = value;
                OnPropertyChanged("SizeMainWindow");
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
            TransferIsAddProductService isAddProductService,
            ResizeMainWindowService resizeMainWindow)
        {
            _pageService = pageService;
            _userService = userService;
            _productService = productService;
            _isAddProductService = isAddProductService;
            _resizeMainWindow = resizeMainWindow;   

            _pageService.OnPageChanged += page => PageSource = page;
            _userService.OnUserChanged += user => User = user;
            _productService.OnProductChanged += product => Product = product;
            _isAddProductService.OnIsAddProductChanged += isAddProduct => IsAddProduct = isAddProduct;
            _resizeMainWindow.OnResizeWindowChanged += size => SizeMainWindow = size;

            _resizeMainWindow.ChangeSizeMainWindow(new SizeMainWindow(450, 450, 450, 450));
            _pageService.ChangePage(new AuthorizationPage());
        }
    }
}

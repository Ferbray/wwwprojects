using Microsoft.Extensions.DependencyInjection;
using wdskills.WebClient.Service;
using wdskills.WPF.Core;
using wdskills.WPF.Services;
using wdskills.WPF.ViewModel;
using wdskills.WPF.ViewModel.Main;
using wdskills.WPF.ViewModel.StartUp;

namespace wdskills.WPF
{
    public class ViewModelLocator
    {
        private static ServiceProvider? _provider;
        public MainViewModel MainViewModel => _provider!.GetRequiredService<MainViewModel>();
        public AuthViewModel AuthViewModel => _provider!.GetRequiredService<AuthViewModel>();
        public RegViewModel RegViewModel => _provider!.GetRequiredService<RegViewModel>();
        public HomePageViewModel HomePageViewModel => _provider!.GetRequiredService<HomePageViewModel>();
        public ProductViewModel ProductViewModel => _provider!.GetRequiredService<ProductViewModel>();
        public static void Init() {
            var services = new ServiceCollection();

            services.AddTransient<MainViewModel>();
            services.AddTransient<RegViewModel>();
            services.AddTransient<HomePageViewModel>();
            services.AddTransient<ProductViewModel>();

            services.AddScoped<AuthViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<CaptchaService>();
            services.AddSingleton<ClientApiService>();
            services.AddSingleton<TransferUserService>();
            services.AddSingleton<TransferProductService>();
            services.AddSingleton<TransferIsAddProductService>();
            services.AddSingleton<ResizeMainWindowService>();
            services.AddSingleton<ClientValidationService>();

            _provider = services.BuildServiceProvider();

            foreach(var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}

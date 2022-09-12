using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.Data;
using wdskills.Services;
using wdskills.ViewModel;
using wdskills.ViewModel.Main;
using wdskills.ViewModel.StartUp;

namespace wdskills
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

            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(@"Data Source=DESKTOP-E44T45H\FERBRAY;Initial Catalog=Trade;Integrated Security=True;"));

            services.AddTransient<MainViewModel>();
            services.AddTransient<RegViewModel>();
            services.AddTransient<AppDbService>();
            services.AddTransient<HomePageViewModel>();
            services.AddTransient<ProductViewModel>();

            services.AddScoped<AuthViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<CaptchaService>();
            services.AddSingleton<TransferUserService>();
            services.AddSingleton<TransferProductService>();
            services.AddSingleton<TransferIsAddProductService>();
            services.AddSingleton<ResizeMainWindowService>();
            services.AddSingleton<ValidationService>();

            _provider = services.BuildServiceProvider();

            foreach(var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}

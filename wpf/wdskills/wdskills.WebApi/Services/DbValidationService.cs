using wdskills.WebServer.Model;
using wdskills.WebServer;

namespace wdskills.WebApi.Service
{
    public class DbValidationService
    {
        private readonly AppDbService _appDbService;

        public DbValidationService(AppDbService appDbService)
        {
            _appDbService = appDbService;
        }
        public static bool IsValidUserModel(User? user)
        {
            return (
                user is not null &&
                !string.IsNullOrEmpty(user.UserLogin) &&
                !string.IsNullOrEmpty(user.UserName) &&
                !string.IsNullOrEmpty(user.UserPassword) &&
                !string.IsNullOrEmpty(user.UserPatronymic) &&
                !string.IsNullOrEmpty(user.UserSurname) &&
                user.RoleId != 0);
        }

        public async Task<string> IsValidRegModelAsync(User? user)
        {
            if (IsValidUserModel(user))
            {
                if (await _appDbService.FindUserToLoginAsync(user!.UserLogin) != null) return "Ошибка: такой пользователь уже существует";
                return string.Empty;
            }
            return "Ошибка: некорректные данные";
        }

        public async Task<string> IsValidAuthModelAsync(string? login, string? password, bool isActionsForAdmin = false)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return "Ошибка: поле пароля или логина пустое";
            }
            User user = new()
            {
                UserLogin = login,
                UserPassword = password
            };
            if (!await _appDbService.CanUserLogInAsync(user))
            {
                return "Ошибка: неверный пароль или логин";
            }
            if(isActionsForAdmin == true)
            {
                User? findUser = await _appDbService.FindUserToLoginAsync(login);
                if (findUser!.RoleId != 3) return "Ошибка: пользователь не админ";
            }
            return string.Empty;
        }
        public string IsValidProductModel(Product? product)
        {
            if (product is null) return "Ошибка: пустые данные";
            if (string.IsNullOrEmpty(product.ProductArticleNumber)) return "Ошибка: артикл продукта пустое";
            if (string.IsNullOrEmpty(product.ProductName)) return "Ошибка: название продукта пустое";
            if (product.ProductCost < 0M) return "Ошибка: стоимость не может быть отрицательной";
            if (product.ProductMaxDiscount < 0) return "Ошибка: максимальная скидка не может быть отрицательной";
            if (string.IsNullOrEmpty(product.ProductUnits)) return "Ошибка: поле ед. измерения пустое";
            if (string.IsNullOrEmpty(product.ProductManufacture)) return "Ошибка: поле производителя пустое";
            if (string.IsNullOrEmpty(product.ProductProvider)) return "Ошибка: поле поставщика пустое";
            if (string.IsNullOrEmpty(product.ProductCategory)) return "Ошибка: поле категории пустое";
            if (product.ProductDiscount < 0) return "Ошибка: поле скидки пустое";
            if (product.ProductStockRoom < 0) return "Ошибка: поле количества на складе пустое";
            if (string.IsNullOrEmpty(product.ProductDescription)) return "Ошибка: поле описание пустое";
            if (string.IsNullOrEmpty(product.ProductImage)) return "Ошибка: неверное фото продукта";
            return string.Empty;
        }

        public async Task<string> IsValidAddNewProductAsync(string? password, string? login, Product product)
        {
            string answerValidUser = await IsValidAuthModelAsync(password, login, true);
            string answerValidProduct = IsValidProductModel(product);
            return (answerValidUser == string.Empty) ? answerValidProduct : answerValidUser;
        }

        public async Task<string> IsValidUpdateProductAsync(string? password, string? login, Product product)
        {
            string answerValidUser = await IsValidAuthModelAsync(password, login, true);
            string answerValidProduct = IsValidProductModel(product);
            if (_appDbService.FindProductToArticleAsync(product.ProductArticleNumber) == null) return "Ошибка: продукт не найден";
            return (answerValidUser == string.Empty) ? answerValidProduct : answerValidUser;
        }

        public async Task<string> IsValidDeleteProductAsync(string? password, string? login, string? articleProduct)
        {
            string answerValidUser = await IsValidAuthModelAsync(password, login, true);
            if (_appDbService.FindProductToArticleAsync(articleProduct) == null) return "Ошибка: продукт не найден";
            return answerValidUser;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.Data;
using wdskills.Model;

namespace wdskills.Services
{
    public class ValidationService
    {
        private readonly AppDbService _appDbService;
        public ValidationService(AppDbService appDbService)
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

        public string IsValidRegModel(User? user)
        {
            if(IsValidUserModel(user))
            {
                return string.Empty;
            }
            return "Ошибка: некорректные данные";
        }

        public async Task<string> IsValidAuthModelAsync(string? login, string? password)
        {
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return "Ошибка: поле пароля или логина пустое";
            }
            User user = new()
            {
                UserLogin = login,
                UserPassword = password
            };
            if (!await _appDbService.CanUserLogInAsync(user)){
                return "Ошибка: неверный пароль или логин";
            }
            return string.Empty;
        }

        public bool IsContainedWordInText(string word, string text)
        {
            return text.ToLower().IndexOf(word.ToLower()) != -1;
        }

        public string IsValidProductModel(Product? product)
        {
            if (product is null) return "Ошибка: админ что-то напутал";
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

        public string IsValidCaptcha(
            bool sendCaptcha, 
            DateTime endBanes, 
            string? inputCaptcha, 
            string? keyCaptcha)
        {
            if(sendCaptcha == true)
            {
                if(endBanes > DateTime.Now) return "Ошибка: подождите 10 секунд";
                if (inputCaptcha != keyCaptcha) return "Ошибка: каптчи не сходятся";
            }
            return string.Empty;
        }
    }
}

using System;

namespace wdskills.WPF.Core
{
    public class ClientValidationService
    {
        public bool IsContainedWordInText(string word, string text)
        {
            return text.ToLower().IndexOf(word.ToLower()) != -1;
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
                if (inputCaptcha?.ToLower() != keyCaptcha?.ToLower()) return "Ошибка: каптчи не сходятся";
            }
            return string.Empty;
        }
    }
}

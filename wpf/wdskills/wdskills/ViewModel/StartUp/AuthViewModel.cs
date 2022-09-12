using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using wdskills.Model;
using wdskills.Services;
using System.Security;
using System.Windows.Data;
using System.Windows.Controls;
using wdskills.Core;
using wdskills.View.Main;
using wdskills.View.StartUp;

namespace wdskills.ViewModel.StartUp
{
    public class AuthViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly TransferUserService _userService;
        private readonly ValidationService _validationService;
        private readonly ResizeMainWindowService _resizeMainWindowService;
        private readonly CaptchaService _captchaService;
        private readonly AppDbService _appDbService;

        public ObservableCollection<Product>? Products { get; set; }

        public string? Login { get; set; }
        public string? Password { get; set; }

        private string? errorMessage;
        private bool sendCaptcha;
        private string? inputCaptcha;
        private string? keyCaptcha;
        public string? imageCaptcha = "captcha.jpg";
        private DateTime endBanes = new();

        public string? ImageCaptcha
        {
            get => imageCaptcha;
            set
            {
                imageCaptcha = value;
                OnPropertyChanged("ImageCaptcha");
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

        public bool SendCaptcha
        {
            get => sendCaptcha;
            set
            {
                sendCaptcha = value;
                OnPropertyChanged("SendCaptcha");
            }
        }

        public string? InputCaptcha
        {
            get => inputCaptcha;
            set
            {
                inputCaptcha = value;
                OnPropertyChanged("InputCaptcha");
            }
        }

        public AuthViewModel(
            PageService pageService,
            TransferUserService userService,
            ValidationService validationService,
            CaptchaService captchaService,
            ResizeMainWindowService resizeMainWindowService,
            AppDbService appDbService)
        {
            _pageService = pageService;
            _userService = userService;
            _validationService = validationService;
            _resizeMainWindowService = resizeMainWindowService;
            _captchaService = captchaService;
            _appDbService = appDbService;
        }

        public ICommand JoinAccount => new AsyncCommand(async () => {
            string answerCaptcha = _validationService.IsValidCaptcha(SendCaptcha, endBanes, InputCaptcha, keyCaptcha);
            ErrorMessage = answerCaptcha;
            if (answerCaptcha == string.Empty)
            {
                endBanes = DateTime.Now;
                string answerAuth = await _validationService.IsValidAuthModelAsync(Login, Password);
                ErrorMessage = answerAuth;
                if (string.Empty == answerAuth)
                {
                    User? user = await _appDbService.FindUserToLoginAsync(Login);
                    ResetInputsPage();
                    _resizeMainWindowService.ChangeSizeMainWindow(new(8000, 8000, 1000, 500));
                    _userService.ChangeUser(user);
                    _pageService.ChangePage(new HomePage());
                    ErrorMessage = "";
                    return;
                }
            }
            _resizeMainWindowService.ChangeSizeMainWindow(new(450, 600, 450, 600));
            InputCaptcha = "";
            keyCaptcha = await _captchaService.CreateCaptcha(300, 100);
            ImageCaptcha = "captcha.jpg";
            if (answerCaptcha == "Ошибка: подождите 10 секунд") return;
            if(SendCaptcha == true) endBanes = (DateTime.Now).AddSeconds(10);
            SendCaptcha = true;
        });

        public ICommand RedirectToRegister => new DelegateCommand(() => {
            _pageService.ChangePage(new RegistartionPage());
        });

        private void ResetInputsPage()
        {
            SendCaptcha = false;
            Login = "";
            Password = "";
            InputCaptcha = "";
        }
    }
}

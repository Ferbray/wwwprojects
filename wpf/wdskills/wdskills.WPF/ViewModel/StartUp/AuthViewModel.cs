using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using wdskills.WebClient.Service;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;
using wdskills.WPF.Services;
using wdskills.WPF.View.Main;
using wdskills.WPF.View.StartUp;

namespace wdskills.WPF.ViewModel.StartUp
{
    public class AuthViewModel : ObservableObject
    {
        private readonly ClientApiService _clientApiService;
        private readonly PageService _pageService;
        private readonly TransferUserService _userService;
        private readonly ClientValidationService _validationService;
        private readonly ResizeMainWindowService _resizeMainWindowService;
        private readonly CaptchaService _captchaService;

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
            ClientApiService clientApiService,
            PageService pageService,
            TransferUserService userService,
            ClientValidationService validationService,
            CaptchaService captchaService,
            ResizeMainWindowService resizeMainWindowService)
        {
            _pageService = pageService;
            _userService = userService;
            _validationService = validationService;
            _resizeMainWindowService = resizeMainWindowService;
            _captchaService = captchaService;
            _clientApiService = clientApiService;
        }

        public ICommand JoinAccount => new AsyncCommand(async () => {
            string answerCaptcha = _validationService.IsValidCaptcha(
                SendCaptcha, endBanes, InputCaptcha, keyCaptcha);
            ErrorMessage = answerCaptcha;

            if (answerCaptcha == string.Empty)
            {
                endBanes = DateTime.Now;
                User? user = await _clientApiService.GetAsync<User?>(
                    $"/Users/GetUser?login={Login}&password={Password}");
                string? answerAuth = _clientApiService.ErrorMessage;
                ErrorMessage = answerAuth;

                if (string.Empty == answerAuth)
                {
                    ResetInputsPage();
                    _resizeMainWindowService.ChangeSizeMainWindow(new(8000, 8000, 1000, 500));
                    _userService.ChangeUser(user);
                    _pageService.ChangePage(new HomePage());
                    return;
                }
            }
            await OpenCaptcha();
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

        private async Task OpenCaptcha()
        {
            _resizeMainWindowService.ChangeSizeMainWindow(new(450, 600, 450, 600));
            InputCaptcha = "";
            keyCaptcha = await _captchaService.CreateCaptcha(300, 100);
            ImageCaptcha = "captcha.jpg";
        }
    }
}

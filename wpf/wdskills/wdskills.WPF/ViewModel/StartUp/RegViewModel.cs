using DevExpress.Mvvm;
using System.Windows.Input;
using System.Net.Http;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;
using wdskills.WPF.Services;
using wdskills.WPF.View.StartUp;
using System.Configuration;
using System.Text.Json;
using System.Text;
using wdskills.WebClient.Service;
using wdskills.WebClient.Model;

namespace wdskills.WPF.ViewModel.StartUp
{
    public class RegViewModel : ObservableObject
    {
        private readonly ClientApiService _clientApiService;
        private readonly PageService _pageService;
        private readonly ClientValidationService _validationService;
        private readonly ResizeMainWindowService _resizeMainWindowService;

        private string? errorMessage;

        private readonly SizeMainWindow? oldSize;

        public User User { private get; set; } = new User();
        public string? ConfirmPassword { get; set; }

        public string? ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public RegViewModel(
            ClientApiService clientApiService,
            PageService pageService, 
            ClientValidationService validationService,
            ResizeMainWindowService resizeMainWindowService)
        {
            _pageService = pageService;
            _clientApiService = clientApiService;
            _validationService = validationService; 
            _resizeMainWindowService = resizeMainWindowService;

            oldSize = _resizeMainWindowService.SizeWindow;
            _resizeMainWindowService.ChangeSizeMainWindow(new(450, 750, 450, 750));
        }

        public ICommand RegisterAccount => new AsyncCommand(async () => {
            if (User.UserPassword == ConfirmPassword)
            {
                string? answer = await _clientApiService.PostAsync(
                    new PostEntityModel<User>()
                    {
                        PostUrl = "/Users/CreateUser",
                        Content = User
                    });
                if (answer == string.Empty)
                {
                    _resizeMainWindowService.ChangeSizeMainWindow(oldSize);
                    _pageService.ChangePage(new AuthorizationPage());
                }
                ErrorMessage = answer;
            }
            else ErrorMessage = "Ошибка: пароль подтверждения не сходится";
        });

        public ICommand RedirectToAuthorization => new DelegateCommand(() => {
            _resizeMainWindowService.ChangeSizeMainWindow(oldSize);
            _pageService.ChangePage(new AuthorizationPage());
        });
    }
}

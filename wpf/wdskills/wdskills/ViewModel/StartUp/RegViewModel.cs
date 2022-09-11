using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wdskills.Core;
using wdskills.Model;
using wdskills.Services;
using wdskills.View.StartUp;

namespace wdskills.ViewModel.StartUp
{
    public class RegViewModel : ObservableObject
    {
        private readonly PageService _pageService;
        private readonly ValidationService _validationService;
        private readonly AppDbService _appDbService;
        private string? errorMessage;

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
            PageService pageService, 
            ValidationService validationService,
            AppDbService appDbService)
        {
            _pageService = pageService;
            _validationService = validationService; 
            _appDbService = appDbService;
        }

        public ICommand RegisterAccount => new AsyncCommand(async () => {
            ErrorMessage = "Ошибка: пароль подтверждения не сходится";
            if (User.UserPassword == ConfirmPassword) {
                User.RoleId = 1;
                string answer = _validationService.IsValidRegModel(User);
                ErrorMessage = answer;
                if (string.Empty == answer)
                {
                    if (await _appDbService.AddUserAsync(User))
                    {
                        _pageService.ChangePage(new AuthorizationPage());
                    }
                    ErrorMessage = "Ошибка: такой пользователь уже существует";
                }
            }
        });

        public ICommand RedirectToAuthorization => new DelegateCommand(() => {
            _pageService.ChangePage(new AuthorizationPage());
        });
    }
}

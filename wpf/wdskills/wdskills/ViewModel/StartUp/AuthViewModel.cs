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
        private readonly AppDbService _appDbService;

        public ObservableCollection<Product>? Products { get; set; }

        public string? Login { get; set; }
        public string? Password { get; set; }

        private string? errorMessage;

        public string? ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public AuthViewModel(
            PageService pageService,
            TransferUserService userService,
            ValidationService validationService,
            AppDbService appDbService)
        {
            _pageService = pageService;
            _userService = userService;
            _validationService = validationService;
            _appDbService = appDbService;
        }

        public ICommand JoinAccount => new AsyncCommand(async () => {
            string answer = await _validationService.IsValidAuthModelAsync(Login, Password);
            ErrorMessage = answer;
            if (string.Empty == answer)
            {
                User? user = await _appDbService.FindUserToLoginAsync(Login);
                _userService.ChangeUser(user);
                _pageService.ChangePage(new HomePage());
                ErrorMessage = "GG";
            }
        });

        public ICommand RedirectToRegister => new DelegateCommand(() => {
            _pageService.ChangePage(new RegistartionPage());
        });
    }
}

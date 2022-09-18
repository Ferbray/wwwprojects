using System;
using wdskills.WebServer.Model;
using wdskills.WPF.Core;

namespace wdskills.WPF.Services
{
    public class TransferUserService : ObservableObject
    {
        public User? User { get; private set; }
        public event Action<User>? OnUserChanged;
        public void ChangeUser(User? user) {
            User = user;
            OnUserChanged?.Invoke(user ?? new());
        }
    }
}

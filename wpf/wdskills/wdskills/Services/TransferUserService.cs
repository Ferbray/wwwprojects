using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wdskills.Core;
using wdskills.Model;

namespace wdskills.Services
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

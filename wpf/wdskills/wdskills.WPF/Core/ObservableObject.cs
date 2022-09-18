using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace wdskills.WPF.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string pop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pop));
        }
    }
}

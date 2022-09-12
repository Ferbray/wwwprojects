
using System.Windows;
using wdskills.Data;
using wdskills.Services;
using wdskills.ViewModel;

namespace wdskills
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
        }
    }
}

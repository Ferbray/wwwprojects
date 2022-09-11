using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wdskills.View.StartUp
{
    /// <summary>
    /// Логика взаимодействия для RegistartionPage.xaml
    /// </summary>
    public partial class RegistartionPage : Page
    {
        public RegistartionPage()
        {
            InitializeComponent();
            this.Loaded += delegate
            {
                Window window = Window.GetWindow(this);
                window.SetBinding(Window.MinHeightProperty, new Binding() { Source = this.MinHeight + 50 });
                window.SetBinding(Window.MinWidthProperty, new Binding() { Source = this.MinWidth });
                window.SetBinding(Window.MaxHeightProperty, new Binding() { Source = this.MaxHeight + 50 });
                window.SetBinding(Window.MaxWidthProperty, new Binding() { Source = this.MaxWidth });
            };
        }
    }
}

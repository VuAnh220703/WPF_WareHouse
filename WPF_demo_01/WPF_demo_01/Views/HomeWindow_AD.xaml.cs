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
using System.Windows.Shapes;
using WPF_demo_01.ViewModels;

namespace WPF_demo_01.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow_AD.xaml
    /// </summary>
    public partial class HomeWindow_AD : Window
    {
        public HomeViewModel_Admin homeViewModel_QL;
        public HomeWindow_AD()
        {
            InitializeComponent();

            Application.Current.MainWindow = this;

            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            homeViewModel_QL = new HomeViewModel_Admin();
            this.DataContext = homeViewModel_QL;
        }
    }
}

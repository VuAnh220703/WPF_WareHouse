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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow_QL : Window
    {
        public HomeViewModel homeViewModel;
        public HomeWindow_QL()
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;

            homeViewModel = new HomeViewModel();
            this.DataContext = homeViewModel;
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Trang chu");
        }

        private void btnBanHangClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bán hàng");

        }

        private void btnKhoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kho");

        }
    }
}

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
using System.Diagnostics;


namespace WPF_demo_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // function bắt sự kiện Got focus điền thông tin
        private void fncTxt_GotFocus(object sender, RoutedEventArgs e)
        {
          if(sender.GetType().Name.ToString() == "PasswordBox")
            {
                PasswordBox password = sender as PasswordBox;
                password.Clear();
            }
          else if (sender.GetType().Name.ToString() == "TextBox")
            {
                TextBox tbIdUser = sender as TextBox;
                tbIdUser.Clear();
                tbIdUser.Foreground = Brushes.Black;
            }
        }

        //Bắt sự kiện rời focus id nhân viên
        private void txtIdUser_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbIdUser = sender as TextBox;
            tbIdUser.Foreground = fncChangeForegroundInput(tbIdUser);
            if (string.IsNullOrEmpty(tbIdUser.Text))
            {
                tbIdUser.Text = "000-NV-000";
            }
        }

        //Kiểm tra giá trị của textbox để chỉnh màu chữ
        private SolidColorBrush fncChangeForegroundInput(TextBox tbIpt)
        {
            if (string.IsNullOrEmpty(tbIpt.Text))
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#727782"));
            }
            return (SolidColorBrush)(new BrushConverter().ConvertFrom("#171C1F"));
            
        }

        // function bắt sự kiện di chuyển chuột
        private void fncButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Border bdBtn = new Border();
            if(btn.Name == "btnLogin")
            {
                 bdBtn = (Border)btnLogin.Template.FindName("bdBtnLogin", btnLogin);
            }
            else if (btn.Name == "btnCodeQR")
            {
                bdBtn = (Border)btnCodeQR.Template.FindName("bdCodeQR", btnCodeQR);
               
            }
             bdBtn.Background = Brushes.LightBlue;
        }

        // function bắt sự kiện di chuyển chuột ra khỏi nút button
        private void fncButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Border borderBtn = new Border();
            if(btn.Name == "btnLogin")
            {
                 borderBtn = (Border)btnLogin.Template.FindName("bdBtnLogin", btnLogin);
                borderBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#004277"));
            }
            else if (btn.Name == "btnCodeQR")
            {
                borderBtn = (Border)btnCodeQR.Template.FindName("bdCodeQR", btnCodeQR);
                borderBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F0F4F8"));
            }
        }
    }
}

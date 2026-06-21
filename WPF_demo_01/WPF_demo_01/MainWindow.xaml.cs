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
using BCrypt.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;
using WPF_demo_01.Helper;
using WPF_demo_01.Services;
using WPF_demo_01.Views;
using WPF_demo_01.Models;
using WPF_demo_01.Class;
using System.Text.Json;

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
            if (sender.GetType().Name.ToString() == "PasswordBox")
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
            //gọi hàm thay đổi màu sắc
            tbIdUser.Foreground = fncChangeForegroundInput(tbIdUser);
            txtErrIdUser.Text = RegexHelper.isValidIdUser(txtIdUser.Text);
           
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
            if (btn.Name == "btnLogin")
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
            if (btn.Name == "btnLogin")
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

        // sự kiến nhấn chuột vào nút đăng nhập
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SupabaseService supbaseservice = new SupabaseService();
            string result = null;

            // 1. Bật hiệu ứng chờ trên giao diện trước khi gọi API
            LoadingOverlay.Visibility = Visibility.Visible;

            try
            {
                // Kiểm tra điều kiện đầu vào hợp lệ
                if (string.IsNullOrEmpty(RegexHelper.isValidIdUser(txtIdUser.Text)) && !string.IsNullOrEmpty(txtPassword.Password))
                {
                    await Task.Delay(2000); // Giả lập chờ mạng (nếu cần)

                    // Gọi hàm đăng nhập
                    result = await supbaseservice.CheckLoginAsnyc(txtIdUser.Text, txtPassword.Password);

                    // XỬ LÝ KẾT QUẢ NGAY TẠI ĐÂY (Tránh lỗi null nếu để ở finally)
                    if (!string.IsNullOrEmpty(result))
                    {
                        using (JsonDocument jsonResult = JsonDocument.Parse(result))
                        {
                            string status = jsonResult.RootElement.GetProperty("status").GetString();

                            if (status == "success")
                            {
                                // Trích xuất cụm "data" bên trong chuỗi JSON
                                string dataRawJson = jsonResult.RootElement.GetProperty("data").GetRawText();
                                User user = JsonConvert.DeserializeObject<User>(dataRawJson);

                                // Kiểm tra phân quyền chuyển hướng màn hình dựa vào RoleCode hoặc RoleId
                                // Mẹo: Nên kiểm tra bằng cả RoleCode ("ADMIN", "MANAGER") cho chắc chắn không sợ lệch ID
                                if (user.Role.RoleCode == "ADMIN" || user.Role.Id == 3)
                                {
                                    var homeWindow_AD = new HomeWindow_AD();
                                    homeWindow_AD.Show();
                                }
                                else if (user.Role.RoleCode == "MANAGER" || user.Role.Id == 2)
                                {
                                    var homeWindow_QL = new HomeWindow_QL();
                                    homeWindow_QL.Show();
                                }
                                else
                                {
                                    Console.WriteLine($"data: {user.Role.RoleCode}");
                                    MessageBox.Show("Tài khoản của bạn không có quyền truy cập hệ thống!", "Thông báo");
                                    return;
                                }

                                // Đóng MainWindow sau khi đã mở giao diện chính thành công
                                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is MainWindow)?.Close();
                            }
                            else
                            {
                                // Đăng nhập thất bại (Sai tài khoản hoặc mật khẩu)
                                string errorMsg = jsonResult.RootElement.TryGetProperty("message", out var msgElement)
                                                  ? msgElement.GetString()
                                                  : "Thông tin đăng nhập không chính xác.";
                                MessageBox.Show(errorMsg, "Thông báo");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không nhận được phản hồi từ hệ thống. Vui lòng thử lại!", "Lỗi kết nối");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi");
            }
            finally
            {
                // 2. Nguyên lý giao diện: Bất kể thành công hay lỗi, xong việc là phải ẩn Loading đi!
                LoadingOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                txtErrPassword.Text = "Vui lòng nhập mật khẩu";
            }
            else
            {
                txtErrPassword.Text = null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_demo_01.Services;
using WPF_demo_01.Views;

namespace WPF_demo_01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Sử dụng override OnStartup để chạy code khi ứng dụng bắt đầu
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Gọi hàm khởi tạo ở đây
            SupabaseConfig.Initialize();

        }
    }
}

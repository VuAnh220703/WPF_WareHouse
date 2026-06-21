using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WPF_demo_01.ViewModels;

namespace WPF_demo_01.Views.Frame_Admin
{
    /// <summary>
    /// Interaction logic for Add_Supplier_dialog.xaml
    /// </summary>
    public partial class Add_Supplier_dialog : UserControl
    {
        public Add_Supplier_dialog()
        {
            InitializeComponent();
        }

        // 1. Hiệu ứng chuột khi kéo file đi vào vùng nhận file
        private void UploadZone_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        // 2. Xử lý sự kiện khi BUÔNG CHUỘT THẢ FILE
        private void UploadZone_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    ProcessSelectedFile(files[0]); // Lấy tệp đầu tiên
                }
            }
        }

        // 3. Xử lý sự kiện khi CLICK VÀO VÙNG ĐỂ CHỌN FILE
        private void UploadZone_MouseClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Tài liệu hợp đồng (*.pdf;*.docx)|*.pdf;*.docx",
                Title = "Chọn tệp hợp đồng nhà cung cấp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProcessSelectedFile(openFileDialog.FileName);
            }
        }

        // 4. HÀM KIỂM TRA ĐỊNH DẠNG VÀ DUNG LƯỢNG FILE
        private void ProcessSelectedFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string extension = fileInfo.Extension.ToLower();

            // Kiểm tra định dạng đuôi file
            if (extension != ".pdf" && extension != ".docx")
            {
                MessageBox.Show("Hệ thống chỉ hỗ trợ tệp định dạng .PDF hoặc .DOCX!", "Sai định dạng", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra dung lượng (10MB = 10 * 1024 * 1024 Bytes)
            if (fileInfo.Length > 10 * 1024 * 1024)
            {
                MessageBox.Show("Dung lượng tệp vượt quá giới hạn cho phép (Tối đa 10MB)!", "Tệp quá lớn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Hiển thị tên file lên giao diện cho người dùng thấy
            TxtFileName.Text = $"Đã chọn: {fileInfo.Name} ({(fileInfo.Length / 1024.0 / 1024.0):F2} MB)";
            TxtFileName.Visibility = Visibility.Visible;
            TxtUploadPrompt.Text = "Thay đổi tệp khác";

            // TRUYỀN ĐƯỜNG DẪN ĐÃ CHỌN SANG VIEWMODEL
            if (this.DataContext is AdViewmodel_NhaCungCap viewModel)
            {
                viewModel.SelectedContractFilePath = filePath;
            }
        }
    }
}

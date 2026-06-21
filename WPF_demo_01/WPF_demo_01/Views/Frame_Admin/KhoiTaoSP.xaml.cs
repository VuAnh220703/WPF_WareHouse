using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_demo_01.Views.Frame_Admin
{
    /// <summary>
    /// Interaction logic for KhoiTaoSP.xaml
    /// </summary>
    public partial class KhoiTaoSP : UserControl
    {
        // Trong Code-behind của bạn
        public ObservableCollection<string> SelectedImages { get; set; } = new ObservableCollection<string>();
        public KhoiTaoSP()
        {
            InitializeComponent();
          //  ImageList.ItemsSource = SelectedImages;
        }
       /* private void DropArea_DragOver(object sender, DragEventArgs e)
        {
            // Kiểm tra nếu dữ liệu kéo vào là File
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

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Lấy danh sách đường dẫn các file đã thả vào
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    // Kiểm tra xem có phải là định dạng ảnh không
                    string ext = System.IO.Path.GetExtension(file).ToLower();
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".bmp")
                    {
                        if (!SelectedImages.Contains(file))
                        {
                            SelectedImages.Add(file);
                        }
                    }
                }
            }
        } */
    }
}

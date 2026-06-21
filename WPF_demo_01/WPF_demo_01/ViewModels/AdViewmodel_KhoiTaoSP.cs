using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_demo_01.Class;
using System.Drawing;
using System.Windows;

namespace WPF_demo_01.ViewModels
{
  public class AdViewmodel_KhoiTaoSP : INotifyPropertyChanged
    {
        // danh sách hình ảnh
        private ObservableCollection<string> _selectedImages;
        // số lượng hình ảnh
        public int TotalImages => SelectedImages?.Count ?? 0;

        // giá trị bardcode
        private string _barcodeValue;
        public string BarcodeValue
        {
            get => _barcodeValue;
            set { _barcodeValue = value; OnPropertyChanged(nameof(BarcodeValue)); }
        }

        public ObservableCollection<string> SelectedImages
        {
            get => _selectedImages;
            set
            {
                _selectedImages = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectBarcodeImageCommand { get; }

        // 2. Định nghĩa các Command
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public AdViewmodel_KhoiTaoSP()
        {
            SelectedImages = new ObservableCollection<string>();

            // Mỗi khi thêm hoặc xóa ảnh, thông báo cho giao diện cập nhật con số tổng
            SelectedImages.CollectionChanged += (s, e) => {
                OnPropertyChanged(nameof(TotalImages));
            };

            // Khởi tạo logic cho các nút
            AddImageCommand = new RelayCommand(ExecuteAddImage);
            RemoveImageCommand = new RelayCommand(ExecuteRemoveImage);
            SelectBarcodeImageCommand = new RelayCommand(ExecuteSelectBarcode);


        }

        // Logic khi nhấn nút "Thêm hình ảnh sản phẩm"
        private void ExecuteAddImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, // Cho phép chọn nhiều ảnh
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    if (!SelectedImages.Contains(fileName))
                    {
                        SelectedImages.Add(fileName);
                    }
                }
            }
        }

        // Logic khi nhấn nút "X" trên từng tấm hình
        private void ExecuteRemoveImage(object obj)
        {
            if (obj is string imagePath)
            {
                SelectedImages.Remove(imagePath);
            }
        }
        private void ExecuteSelectBarcode(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                // Chỉ cho phép chọn 1 ảnh để đọc mã vạch
                Multiselect = false,
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Sau khi chọn xong, truyền đường dẫn vào hàm đọc barcode
                ReadBarcodeFromImage(openFileDialog.FileName);
            }
        }
        // Hàm xử lý khi người dùng chọn ảnh
        private void ReadBarcodeFromImage(string filePath)
        {
            try
            {
                // 1. Load file ảnh lên
                using (var bitmap = (Bitmap)System.Drawing.Image.FromFile(filePath))
                {
                    // 2. Khởi tạo đầu đọc Barcode
                    var reader = new ZXing.BarcodeReader();

                    // 3. Giải mã
                    var result = reader.Decode(bitmap);

                    if (result != null)
                    {
                        // 4. Gán giá trị đọc được vào TextBox thông qua Property
                        BarcodeValue = result.Text;
                    }
                    else
                    {
                        // Thông báo nếu không tìm thấy mã
                        MessageBox.Show("Không tìm thấy mã barcode trong ảnh!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file: " + ex.Message);
            }
        }

        #region INotifyPropertyChanged Implementation (Thủ công)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

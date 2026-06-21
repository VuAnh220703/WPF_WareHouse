using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_demo_01.Class;
using WPF_demo_01.Models;

namespace WPF_demo_01.ViewModels
{
    public class AdViewmodel_NhaCungCap : INotifyPropertyChanged
    {
        // ------------------------- Các thuộc tính
        private bool _isAddOverlayVisible;
        private string _selectedContractFilePath;
        // lấy dữ liệu loại hợp đồng
        private ObservableCollection<ContractTypes> _filtereTypeContracts = new ObservableCollection<ContractTypes>();


        /// <summary>
        /// public các thuộc tính
        /// </summary>
        public bool IsAddOverlayVisible
        {
            get => _isAddOverlayVisible;
            set
            {
                _isAddOverlayVisible = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ContractTypes> FiltereTypeContracts
        {
            get => _filtereTypeContracts;
            set
            {
                _filtereTypeContracts = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// ------------Các ICOMMAND----------
        /// </summary>
       
        // ICOMMAND đóng Dialog thêm nhà cung cấp
        public ICommand HideAddOverlayVisible { set; get; }
        //ICOMMAND hiển thị Dialog thêm nhà cung cấp
        public ICommand ShowAddOverlayVisible { set; get; }

        public string SelectedContractFilePath
        {
            get => _selectedContractFilePath;
            set
            {
                _selectedContractFilePath = value;
                OnPropertyChanged(nameof(SelectedContractFilePath));
            }
        }

        public AdViewmodel_NhaCungCap()
        {
            // ---------------ĐĂNG KÝ CÁC ICOMMAND-------------
            // icommand ẩn thêm nhà cung cấp
            HideAddOverlayVisible = new RelayCommand((p) => IsAddOverlayVisible = false);
            //icommand hiển thị thêm nhà cung cấp
            ShowAddOverlayVisible = new RelayCommand((p) => IsAddOverlayVisible = true);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_demo_01.Class;

namespace WPF_demo_01.ViewModels
{
   public class HomeViewModel_Admin : INotifyPropertyChanged
    {
        private string _selectedMenu;
        private object _currentChildView;
        public ICommand ChangeMenuCommand { get; set; }

        // Các Command cho Menu
        public RelayCommand ShowKhoiTaoSPCommand { get; set; }
        public RelayCommand ShowNhapHangCommand { get; set; }

        private readonly AdViewmodel_DanhMucSanPham viewmodel_DanhMucSP = new AdViewmodel_DanhMucSanPham();
        private readonly AdViewmodel_KhoiTaoSP viewmodel_KhoiTaoSP = new AdViewmodel_KhoiTaoSP();
        private readonly AdViewmodel_NhaCungCap viewmodel_NhaCungCap = new AdViewmodel_NhaCungCap();
        private readonly AdViewmodel_NhanSu viewmodel_NhanSu = new AdViewmodel_NhanSu();
        private readonly AdViewModel_NhapHang viewmodel_NhapHang = new AdViewModel_NhapHang();
        private readonly AdViewmodel_PhanPhoiSP viewmodel_PhanPhoiSP = new AdViewmodel_PhanPhoiSP();


        public string SelectedMenu
        {
            get => _selectedMenu;
            set
            {
                _selectedMenu = value;
                OnPropertyChanged();
            }
        }

        public object CurrentChildView
        {
            get => _currentChildView;
            set { _currentChildView = value; OnPropertyChanged(); }
        }
        public HomeViewModel_Admin()
        {
            SelectedMenu = "KhoiTaoSP";
            ChangeMenuCommand = new RelayCommand(param =>
            {
                SelectedMenu = param.ToString();
            });

            CurrentChildView = new AdViewmodel_KhoiTaoSP();

            // 2. Logic khi bấm nút Menu
            ChangeMenuCommand = new RelayCommand(param =>
            {
                string menuName = param.ToString();
                SelectedMenu = menuName;

                if (menuName == "KhoiTaoSP")
                {
                    CurrentChildView = viewmodel_KhoiTaoSP;
                }
                else if (menuName == "NhapHang")
                {
                    CurrentChildView = viewmodel_NhapHang;
                }
                else if(menuName == "NhaCungCap")
                {
                    CurrentChildView = viewmodel_NhaCungCap;
                }
                else if (menuName == "DanhMucSP")
                {
                    CurrentChildView = viewmodel_DanhMucSP;
                }
                else if(menuName == "PhanPhoiSP")
                {
                    CurrentChildView = viewmodel_PhanPhoiSP;
                }
                else if(menuName == "NhanSu")
                {
                    CurrentChildView = viewmodel_NhanSu;
                }
            });
        }

        public void ChangeMenu(string menu)
        {
            SelectedMenu = menu;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

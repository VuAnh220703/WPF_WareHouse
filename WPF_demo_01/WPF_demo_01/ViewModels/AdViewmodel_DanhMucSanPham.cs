using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_demo_01.Class;
using WPF_demo_01.Models;
using WPF_demo_01.Services;
using WPF_demo_01.Views;

namespace WPF_demo_01.ViewModels
{
    public class AdViewmodel_DanhMucSanPham : INotifyPropertyChanged
    {
        // --------- Các biến private ----------------------------
        private SupabaseService supabaseService = new SupabaseService();
        // 1. Biến điều khiển ẩn hiện
        private bool _isAddOverlayVisible;
        // ẩn/hiện dialog chi tiết danh mục
        private bool _isDetailVisible;
        // biến thông báo lỗi nhập tên danh mục
        private string _errInputCategoryName;
        // thông báo lỗi nhập mô tả danh mục
        private string _errInputCategoryDesc;
        // tên danh mục
        private string _newCategoryName;
        // mô tả danh mục
        private string _newCategoryDesc;
        // thông tin category vừa chọn
        private Categories _currentSelectedCategory;
        // danh sách danh mục
        private ObservableCollection<Categories> _listCategories = new ObservableCollection<Categories>();
        // mảng chứa danh sách demo nhà cung cấp
        private List<Suppliers> _arrSuppliers;
        // danh sách nhà cung cấp
        private ObservableCollection<Suppliers> _filtereSuppliers = new ObservableCollection<Suppliers>();


        //---------------- thiết lập thuộc tính----------------------
        // biến nhận giá trị tên danh mục và mô tả danh mục
        public string NewCategoryName 
        {
            get => _newCategoryName;
            set
            {
                _newCategoryName = value;
                ErrInputCategoryName = string.Empty;
                OnPropertyChanged();
            }
        }
        public string NewCategoryDesc { get => _newCategoryDesc;
            set 
            {
                _newCategoryDesc = value;
                ErrInputCategoryDesc = string.Empty;  
                OnPropertyChanged();
            } 
        }
        public string ErrInputCategoryName
        {
            get => _errInputCategoryName;
            set
            {
                _errInputCategoryName = value;
                OnPropertyChanged();
            }
        }
        public string ErrInputCategoryDesc
        {
            get => _errInputCategoryDesc;
            set
            {
                _errInputCategoryDesc = value;
                OnPropertyChanged();
            }
        }
        public Categories CurrentSelectedCategory
        {
            get => _currentSelectedCategory;
            set
            {
                _currentSelectedCategory = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Categories> ListCategories
        {
            get => _listCategories;
            set
            {
                _listCategories = value;
                OnPropertyChanged();
            }
        }
        public bool IsAddOverlayVisible
        {
            get => _isAddOverlayVisible;
            set { _isAddOverlayVisible = value; OnPropertyChanged(); }
        }
        public bool IsDetailVisible
        {
            get => _isDetailVisible;
            set
            {
                _isDetailVisible = value; OnPropertyChanged();
            }
        }
        public ObservableCollection<Suppliers> FiltereSuppliers
        {
            get => _filtereSuppliers;
            set
            {
                _filtereSuppliers = value;
                OnPropertyChanged();
            }
        }




        // ------------------ Các Command---------------------------------
        public ICommand ShowAddOverlayCommand { get; set; }
        public ICommand ShowDetailCategoriesCommand { get; set; }
        public ICommand HideAddOverlayCommand { get; set; }
        public ICommand HideDetailOverlayCommand { set; get; }
        public ICommand LoadDataCommand { get; }
        public ICommand SelectItemCommand { get; set; }
        // icommand sự kiện thêm danh mục
        public ICommand SaveNewCategory { get; set; }
        // sự kiện xoá item danh mục
        public ICommand DeleteItemCategory { get; set; }



        // constructor
        public AdViewmodel_DanhMucSanPham() 
        {
            _ = LoadDataCategories();
            SelectItemCommand = new RelayCommand(p => ExecuteSelectItem(p));
            ShowAddOverlayCommand = new RelayCommand(p => IsAddOverlayVisible = true);
            ShowDetailCategoriesCommand = new RelayCommand(p => IsDetailVisible = true);
            HideDetailOverlayCommand = new RelayCommand( p => IsDetailVisible = false);
            HideAddOverlayCommand = new RelayCommand(p =>
            {
                IsAddOverlayVisible = false;
                // xoá dữ liệu thêm danh mục 
                clearForm_AddCategory();
            });

            // nút lưu danh mục thêm vào
            SaveNewCategory = new RelayCommand((p) => fncAddCategoryNew());

            DeleteItemCategory = new RelayCommand((p) => ExecuteDeleteItemCategory(p));


            // -------------------KHỞI TẠO DỮ LIỆU NHÀ CUNG CẤP GIẢ
            _filtereSuppliers = new ObservableCollection<Suppliers>
            {
                new Suppliers(1,"Nhà cung cấp 01","0123456789","nhacungcap01@gmail.com","diachidemo01"),
                new Suppliers(2,"Nhà cung cấp 02","0123456789","nhacungcap02@gmail.com","diachidemo02"),
                new Suppliers(3,"Nhà cung cấp 03","0123456789","nhacungcap03@gmail.com","diachidemo03"),
                new Suppliers(4,"Nhà cung cấp 04","0123456789","nhacungcap04@gmail.com","diachidemo04"),
            };
            
        }

        // hàm bắt sự kiện click item trong danh sách danh mục
        private void ExecuteSelectItem(object parameter)
        {
            var item = parameter as Categories;
            IsDetailVisible = true;
            CurrentSelectedCategory = item;

        }

        // hàm xoá item danh mục 
        private async void ExecuteDeleteItemCategory(object parameter)
        {
            var itemCategory = parameter as Categories;
            if (itemCategory == null) return;

            // hiển thị thông báo xác nhận có muốn xoá item category không
            MessageBoxResult confirm = MessageBox.Show(
                $"Bạn có muốn xoá item danh mục có tên: {itemCategory.Name} này không ?",
                "Xác nhận xoá item danh mục",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if(confirm == MessageBoxResult.Yes)
            {
                bool resultDeletCategory = await supabaseService.DeleteCategoriesAsnyc(itemCategory.Id);
                if (!resultDeletCategory)
                {
                    MessageBox.Show($"Xoá danh mục có tên: {itemCategory.Name} đã bị lỗi. Vui lòng thao tác lại sau.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ListCategories.Remove(itemCategory);
               
            }
        }

        // hàm lưu thông tin danh mục vừa thêm vào
        private async void fncAddCategoryNew()
        {
            if (string.IsNullOrWhiteSpace(_newCategoryName))
            {
               ErrInputCategoryName = "Lỗi tên danh mục không hợp lệ";
                Console.WriteLine($"Teen loi: {_errInputCategoryName}");

            }
            else if (string.IsNullOrWhiteSpace(_newCategoryDesc))
            {
                ErrInputCategoryDesc = "Lỗi mô tả danh mục không hợp lệ";
            }
            else
            {
                ErrInputCategoryName = string.Empty;
                ErrInputCategoryDesc = string.Empty;
                Categories categoryNew = new Categories(NewCategoryName, NewCategoryDesc);
                var result = await supabaseService.InsertCategoryAsnyc(categoryNew);
                _listCategories.Add(categoryNew);
                // chức năng lưu danh mục
                IsAddOverlayVisible = false;
                // xoá dữ liệu form thêm danh mục
                clearForm_AddCategory();
            }
        }

        // hàm load danh sách category
        public async Task LoadDataCategories()
        {
            try
            {
                var result = await supabaseService.GetCategoriesAsnyc();
                Console.WriteLine($"result data: {result.Count()}");
                ListCategories = new ObservableCollection<Categories>(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Err exception load data categories: {ex}");
            }
        }

        // hàm xoá thông tin nhập thêm danh mục sản phẩm
        private void clearForm_AddCategory()
        {
            NewCategoryName = string.Empty;
            NewCategoryDesc = string.Empty;
            ErrInputCategoryDesc = string.Empty;
            ErrInputCategoryName = string.Empty;
            
        }


        // hàm bắt sự thay đổi các property
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

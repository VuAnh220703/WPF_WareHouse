using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_demo_01.Class;

namespace WPF_demo_01.ViewModels
{
   public class HomeViewModel : INotifyPropertyChanged
    {
        private string _selectedMenu;
        public ICommand ChangeMenuCommand { get; set; }

        public string SelectedMenu
        {
            get => _selectedMenu;
            set
            {
                _selectedMenu = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            SelectedMenu = "BanHang";
            ChangeMenuCommand = new RelayCommand(param =>
            {
                SelectedMenu = param.ToString();
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

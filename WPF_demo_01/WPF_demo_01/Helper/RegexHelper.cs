using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WPF_demo_01.Helper
{
   public static class RegexHelper
    {
        private static string _regexPhone = @"^ 0\d{9}$";
        private static string _regexIduser = @"^\d{4}[A-Za-z]{2}\d{4}$";
       
    // regex input numberphone
        public static bool isValidPhone (string numberphone)
        {
            
            return Regex.IsMatch(numberphone.Trim(), _regexPhone);
        }

        public static string isValidIdUser(string idUser)
        {
            string id = idUser.Trim();
            if (string.IsNullOrEmpty(id))
            {
                return "Vui lòng nhập ID nhân viên";
            }
            else
            {
                if (!Regex.IsMatch(id, _regexIduser))
                {
                    return "ID nhân viên không hợp lệ";
                }
            }
            return null;
        }
    }
}

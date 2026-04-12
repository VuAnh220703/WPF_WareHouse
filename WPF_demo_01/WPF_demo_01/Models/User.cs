using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_demo_01.Models
{
   public class User
    {
        public int Id { private set; get; }
        public string Code { private set; get; }
        public string Qrcode { private set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string PasswordHash { set; get; }
        public string SignatureToken { set; get; }
        public DateTime CreateAt { private set; get; }
        public DateTime? DaleteAt { set; get; }
        public int RoleId { set; get; }
    }
}

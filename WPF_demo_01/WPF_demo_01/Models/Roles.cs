using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_demo_01.Models
{
    [Table("roles")]
    public class Roles : BaseModel
    {
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("role_code")]
        [JsonProperty("role_code")] // Giúp C# hiểu "role_code" chính là RoleCode
        public string RoleCode { get; set; }

        [Column("role_name")]
        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}

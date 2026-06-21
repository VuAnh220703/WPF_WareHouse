using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace WPF_demo_01.Models
{
   [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("username")]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Column("qrcode")]
        [JsonProperty("qrcode")]
        public string Qrcode { get; set; }

        [Column("full_name")]
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [Column("avatar_url")]
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [Column("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Column("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [Column("password_hash")]
        [JsonProperty("password_hash")]
        public string PasswordHash { get; set; }

        [Column("signature_token")]
        [JsonProperty("signature_token")]
        public string SignatureToken { get; set; }

        [Column("warehouse_id")]
        [JsonProperty("warehouse_id")]
        public long? WarehouseId { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("deleted_at")]
        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("role_id")]
        [JsonProperty("role_id")]
        public int RoleId { get; set; }

        [Reference(typeof(Roles))]
        [JsonProperty("role")]
        public Roles Role { get; set; }
    }
    
}

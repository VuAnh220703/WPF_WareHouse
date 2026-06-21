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
    [Table("suppliers")]
    public class Suppliers : BaseModel
    {
        [PrimaryKey("id")]
        [JsonProperty("id")] // Đồng bộ id giữa DB, JSON và C#
        public int Id { get; set; }

        [Column("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Column("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [Column("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Column("address")]
        [JsonProperty("address")]
        public string Address { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        // Constructor mặc định cho Supabase/Newtonsoft hoạt động ổn định
        public Suppliers() { }

        // Constructor đầy đủ tham số
        public Suppliers(int id, string name, string phone, string email, string address, DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            CreatedAt = createdAt;
        }

        public Suppliers(int id, string name, string phone, string email, string address)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}

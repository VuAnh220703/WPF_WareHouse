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
    [Table("contract_types")]
    public class ContractTypes : BaseModel
    {
        [PrimaryKey("id")]
        [JsonProperty("id")] 
        public int Id { get; set; }

        [Column("type_code")]
        [JsonProperty("type_code")] 
        public string TypeCode { get; set; }

        [Column("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Column("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")] 
        public DateTime? CreatedAt { get; set; }

        // Constructor mặc định bắt buộc phải có để Supabase map dữ liệu ổn định
        public ContractTypes() { }

        // Constructor không có ngày tạo
        public ContractTypes(int id, string typecode, string name, string description)
        {
            Id = id;
            TypeCode = typecode;
            Name = name;
            Description = description;
        }

        // Constructor đầy đủ tham số
        public ContractTypes(int id, string typecode, string name, string description, DateTime? createdAt)
        {
            Id = id;
            TypeCode = typecode;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}

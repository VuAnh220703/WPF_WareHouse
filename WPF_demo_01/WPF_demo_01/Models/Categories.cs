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
    [Table("categories")] 
    public class Categories : BaseModel
    {
        [PrimaryKey("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("category_code")]
        [JsonProperty("category_code")]
        public string CategoryCode { get; set; }

        [Column("category_name")]
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [Column("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("deleted_at")]
        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        public Categories() { }

        public Categories(string name, string description)
        {
            CategoryName = name;
            Description = description;
        }

        [JsonConstructor]
        public Categories(int id, string name, DateTime? deletedAt, string description)
        {
            Id = id;
            CategoryName = name;
            DeletedAt = deletedAt;
            Description = description;
        }

        public override string ToString()
        {
            return $"Categories => id: {Id} , name: {CategoryName}, description: {Description}, deleted_at: {DeletedAt}";
        }
    }
}

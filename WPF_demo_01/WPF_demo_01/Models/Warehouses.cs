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
    [Table("warehouses")] // Khớp tên bảng số nhiều trong DB
    public class Warehouses : BaseModel
    {
        Nâng cấp luôn lớp Warehouse này theo đúng chuẩn "song sát" để đổ dữ liệu danh sách kho hàng lên giao diện WPF mượt mà, không lo dính lỗi null.

Mã nguồn hoàn chỉnh sau khi được làm sạch và bổ sung nhãn JSON:

C#
using Postgrest.Attributes;
using Postgrest.Models;
using Newtonsoft.Json; // Thư viện bắt buộc để đồng bộ hóa JSON
using System;

namespace YourProject.Models
    {
        [Table("warehouses")] // Khớp với tên bảng số nhiều trong Database
        public class Warehouse : BaseModel
        {
            [PrimaryKey("id")]
            [JsonProperty("id")] // Ép kiểu chính xác trường id
            public int Id { get; set; }

            [Column("warehouse_code")]
            [JsonProperty("warehouse_code")] // Đồng bộ mã kho từ JSON vào C#
            public string WarehouseCode { get; set; }

            [Column("warehouse_name")]
            [JsonProperty("warehouse_name")] // Đồng bộ tên kho từ JSON vào C#
            public string WarehouseName { get; set; }

            [Column("address")]
            [JsonProperty("address")]
            public string Address { get; set; }

            [Column("deleted_at")]
            [JsonProperty("deleted_at")] // Đồng bộ trường xóa mềm phục vụ logic hệ thống
            public DateTime? DeletedAt { get; set; }

            // Constructor mặc định bắt buộc phải có cho SDK Supabase hoạt động
            public Warehouses() { }
    }
}

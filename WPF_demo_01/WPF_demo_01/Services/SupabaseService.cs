using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WPF_demo_01.Models;
using Postgrest;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WPF_demo_01.Services
{
    //connect Supabase
   public class SupabaseService
    {
        // Truy vấn xác thực đăng nhập
        public async Task<string> CheckLoginAsnyc(string idUser, string password)
        {
            // Đổi p_iduser thành p_username cho đồng bộ với database mới
            var parameters = new Dictionary<string, object>
    {
        { "p_username", idUser },
        { "p_password", password }
    };

            try
            {
                var response = await SupabaseConfig.Client.Rpc("check_login_user", parameters);
                Console.WriteLine($"Log return login: {response.Content}");
                return response.Content; // Trả về chuỗi JSON nguyên bản
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR RPC LOGIN: " + ex.Message);
                return null;
            }
        }

        // hàm thêm danh mục RPC
        public async Task<Categories> InsertCategoryAsnyc(Categories categoryNew)
        {
            try
            {
                var param = new Dictionary<string, Object>
                {
                    {"category_data",categoryNew }
                };

               var response = await SupabaseConfig.Client.Rpc("insert_category", param);
                var data = JsonConvert.DeserializeObject<List<Categories>>(response.Content);
                return data[0] ?? new Categories();
            }
            catch(Exception ex)
            {
               Console.WriteLine($"Err insert category: {ex}");
                return new Categories();
            }
        }

        // hàm lấy danh sách danh mục
        public async Task<List<Categories>> GetCategoriesAsnyc()
        {
            try
            {
                var response = await SupabaseConfig.Client.Rpc("get_category", null);
                var data = JsonConvert.DeserializeObject<List<Categories>>(response.Content);
                return data ?? new List<Categories>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error get category: {ex}");
                return new List<Categories>();
            }
        }

        // hàm xoá item danh mục
        public async Task<bool> DeleteCategoriesAsnyc(int? idCategory)
        {
            try
            {
                var parameter = new Dictionary<string, object>
                {
                    {"category_id", idCategory}
                };

                var result = await SupabaseConfig.Client.Rpc<bool>("delete_category", parameter);
                return result;
            }catch(Exception ex)
            {
                Console.WriteLine($"Lỗi khi xoá item danh mục có id: {idCategory} là: {ex}");
                return false;
            }
        }
    }
}

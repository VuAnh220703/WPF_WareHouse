using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WPF_demo_01.Services
{
    //connect Supabase
   public class SupabaseService
    {
        private readonly HttpClient _client;

        private const string BASE_URL = "https://vlgwrhcsdhzzaiscgsjw.supabase.co";
        private const string API_KEY = "sb_publishable_zvXF-9ZwBNK2VvxLS0N_nQ_1U4tupfh";

        public SupabaseService()
        {
            _client = new HttpClient();

            _client.DefaultRequestHeaders.Add("apikey", API_KEY);
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", API_KEY);
        }
    }
}

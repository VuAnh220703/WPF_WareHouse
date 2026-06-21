using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_demo_01.Models;

namespace WPF_demo_01.Services
{
  public static class SupabaseConfig
    {
        private const string URL = "https://vlgwrhcsdhzzaiscgsjw.supabase.co";
        private const string KEY = "sb_publishable_zvXF-9ZwBNK2VvxLS0N_nQ_1U4tupfh";

        public static Supabase.Client Client { get ;private set ;}
        public static void Initialize()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
            };
            Client = new Supabase.Client(URL, KEY, options);
        }

    }
}

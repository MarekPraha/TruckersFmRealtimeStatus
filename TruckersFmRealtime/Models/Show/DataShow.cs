using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckersFmRealtime.Models.Show
{
    public class DataShow
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int user_id { get; set; }
        public object perm_slot_id { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int start { get; set; }
        public object midnight { get; set; }
        public int end { get; set; }
        public string image { get; set; }
        public object banner { get; set; }
        public bool secret_sound { get; set; }
        public object on_demand { get; set; }
        public User user { get; set; }
    }
}

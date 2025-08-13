using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckersFmRealtime.Models.Songs
{
    public class CurrentSong
    {
        public int song_id { get; set; }
        public int user_id { get; set; }
        public int slot_id { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public int id { get; set; }
        public SongFull song { get; set; }
    }
}

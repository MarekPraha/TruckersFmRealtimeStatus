using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckersFmRealtime.Models.Songs
{
    internal class SongSocketData
    {
        public CurrentSong current_song { get; set; }
        public List<SongFull>? song_history { get; set; }
    }
}

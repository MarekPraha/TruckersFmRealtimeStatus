using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckersFmRealtime.Models.Songs
{
    public class SongFull
    {
        public int id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string album_art { get; set; }
        public string link { get; set; }
        public int playcount { get; set; }
        public int hidden { get; set; }
        public DateTime? created_at { get; set; } = null;
        public DateTime? updated_at { get; set; } = null;
        public int? chartPlacings { get; set; } = null;
        public int? monthlyChartPlacings { get; set; } = null;
    }
}

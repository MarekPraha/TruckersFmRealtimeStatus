using System;
using System.Collections.Generic;

namespace TruckersFmRealtime.ModelsDB;

public partial class TbSong
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public int PlayCount { get; set; }

    public int? ChartPlacings { get; set; }

    public string Link { get; set; } = null!;

    public virtual ICollection<TbSongsShow> TbSongsShows { get; set; } = new List<TbSongsShow>();
}

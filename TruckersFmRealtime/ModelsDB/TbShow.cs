using System;
using System.Collections.Generic;

namespace TruckersFmRealtime.ModelsDB;

public partial class TbShow
{
    public int Id { get; set; }

    public string ShowDescription { get; set; } = null!;

    public string PresentedBy { get; set; } = null!;

    public virtual ICollection<TbSongsShow> TbSongsShows { get; set; } = new List<TbSongsShow>();
}

using System;
using System.Collections.Generic;

namespace TruckersFmRealtime.ModelsDB;

public partial class TbSongsShow
{
    public int Id { get; set; }

    public int SongId { get; set; }

    public int ShowId { get; set; }

    public virtual TbShow Show { get; set; } = null!;

    public virtual TbSong Song { get; set; } = null!;
}

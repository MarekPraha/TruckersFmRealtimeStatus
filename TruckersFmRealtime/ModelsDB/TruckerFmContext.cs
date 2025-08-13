using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TruckersFmRealtime.ModelsDB;

public partial class TruckerFmContext : DbContext
{
    public TruckerFmContext()
    {
    }

    public TruckerFmContext(DbContextOptions<TruckerFmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbShow> TbShows { get; set; }

    public virtual DbSet<TbSong> TbSongs { get; set; }

    public virtual DbSet<TbSongsShow> TbSongsShows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning Add your connection string here.
        //add your connection string here
        => optionsBuilder.UseMySQL("<Put the connection string here>");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbShow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.PresentedBy).HasMaxLength(300);
            entity.Property(e => e.ShowDescription).HasMaxLength(300);
        });

        modelBuilder.Entity<TbSong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Artist).HasMaxLength(300);
            entity.Property(e => e.ChartPlacings)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Link).HasMaxLength(300);
            entity.Property(e => e.PlayCount).HasColumnType("int(11)");
            entity.Property(e => e.Title).HasMaxLength(300);
        });

        modelBuilder.Entity<TbSongsShow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.ShowId, "TvSongsShows_ShowID_to_tbshow_Id");

            entity.HasIndex(e => e.SongId, "TvSongsShows_SongID_to_tbSongs_Id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ShowId)
                .HasColumnType("int(11)")
                .HasColumnName("ShowID");
            entity.Property(e => e.SongId)
                .HasColumnType("int(11)")
                .HasColumnName("SongID");

            entity.HasOne(d => d.Show).WithMany(p => p.TbSongsShows)
                .HasForeignKey(d => d.ShowId)
                .HasConstraintName("TvSongsShows_ShowID_to_tbshow_Id");

            entity.HasOne(d => d.Song).WithMany(p => p.TbSongsShows)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("TvSongsShows_SongID_to_tbSongs_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

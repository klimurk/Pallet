﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pallet.ExternalDatabase.Models
{
    /// <summary>
    /// Katalog für alle allgemeine Listen
    /// </summary>
    [Table("t_katalog")]
    [Index("KatId", Name = "idx_kat_id")]
    [Index("KatText", "KatId", Name = "idx_kat_text_kat_id")]
    public partial class TKatalog
    {
        [Key]
        [Column("kat_grp")]
        public int KatGrp { get; set; }
        [Key]
        [Column("kat_id")]
        public int KatId { get; set; }
        [Column("kat_text")]
        [StringLength(80)]
        public string KatText { get; set; }
        [Column("kat_kurz_text")]
        [StringLength(20)]
        public string KatKurzText { get; set; }
        [Column("d_geloescht")]
        public DateOnly? DGeloescht { get; set; }
        [Column("kat_image_1")]
        public byte[] KatImage1 { get; set; }
        [Column("n_internal")]
        public bool? NInternal { get; set; }
        [Column("n_zipped")]
        public bool NZipped { get; set; }
        /// <summary>
        /// 1=wird in einer app nicht angezeigt
        /// </summary>
        [Column("n_no_app_use")]
        public bool? NNoAppUse { get; set; }
    }
}
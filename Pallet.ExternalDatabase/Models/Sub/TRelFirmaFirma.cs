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
    /// Verbindung von Firma zu Firma mit den entsprecheden Nummern.
    /// </summary>
    [Table("t_rel_firma_firma")]
    [Index("NFirmaIdDest", Name = "IDX_ID_DEST")]
    [Index("NFirmaIdSrc", Name = "IDX_ID_SRC")]
    public partial class TRelFirmaFirma
    {
        [Key]
        [Column("n_firma_id_src")]
        public int NFirmaIdSrc { get; set; }
        [Key]
        [Column("n_firma_id_dest")]
        public int NFirmaIdDest { get; set; }
        [Column("c_nr_src_dst")]
        [StringLength(25)]
        public string CNrSrcDst { get; set; }
        [Column("c_nr_dst_src")]
        [StringLength(25)]
        public string CNrDstSrc { get; set; }
        [Column("c_lieferantnr_src_dst")]
        [StringLength(25)]
        public string CLieferantnrSrcDst { get; set; }
        [Column("c_lieferantnr_dst_src")]
        [StringLength(25)]
        public string CLieferantnrDstSrc { get; set; }
    }
}
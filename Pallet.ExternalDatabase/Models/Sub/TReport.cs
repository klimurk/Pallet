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
    /// Alle Reports die für den Ausdruck benötigt werden sind in dieser Tabelle gespeichert.
    /// </summary>
    [Table("t_report")]
    public partial class TReport
    {
        [Key]
        [Column("n_user_id")]
        public int NUserId { get; set; }
        [Key]
        [Column("c_filename")]
        [StringLength(70)]
        public string CFilename { get; set; }
        [Column("d_geaendert", TypeName = "datetime")]
        public DateTime DGeaendert { get; set; }
        [Required]
        [Column("m_data")]
        public byte[] MData { get; set; }
        [Required]
        [Column("n_sync")]
        public bool? NSync { get; set; }
        [Column("n_re_sync")]
        public bool NReSync { get; set; }
        [Column("c_name")]
        [StringLength(45)]
        public string CName { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("n_downloaded")]
        public bool NDownloaded { get; set; }
    }
}
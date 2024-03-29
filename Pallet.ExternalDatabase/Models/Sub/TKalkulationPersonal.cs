﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pallet.ExternalDatabase.Models
{
    [Table("t_kalkulation_personal")]
    public partial class TKalkulationPersonal
    {
        [Key]
        [Column("n_verp_id")]
        public int NVerpId { get; set; }
        [Key]
        [Column("n_verptyp_id")]
        public int NVerptypId { get; set; }
        [Key]
        [Column("n_parent_id")]
        public int NParentId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(45)]
        public string CName { get; set; }
        [Column("c_bezeichnung")]
        [StringLength(50)]
        public string CBezeichnung { get; set; }
        [Column("n_anzahl")]
        [Precision(12, 2)]
        public decimal NAnzahl { get; set; }
        /// <summary>
        /// kat_grp=11
        /// </summary>
        [Column("n_anzahl_einheit_id")]
        public int NAnzahlEinheitId { get; set; }
        /// <summary>
        /// Minuten pro Quadratmeter, Qubikmeter oder Stueck
        /// </summary>
        [Column("n_minuten_pro")]
        [Precision(6, 2)]
        public decimal? NMinutenPro { get; set; }
        [Column("n_preis")]
        [Precision(25, 4)]
        public decimal NPreis { get; set; }
        /// <summary>
        /// Preis pro Stunde
        /// </summary>
        [Column("n_preis_pro")]
        [Precision(10, 2)]
        public decimal? NPreisPro { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("d_created", TypeName = "timestamp")]
        public DateTime DCreated { get; set; }
    }
}
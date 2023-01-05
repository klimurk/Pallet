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
    /// Objekte wie Packstücke die zu einer Planung einer Verladung/Stauung gehören
    /// </summary>
    [Table("t_raumstau_objekte")]
    [Index("NVerpId", "NVerptypId", "NVerpSubId", "CNr", Name = "IDX_VERP")]
    public partial class TRaumstauObjekte
    {
        [Key]
        [Column("n_planung_id")]
        public int NPlanungId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        /// <summary>
        /// Nur gefuellt wenn Referenz auf echte Verpackung
        /// </summary>
        [Column("n_verp_id")]
        public int? NVerpId { get; set; }
        /// <summary>
        /// Nur gefuellt wenn Referenz  auf echte Verpackung
        /// </summary>
        [Column("n_verptyp_id")]
        public int? NVerptypId { get; set; }
        [Column("n_verp_sub_id")]
        public int? NVerpSubId { get; set; }
        /// <summary>
        /// sotKiste=1, sotVerschlag, sotPalette, sotKarton, sotGitterbox
        /// </summary>
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("c_nr")]
        [StringLength(25)]
        public string CNr { get; set; }
        [Column("c_nr_ext")]
        [StringLength(45)]
        public string CNrExt { get; set; }
        [Column("c_bezeichnung")]
        [StringLength(50)]
        public string CBezeichnung { get; set; }
        [Column("c_position")]
        [StringLength(25)]
        public string CPosition { get; set; }
        [Column("n_laenge")]
        public int NLaenge { get; set; }
        [Column("n_breite")]
        public int NBreite { get; set; }
        [Column("n_hoehe")]
        public int NHoehe { get; set; }
        [Column("n_stapelbar")]
        public bool NStapelbar { get; set; }
        [Column("d_created", TypeName = "timestamp")]
        public DateTime DCreated { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("n_gewicht")]
        public int NGewicht { get; set; }
    }
}
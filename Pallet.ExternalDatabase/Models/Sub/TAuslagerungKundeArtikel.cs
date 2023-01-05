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
    /// Kopfdaten zu Auslagerung von Kunden-Artikeln
    /// </summary>
    [Table("t_auslagerung_kunde_artikel")]
    public partial class TAuslagerungKundeArtikel
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int? NKundeId { get; set; }
        [Column("c_nr")]
        [StringLength(25)]
        public string CNr { get; set; }
        /// <summary>
        /// kat_grp=14 Status dient zu besseren Übersicht 
        /// </summary>
        [Column("n_status_id")]
        public int NStatusId { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("d_datum")]
        public DateOnly? DDatum { get; set; }
        [Column("d_erstellt")]
        public DateOnly? DErstellt { get; set; }
        [Column("d_geloescht", TypeName = "datetime")]
        public DateTime? DGeloescht { get; set; }
        [Column("n_verrechnet")]
        public int? NVerrechnet { get; set; }
        [Column("n_nicht_verrechenbar")]
        public bool? NNichtVerrechenbar { get; set; }
        [Column("n_auftrag_id")]
        public int? NAuftragId { get; set; }
        [Column("n_user_id")]
        public int? NUserId { get; set; }
    }
}
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
    /// Posten zur Auslagerung für Verpackungen, Artikel
    /// </summary>
    [Table("t_auslagerungartikel")]
    [Index("NEinlagerungId", Name = "IDX_WARENEINGANG_VERP_ID")]
    [Index("NAuslagerungId", Name = "idx_n_auslagerung_id")]
    public partial class TAuslagerungartikel
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_auslagerung_id")]
        public int NAuslagerungId { get; set; }
        [Column("n_artikel_id")]
        public int NArtikelId { get; set; }
        [Column("n_auslagerung_verp_id")]
        public int? NAuslagerungVerpId { get; set; }
        [Column("n_einlagerung_id")]
        public int? NEinlagerungId { get; set; }
        [Column("n_gebraucht_verp_id")]
        public int? NGebrauchtVerpId { get; set; }
        [Column("n_einheit_id")]
        public int? NEinheitId { get; set; }
        [Column("n_anzahl")]
        public float NAnzahl { get; set; }
        [Column("n_anzahl_verpackt")]
        public int NAnzahlVerpackt { get; set; }
        [Column("n_lager_id")]
        public int NLagerId { get; set; }
        [Column("n_lagerort_id")]
        public int NLagerortId { get; set; }
        [Column("c_kommisions_nr")]
        [StringLength(50)]
        public string CKommisionsNr { get; set; }
        [Column("d_geloescht", TypeName = "datetime")]
        public DateTime? DGeloescht { get; set; }
        /// <summary>
        /// wird gesetzt, wenn eine Verpackung aus dem Lager genommen wird und die Artikelanzahl in der Verpackung unbekannt ist
        /// </summary>
        [Column("n_komplett_auslagerung")]
        public bool? NKomplettAuslagerung { get; set; }
        [Column("n_verrechnet")]
        public int? NVerrechnet { get; set; }
    }
}
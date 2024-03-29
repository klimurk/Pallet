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
    /// Posten zum Lieferschein
    /// </summary>
    [Table("t_lieferschein_posten")]
    [Index("NLieferscheinId", "NAuftragPostenId", "NVerpId", "NVerpTypId", "NAuftragId", Name = "FK_t_lieferschein_posten_verp_lieferschein_id")]
    [Index("NAuftragId", Name = "IDX_AUFTRAG_ID")]
    [Index("NAuftragPostenId", "NAuftragId", "NTypId", "NAnzahl", Name = "IDX_AUFTRAG_POSTEN")]
    [Index("NVerpId", "NVerpTypId", Name = "IDX_VERP_REF")]
    public partial class TLieferscheinPosten
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("n_lieferschein_id")]
        public int NLieferscheinId { get; set; }
        [Column("n_position")]
        public int? NPosition { get; set; }
        [Column("n_kunde_id")]
        public int? NKundeId { get; set; }
        [Column("n_auftrag_id")]
        public int? NAuftragId { get; set; }
        [Column("n_verp_id")]
        public int? NVerpId { get; set; }
        [Column("n_verp_typ_id")]
        public int? NVerpTypId { get; set; }
        [Column("c_verp_nr")]
        [StringLength(50)]
        public string CVerpNr { get; set; }
        [Column("n_auftrag_posten_id")]
        public int? NAuftragPostenId { get; set; }
        [Column("n_anzahl")]
        [Precision(19, 3)]
        public decimal NAnzahl { get; set; }
        [Column("c_bez")]
        [StringLength(45)]
        public string CBez { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
        [Column("n_laenge")]
        public int? NLaenge { get; set; }
        [Column("n_breite")]
        public int? NBreite { get; set; }
        [Column("n_hoehe")]
        public int? NHoehe { get; set; }
        [Column("n_gewicht_brutto")]
        public int? NGewichtBrutto { get; set; }
        /// <summary>
        /// 0=Gesamtgewicht; 1=Einzelgewicht&apos; AFTER `n_gewicht_brutto
        /// </summary>
        [Column("n_einzelgewicht")]
        public bool NEinzelgewicht { get; set; }
        [Column("n_behandlungsart_id")]
        public int? NBehandlungsartId { get; set; }
        /// <summary>
        /// Anzahl der schon erfassten xxx in einer Rechnung
        /// </summary>
        [Column("n_anzahl_rechnung")]
        public int? NAnzahlRechnung { get; set; }
        [Column("d_created", TypeName = "timestamp")]
        public DateTime DCreated { get; set; }
    }
}
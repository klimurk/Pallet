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
    /// Immer wiederkehrende Artikel eines Kunden. Kann auch mit t_artikel abgebildet werden. Spalten wie t_auftrag_posten_sonstige
    /// </summary>
    [Table("t_auftrag_posten_sonstige_artikel")]
    public partial class TAuftragPostenSonstigeArtikel
    {
        [Key]
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("n_laenge")]
        public ushort? NLaenge { get; set; }
        [Column("n_laenge_bausatz")]
        public ushort? NLaengeBausatz { get; set; }
        [Column("n_breite")]
        public ushort? NBreite { get; set; }
        [Column("n_breite_bausatz")]
        public ushort? NBreiteBausatz { get; set; }
        [Column("n_hoehe")]
        public ushort? NHoehe { get; set; }
        [Column("n_hoehe_bausatz")]
        public ushort? NHoeheBausatz { get; set; }
        [Column("n_zn")]
        public bool? NZn { get; set; }
        /// <summary>
        /// kat_grp=10
        /// </summary>
        [Column("n_holz_typ_id")]
        public ushort? NHolzTypId { get; set; }
        [Column("n_gewicht_tara")]
        public uint? NGewichtTara { get; set; }
        [Column("c_bezeichnung")]
        [StringLength(45)]
        public string CBezeichnung { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
        [Column("n_gedruckt")]
        public bool? NGedruckt { get; set; }
        [Column("n_user_id")]
        public int NUserId { get; set; }
        [Column("n_einzelpreis")]
        [Precision(19, 2)]
        public decimal NEinzelpreis { get; set; }
        [Column("n_gesamtpreis")]
        [Precision(19, 2)]
        public decimal NGesamtpreis { get; set; }
    }
}
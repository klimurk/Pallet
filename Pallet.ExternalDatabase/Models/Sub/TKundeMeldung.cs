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
    /// Erfassung von Reklamationen zur Qualitätssicherung
    /// </summary>
    [Table("t_kunde_meldung")]
    public partial class TKundeMeldung
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("n_auftrag_id")]
        public int? NAuftragId { get; set; }
        [Column("d_datum")]
        public DateOnly DDatum { get; set; }
        /// <summary>
        /// kat_grp=120
        /// </summary>
        [Column("n_meldung_typ_id")]
        public int NMeldungTypId { get; set; }
        /// <summary>
        /// kat_grp=121
        /// </summary>
        [Column("n_zufriedenheit_id")]
        public int NZufriedenheitId { get; set; }
        [Column("c_beschreibung")]
        [StringLength(45)]
        public string CBeschreibung { get; set; }
        [Column("m_beschreibung", TypeName = "mediumtext")]
        public string MBeschreibung { get; set; }
        /// <summary>
        /// kat_grp=124
        /// </summary>
        [Column("n_err_verursacht_durch_id")]
        public int? NErrVerursachtDurchId { get; set; }
        /// <summary>
        /// t_ansprechpartner
        /// </summary>
        [Column("n_err_verursacht_durch_person_id")]
        public int? NErrVerursachtDurchPersonId { get; set; }
        [Column("c_err_verursacht_durch")]
        [StringLength(45)]
        public string CErrVerursachtDurch { get; set; }
        /// <summary>
        /// kat_grp=123
        /// </summary>
        [Column("n_err_bereich_id")]
        public int? NErrBereichId { get; set; }
        /// <summary>
        /// kat_grp=122
        /// </summary>
        [Column("n_err_ursache_id")]
        public int? NErrUrsacheId { get; set; }
        [Column("m_err_ursache", TypeName = "mediumtext")]
        public string MErrUrsache { get; set; }
        [Column("n_err_schadens_summe")]
        public int? NErrSchadensSumme { get; set; }
        [Column("c_err_ergebnis")]
        [StringLength(115)]
        public string CErrErgebnis { get; set; }
        [Column("m_sofort_massnahme", TypeName = "mediumtext")]
        public string MSofortMassnahme { get; set; }
        [Column("m_korrektur_massnahme", TypeName = "mediumtext")]
        public string MKorrekturMassnahme { get; set; }
        [Column("d_einfuehrung_bis")]
        public DateOnly? DEinfuehrungBis { get; set; }
        [Column("d_erledigt_am")]
        public DateOnly? DErledigtAm { get; set; }
        [Column("d_erstellt", TypeName = "datetime")]
        public DateTime DErstellt { get; set; }
        [Column("n_user_id")]
        public int NUserId { get; set; }
    }
}
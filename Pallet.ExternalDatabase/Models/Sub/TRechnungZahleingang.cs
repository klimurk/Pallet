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
    /// 1 Zahlungseingänge zu n Ausgangsrechnung. Der Gesamtbetrag des Zahlungseingangs ist in t_rechnung_zahleingang_multi
    /// </summary>
    [Table("t_rechnung_zahleingang")]
    [Index("NMultiId", "NRechnungId", Name = "IDX_MULTI_ID")]
    [Index("NMultiId", Name = "IDX_MULTI_ID_SMALL")]
    [Index("DDatum", Name = "IDX_ZAHL_DATUM")]
    [Index("NRechnungId", Name = "IDX_ZRECHNUNG_ID")]
    public partial class TRechnungZahleingang
    {
        [Key]
        [Column("n_rechnung_id")]
        public int NRechnungId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        /// <summary>
        /// FK t_rechnung_zahleingang_multi.n_id
        /// </summary>
        [Column("n_multi_id")]
        public int? NMultiId { get; set; }
        [Column("d_datum")]
        public DateOnly DDatum { get; set; }
        [Column("n_betrag")]
        [Precision(19, 2)]
        public decimal NBetrag { get; set; }
        /// <summary>
        /// kat_grp=29
        /// </summary>
        [Column("n_zahlungsart_id")]
        public int NZahlungsartId { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("m_bemerkung_opos", TypeName = "mediumtext")]
        public string MBemerkungOpos { get; set; }
    }
}
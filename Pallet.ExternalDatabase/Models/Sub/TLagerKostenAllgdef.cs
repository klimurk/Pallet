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
    /// Allgemeine Lagerkosten-Einstellungen zu einem Kunden
    /// </summary>
    [Table("t_lager_kosten_allgdef")]
    public partial class TLagerKostenAllgdef
    {
        [Key]
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("n_zuwegung")]
        [Precision(6, 3)]
        public decimal NZuwegung { get; set; }
        /// <summary>
        /// 1=Wareneinagng wird berechnet
        /// </summary>
        [Column("n_is_we_berechnen")]
        public bool? NIsWeBerechnen { get; set; }
        [Column("n_lager_freie_tage")]
        public int? NLagerFreieTage { get; set; }
    }
}
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
    /// Finale Werte der Plattenaufteilung in Abhängigkeit der existierenden Plattenmasse und zu berücksichitigen Parameter.
    /// </summary>
    [Table("t_zuschnitt_plattenwerkstoff_final")]
    public partial class TZuschnittPlattenwerkstoffFinal
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        /// <summary>
        /// siehe HolzElementDetailTyp 36=Verschalung Kopf, 37=Verscahlung Seite, 38=Verschalung Deckel
        /// </summary>
        [Column("n_holz_detail_typ")]
        public int NHolzDetailTyp { get; set; }
        [Key]
        [Column("n_id")]
        public long NId { get; set; }
        [Column("c_item_id")]
        [StringLength(45)]
        public string CItemId { get; set; }
        [Column("c_bez")]
        [StringLength(30)]
        public string CBez { get; set; }
        [Column("n_laenge")]
        public int NLaenge { get; set; }
        [Column("n_breite")]
        public int NBreite { get; set; }
        [Column("n_hoehe")]
        public int NHoehe { get; set; }
        [Column("n_anzahl")]
        public int NAnzahl { get; set; }
    }
}
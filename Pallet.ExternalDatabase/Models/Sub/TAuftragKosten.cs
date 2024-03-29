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
    /// Angefallende Kosten basisert auf den Eingangsrechnung und Konfiguration in der Feinkalkultion
    /// </summary>
    [Table("t_auftrag_kosten")]
    [Index("NAuftragId", Name = "IDX_AUFTRAG_ID")]
    public partial class TAuftragKosten
    {
        [Key]
        [Column("n_auftrag_id")]
        public int NAuftragId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        /// <summary>
        /// kat_grp = 61
        /// </summary>
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("n_betrag")]
        [Precision(19, 2)]
        public decimal NBetrag { get; set; }
        [Column("c_bez")]
        [StringLength(45)]
        public string CBez { get; set; }
        /// <summary>
        /// 1=vom System erstellt, 0=vom Benutzer hinzugefügt
        /// </summary>
        [Column("n_auto_created")]
        public bool? NAutoCreated { get; set; }
    }
}
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
    /// Erfassten Stützflächen einer Verpackung zu statischen Berechnung. DIe Stützflächen haben eine Referenz auf die zuvor erfasste Maschine
    /// </summary>
    [Table("t_verpackung_stuetzflaechen")]
    public partial class TVerpackungStuetzflaechen
    {
        [Key]
        [Column("n_verp_id")]
        public int NVerpId { get; set; }
        [Key]
        [Column("n_verp_typ_id")]
        public int NVerpTypId { get; set; }
        [Key]
        [Column("c_machine_id")]
        [StringLength(45)]
        public string CMachineId { get; set; }
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        /// <summary>
        /// 1=Position ist zum Boden
        /// </summary>
        [Column("n_pos_absolut")]
        public bool? NPosAbsolut { get; set; }
        [Column("n_pos_x")]
        public int? NPosX { get; set; }
        [Column("n_pos_y")]
        public int? NPosY { get; set; }
        [Column("n_laenge")]
        public int? NLaenge { get; set; }
        [Column("n_breite")]
        public int? NBreite { get; set; }
        [Column("n_hoehe")]
        public int? NHoehe { get; set; }
        [Column("n_schwer_x")]
        public int? NSchwerX { get; set; }
        [Column("n_schwer_y")]
        public int? NSchwerY { get; set; }
        [Column("n_schwer_z")]
        public int? NSchwerZ { get; set; }
        [Column("c_caption")]
        [StringLength(45)]
        public string CCaption { get; set; }
        [Column("n_belastung")]
        public int? NBelastung { get; set; }
        [Column("n_form")]
        public int? NForm { get; set; }
        [Column("c_bez")]
        [StringLength(25)]
        public string CBez { get; set; }
    }
}
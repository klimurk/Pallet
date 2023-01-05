﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pallet.ExternalDatabase.Models
{
    [Table("t_kalkulation_verpackung")]
    public partial class TKalkulationVerpackung
    {
        [Key]
        [Column("n_verp_id")]
        public int NVerpId { get; set; }
        [Key]
        [Column("n_verptyp_id")]
        public int NVerptypId { get; set; }
        [Key]
        [Column("n_parent_id")]
        public int NParentId { get; set; }
        [Key]
        [Column("n_child_id")]
        public int NChildId { get; set; }
        [Column("c_name")]
        [StringLength(45)]
        public string CName { get; set; }
        [Column("n_anzahl")]
        [Precision(10, 2)]
        public decimal NAnzahl { get; set; }
        /// <summary>
        /// 1=von System erzeugt; 0=manuell erzeugt
        /// </summary>
        [Required]
        [Column("n_auto")]
        public bool? NAuto { get; set; }
        [Column("n_preis")]
        [Precision(10, 2)]
        public decimal NPreis { get; set; }
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("n_icon_index")]
        public int? NIconIndex { get; set; }
        [Column("n_leaf")]
        public bool NLeaf { get; set; }
        [Column("d_created", TypeName = "timestamp")]
        public DateTime DCreated { get; set; }
    }
}
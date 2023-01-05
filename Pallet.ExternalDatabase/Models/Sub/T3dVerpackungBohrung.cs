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
    /// 3D Daten der Bohrung zur Verpackung, Referenz auf ein t_3d_verpackung_detail (Kantholz, ..)
    /// </summary>
    [Table("t_3d_verpackung_bohrung")]
    [Index("CParentItemId", Name = "idx_c_parent_item_id")]
    public partial class T3dVerpackungBohrung
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        [Key]
        [Column("n_id")]
        public long NId { get; set; }
        [Column("c_parent_item_id")]
        [StringLength(45)]
        public string CParentItemId { get; set; }
        [Column("n_x")]
        public int NX { get; set; }
        [Column("n_y")]
        public int NY { get; set; }
        [Column("n_z")]
        public int NZ { get; set; }
        [Column("n_laenge")]
        public int NLaenge { get; set; }
        [Column("n_durchmesser")]
        public int NDurchmesser { get; set; }
    }
}
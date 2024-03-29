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
    /// Speicehrt die Stahl Elemente im 3D Raum einer Verpackung.
    /// </summary>
    [Table("t_3d_verpackung_stahl")]
    public partial class T3dVerpackungStahl
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        [Key]
        [Column("n_id")]
        public long NId { get; set; }
        /// <summary>
        /// 1=Deckelsicherung, 2=Kopfwinkel-L, 3=Kopfwinkel-L, 4=U-Eisen, 5=Schlossschraube
        /// </summary>
        [Column("n_typ_id")]
        public int NTypId { get; set; }
        [Column("c_bez")]
        [StringLength(30)]
        public string CBez { get; set; }
        [Column("n_laenge_1")]
        public int NLaenge1 { get; set; }
        [Column("n_breite_1")]
        public int NBreite1 { get; set; }
        [Column("n_hoehe_1")]
        public int NHoehe1 { get; set; }
        [Column("n_laenge_2")]
        public int? NLaenge2 { get; set; }
        [Column("n_breite_2")]
        public int? NBreite2 { get; set; }
        [Column("n_hoehe_2")]
        public int? NHoehe2 { get; set; }
        [Column("n_laenge_3")]
        public int? NLaenge3 { get; set; }
        [Column("n_breite_3")]
        public int? NBreite3 { get; set; }
        [Column("n_hoehe_3")]
        public int? NHoehe3 { get; set; }
        [Column("n_visible")]
        public bool NVisible { get; set; }
        [Column("n_x")]
        public int NX { get; set; }
        [Column("n_y")]
        public int NY { get; set; }
        [Column("n_z")]
        public int NZ { get; set; }
        [Column("n_rotate_x")]
        public int NRotateX { get; set; }
        [Column("n_rotate_y")]
        public int NRotateY { get; set; }
        [Column("n_rotate_z")]
        public int NRotateZ { get; set; }
        [Column("c_layer_id")]
        [StringLength(15)]
        public string CLayerId { get; set; }
        [Column("c_layer_parent_id")]
        [StringLength(15)]
        public string CLayerParentId { get; set; }
    }
}
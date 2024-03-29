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
    /// CAD System - 3D hinzugefügte Objekte
    /// </summary>
    [Table("t_verpackung_3d_material")]
    public partial class TVerpackung3dMaterial
    {
        [Key]
        [Column("n_verp_id")]
        public int NVerpId { get; set; }
        [Key]
        [Column("n_verp_typ_id")]
        public int NVerpTypId { get; set; }
        [Key]
        [Column("n_verp_sub_id")]
        public int NVerpSubId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_pos_x")]
        public int NPosX { get; set; }
        [Column("n_pos_y")]
        public int NPosY { get; set; }
        [Column("n_pos_z")]
        public int NPosZ { get; set; }
        [Column("n_laenge")]
        public int? NLaenge { get; set; }
        [Column("n_breite")]
        public int NBreite { get; set; }
        [Column("n_hoehe")]
        public int? NHoehe { get; set; }
        [Column("n_gewicht_brutto")]
        public int? NGewichtBrutto { get; set; }
        /// <summary>
        /// kat_grp=92
        /// </summary>
        [Column("n_oberflaeche_id")]
        public int NOberflaecheId { get; set; }
        /// <summary>
        /// kat_grp=91
        /// </summary>
        [Column("n_form_id")]
        public int NFormId { get; set; }
        [Column("c_titel")]
        [StringLength(15)]
        public string CTitel { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
    }
}
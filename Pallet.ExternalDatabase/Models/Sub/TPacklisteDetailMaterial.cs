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
    /// Material zum Packstück (t_packliste_packstuecke_detail) einer Packliste (t_packliste_packstuecke_main)
    /// </summary>
    [Table("t_packliste_detail_material")]
    public partial class TPacklisteDetailMaterial
    {
        [Key]
        [Column("n_detail_id")]
        public int NDetailId { get; set; }
        [Key]
        [Column("n_material_id")]
        public int NMaterialId { get; set; }
        /// <summary>
        /// Referenz auf das Beiblatt
        /// </summary>
        [Column("n_beiblatt_id")]
        public int? NBeiblattId { get; set; }
        /// <summary>
        /// Referenz auf das Material vom Beiblatt
        /// </summary>
        [Column("n_beiblatt_material_id")]
        public int? NBeiblattMaterialId { get; set; }
        [Column("n_anzahl")]
        [Precision(19, 3)]
        public decimal NAnzahl { get; set; }
        [Column("n_einheit_id")]
        public int NEinheitId { get; set; }
        [Column("n_gewicht")]
        [Precision(19, 3)]
        public decimal? NGewicht { get; set; }
        [Column("n_einzelgewicht")]
        public bool? NEinzelgewicht { get; set; }
        [Column("c_position")]
        [StringLength(10)]
        public string CPosition { get; set; }
        [Column("c_artikel_nr")]
        [StringLength(25)]
        public string CArtikelNr { get; set; }
        /// <summary>
        /// kat_grp=19
        /// </summary>
        [Column("n_lang_id")]
        public int NLangId { get; set; }
        [Column("c_bez")]
        [StringLength(255)]
        public string CBez { get; set; }
        [Column("c_bez_zusatz")]
        [StringLength(50)]
        public string CBezZusatz { get; set; }
        /// <summary>
        /// Referenz auf t_translation wenn in Uebrsetzng aufgenommen
        /// </summary>
        [Column("n_trans_id")]
        public int? NTransId { get; set; }
        [Column("n_lang_id_1")]
        public int? NLangId1 { get; set; }
        [Column("c_bez_1")]
        [StringLength(255)]
        public string CBez1 { get; set; }
        [Column("c_bez_1_zusatz")]
        [StringLength(50)]
        public string CBez1Zusatz { get; set; }
        [Column("n_lang_id_2")]
        public int? NLangId2 { get; set; }
        [Column("c_bez_2")]
        [StringLength(255)]
        public string CBez2 { get; set; }
        [Column("c_bez_2_zusatz")]
        [StringLength(50)]
        public string CBez2Zusatz { get; set; }
    }
}
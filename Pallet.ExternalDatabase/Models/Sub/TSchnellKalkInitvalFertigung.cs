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
    /// Schnellkalkulation - Konfigurierte Berechnungsmethoden für die Fertigung
    /// </summary>
    [Table("t_schnell_kalk_initval_fertigung")]
    public partial class TSchnellKalkInitvalFertigung
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("n_is_standard")]
        public bool? NIsStandard { get; set; }
        [Column("c_name")]
        [StringLength(15)]
        public string CName { get; set; }
        [Column("n_fert_mat_schnitt_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittEk { get; set; }
        [Column("n_fert_mat_schnitt_konstr_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittKonstrEk { get; set; }
        [Column("n_fert_mat_schnitt_konstr_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittKonstrVk { get; set; }
        [Column("n_fert_mat_schnitt_konstr_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatSchnittKonstrVerschhnitt { get; set; }
        [Column("n_fert_mat_schnitt_beipack_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackEk { get; set; }
        [Column("n_fert_mat_schnitt_beipack_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackVk { get; set; }
        [Column("n_fert_mat_schnitt_beipack_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatSchnittBeipackVerschhnitt { get; set; }
        [Column("n_fert_mat_schnitt_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittVk { get; set; }
        [Column("n_fert_mat_schnitt_verschnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatSchnittVerschnitt { get; set; }
        [Column("n_fert_mat_kvh_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatKvhEk { get; set; }
        [Column("n_fert_mat_kvh_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatKvhVk { get; set; }
        [Column("n_fert_mat_oelpapier_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatOelpapierEk { get; set; }
        [Column("n_fert_mat_oelpapier_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatOelpapierVk { get; set; }
        [Column("n_fert_mat_oelpapier_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatOelpapierVerschhnitt { get; set; }
        [Column("n_fert_mat_peauskleiden_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatPeauskleidenEk { get; set; }
        [Column("n_fert_mat_peauskleiden_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatPeauskleidenVk { get; set; }
        [Column("n_fert_mat_peauskleiden_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatPeauskleidenVerschhnitt { get; set; }
        [Column("n_fert_mat_kvh_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatKvhVerschhnitt { get; set; }
        [Column("n_fert_mat_sperrholz_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzEk { get; set; }
        [Column("n_fert_mat_sperrholz_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzVk { get; set; }
        [Column("n_fert_mat_sperrholz_verschnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatSperrholzVerschnitt { get; set; }
        [Column("n_fert_mat_osb_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbEk { get; set; }
        [Column("n_fert_mat_osb_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbVk { get; set; }
        [Column("n_fert_mat_osb_verschnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatOsbVerschnitt { get; set; }
        [Column("n_fert_mat_siebdruck_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckEk { get; set; }
        [Column("n_fert_mat_siebdruck_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckVk { get; set; }
        [Column("n_fert_mat_siebdruck_verschnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatSiebdruckVerschnitt { get; set; }
        [Column("n_fert_mat_akylux_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatAkyluxEk { get; set; }
        [Column("n_fert_mat_akylux_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatAkyluxVk { get; set; }
        [Column("n_fert_mat_akylux_verschnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatAkyluxVerschnitt { get; set; }
        [Column("n_fert_mat_schloss_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchlossEk { get; set; }
        [Column("n_fert_mat_schloss_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchlossVk { get; set; }
        [Column("n_fert_mat_beschlaege_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatBeschlaegeEk { get; set; }
        [Column("n_fert_mat_beschlaege_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatBeschlaegeVk { get; set; }
        [Column("n_fert_mat_schwingdaempf_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchwingdaempfEk { get; set; }
        [Column("n_fert_mat_schwingdaempf_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchwingdaempfVk { get; set; }
        [Column("n_fert_mat_eckverbinder_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatEckverbinderEk { get; set; }
        [Column("n_fert_mat_eckverbinder_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatEckverbinderVk { get; set; }
        [Column("n_fert_pers_std_pro_schnitt_m3")]
        [Precision(7, 3)]
        public decimal? NFertPersStdProSchnittM3 { get; set; }
        [Column("n_fert_pers_std_pro_platte_m3")]
        [Precision(7, 3)]
        public decimal? NFertPersStdProPlatteM3 { get; set; }
        [Column("n_fert_pers_std_ek_kosten")]
        [Precision(7, 3)]
        public decimal? NFertPersStdEkKosten { get; set; }
        [Column("n_fert_pers_std_vk_kosten")]
        [Precision(7, 3)]
        public decimal? NFertPersStdVkKosten { get; set; }
        [Column("n_fert_mat_proz_kleinteile_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatProzKleinteileVk { get; set; }
        [Column("n_fert_mat_preis_kleinteile_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatPreisKleinteileVk { get; set; }
        [Column("n_fert_user_id_created")]
        public int? NFertUserIdCreated { get; set; }
        [Column("d_fert_created", TypeName = "datetime")]
        public DateTime? DFertCreated { get; set; }
        [Column("n_fert_user_id_updated")]
        public int? NFertUserIdUpdated { get; set; }
        [Column("d_fert_updated", TypeName = "datetime")]
        public DateTime? DFertUpdated { get; set; }
        [Column("n_fert_mat_deckelh_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatDeckelhEk { get; set; }
        [Column("n_fert_mat_deckelh_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatDeckelhVk { get; set; }
        [Column("n_fert_mat_deckelh_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatDeckelhVerschhnitt { get; set; }
    }
}
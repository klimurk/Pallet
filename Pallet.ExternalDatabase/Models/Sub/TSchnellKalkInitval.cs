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
    /// Schnellkalkulation - Die Werte der Verpackung, die bei der Kalkulation benutzt wurden
    /// </summary>
    [Table("t_schnell_kalk_initval")]
    public partial class TSchnellKalkInitval
    {
        [Key]
        [Column("c_verp_id")]
        [StringLength(45)]
        public string CVerpId { get; set; }
        /// <summary>
        /// t_schnell_kalk_initval_fertigung.n_id die ausgewählte Berechnungsmethode der Fertigung
        /// </summary>
        [Column("n_method_fert_id")]
        public int? NMethodFertId { get; set; }
        /// <summary>
        /// t_schnell_kalk_initval_verpackung.n_id die ausgewählte Berechnungsmethode der Verpackung
        /// </summary>
        [Column("n_method_verp_id")]
        public int? NMethodVerpId { get; set; }
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
        /// <summary>
        /// 1=m³, 2=zeitangabe
        /// </summary>
        [Column("n_fert_pers_calc_type")]
        public int? NFertPersCalcType { get; set; }
        [Column("n_fert_pers_min_zuschnitt")]
        public int? NFertPersMinZuschnitt { get; set; }
        [Column("n_fert_pers_min_bau")]
        public int? NFertPersMinBau { get; set; }
        [Column("n_fert_mat_deckelh_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatDeckelhEk { get; set; }
        [Column("n_fert_mat_deckelh_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatDeckelhVk { get; set; }
        [Column("n_fert_mat_deckelh_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NFertMatDeckelhVerschhnitt { get; set; }
        [Column("n_verp_mat_pe_folie_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatPeFolieEk { get; set; }
        [Column("n_verp_mat_pe_folie_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatPeFolieVk { get; set; }
        [Column("n_verp_mat_pe_folie_verschnitt")]
        [Precision(7, 3)]
        public decimal? NVerpMatPeFolieVerschnitt { get; set; }
        [Column("n_verp_mat_al_folie_verschnitt")]
        [Precision(12, 5)]
        public decimal? NVerpMatAlFolieVerschnitt { get; set; }
        [Column("n_verp_mat_vci_pe_folie_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciPeFolieEk { get; set; }
        [Column("n_verp_mat_vci_pe_folie_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciPeFolieVk { get; set; }
        [Column("n_verp_mat_vci_pe_folie_verschnitt")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciPeFolieVerschnitt { get; set; }
        [Column("n_verp_mat_vci_al_folie_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciAlFolieEk { get; set; }
        [Column("n_verp_mat_vci_al_folie_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciAlFolieVk { get; set; }
        [Column("n_verp_mat_vci_al_folie_verschnitt")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciAlFolieVerschnitt { get; set; }
        [Column("n_verp_mat_vci_produkt_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciProduktEk { get; set; }
        [Column("n_verp_mat_vci_produkt_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatVciProduktVk { get; set; }
        [Column("n_verp_mat_schrumpfh_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatSchrumpfhEk { get; set; }
        [Column("n_verp_mat_schrumpfh_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatSchrumpfhVk { get; set; }
        [Column("n_verp_mat_schrumpfh_verschhnitt")]
        [Precision(7, 3)]
        public decimal? NVerpMatSchrumpfhVerschhnitt { get; set; }
        [Column("n_verp_mat_al_folie_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatAlFolieVk { get; set; }
        [Column("n_verp_mat_al_folie_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatAlFolieEk { get; set; }
        [Column("n_verp_mat_trockenbeutel_ek")]
        [Precision(12, 5)]
        public decimal? NVerpMatTrockenbeutelEk { get; set; }
        [Column("n_verp_mat_trockenbeutel_vk")]
        [Precision(12, 5)]
        public decimal? NVerpMatTrockenbeutelVk { get; set; }
        [Column("n_verp_pers_std_pro_afl_m2")]
        [Precision(7, 3)]
        public decimal? NVerpPersStdProAflM2 { get; set; }
        [Column("n_verp_pers_min")]
        [Precision(10, 3)]
        public decimal? NVerpPersMin { get; set; }
        /// <summary>
        /// 1=m², 2=zeitangabe
        /// </summary>
        [Column("n_verp_pers_calc_type")]
        public int? NVerpPersCalcType { get; set; }
        [Column("n_verp_pers_std_ek_kosten")]
        [Precision(7, 3)]
        public decimal? NVerpPersStdEkKosten { get; set; }
        [Column("n_verp_pers_std_vk_kosten")]
        [Precision(7, 3)]
        public decimal? NVerpPersStdVkKosten { get; set; }
        [Column("n_verp_pers_std_fahrzeit")]
        [Precision(12, 2)]
        public decimal? NVerpPersStdFahrzeit { get; set; }
        [Column("n_fert_allgemein_kosten")]
        [Precision(7, 3)]
        public decimal? NFertAllgemeinKosten { get; set; }
        [Column("n_fert_sonstiges_preis")]
        [Precision(12, 5)]
        public decimal? NFertSonstigesPreis { get; set; }
        [Column("c_verp_sonstiges_text")]
        [StringLength(45)]
        public string CVerpSonstigesText { get; set; }
        [Column("n_verp_allg_kosten_ek")]
        [Precision(12, 5)]
        public decimal? NVerpAllgKostenEk { get; set; }
        [Column("n_verp_allg_kosten_vk")]
        [Precision(12, 5)]
        public decimal? NVerpAllgKostenVk { get; set; }
        [Column("c_fert_sonstiges_text")]
        [StringLength(45)]
        public string CFertSonstigesText { get; set; }
        [Column("n_fert_allg_kosten_ek")]
        [Precision(12, 5)]
        public decimal? NFertAllgKostenEk { get; set; }
        [Column("n_fert_allg_kosten_vk")]
        [Precision(12, 5)]
        public decimal? NFertAllgKostenVk { get; set; }
        [Column("n_fert_mat_beipack_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatBeipackVk { get; set; }
        [Column("n_fert_gesamtpreis_ohne_allg_ek")]
        [Precision(12, 3)]
        public decimal? NFertGesamtpreisOhneAllgEk { get; set; }
        [Column("n_fert_gesamtpreis_ohne_allg_vk")]
        [Precision(12, 3)]
        public decimal? NFertGesamtpreisOhneAllgVk { get; set; }
        [Column("n_verp_gesamtpreis_ohne_allg_vk")]
        [Precision(12, 3)]
        public decimal? NVerpGesamtpreisOhneAllgVk { get; set; }
        [Column("n_fert_gesamtpreis_beipack_ohne_allg_vk")]
        [Precision(12, 3)]
        public decimal? NFertGesamtpreisBeipackOhneAllgVk { get; set; }
        [Column("n_fert_gesamtpreis_ek")]
        [Precision(12, 5)]
        public decimal? NFertGesamtpreisEk { get; set; }
        [Column("n_fert_gesamtpreis_vk")]
        [Precision(12, 5)]
        public decimal? NFertGesamtpreisVk { get; set; }
        [Column("n_verp_gesamtpreis_ek")]
        [Precision(12, 5)]
        public decimal? NVerpGesamtpreisEk { get; set; }
        [Column("n_gesamtpreis_vk")]
        [Precision(12, 5)]
        public decimal? NGesamtpreisVk { get; set; }
        [Column("n_gesamtpreis_vk_zuschlag")]
        [Precision(12, 5)]
        public decimal? NGesamtpreisVkZuschlag { get; set; }
        [Column("n_verp_gesamtpreis_vk")]
        [Precision(12, 5)]
        public decimal? NVerpGesamtpreisVk { get; set; }
        [Column("n_gesamtpreis_ek")]
        [Precision(12, 5)]
        public decimal? NGesamtpreisEk { get; set; }
        [Column("n_gewinnzuschlag_proz")]
        [Precision(7, 3)]
        public decimal? NGewinnzuschlagProz { get; set; }
        [Column("n_gewinnzuschlag_betrag")]
        [Precision(12, 3)]
        public decimal? NGewinnzuschlagBetrag { get; set; }
        [Column("n_fert_kleinteil_ek_proz")]
        [Precision(7, 3)]
        public decimal? NFertKleinteilEkProz { get; set; }
        [Column("n_fert_kleinteil_ek_betrag")]
        [Precision(12, 3)]
        public decimal? NFertKleinteilEkBetrag { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime? DCreated { get; set; }
        [Column("d_changed", TypeName = "datetime")]
        public DateTime? DChanged { get; set; }
        [Column("n_verp_allgemein_kosten")]
        [Precision(7, 3)]
        public decimal? NVerpAllgemeinKosten { get; set; }
        [Column("n_verp_sonstiges_preis")]
        [Precision(12, 5)]
        public decimal? NVerpSonstigesPreis { get; set; }
        [Column("n_fert_qm_af_user")]
        [Precision(7, 2)]
        public decimal? NFertQmAfUser { get; set; }
        [Column("n_fert_qm_boden_user")]
        [Precision(7, 2)]
        public decimal? NFertQmBodenUser { get; set; }
        [Column("n_fert_qbm_user")]
        [Precision(7, 2)]
        public decimal? NFertQbmUser { get; set; }
        [Column("n_verp_qm_af_user")]
        [Precision(7, 2)]
        public decimal? NVerpQmAfUser { get; set; }
        [Column("n_verp_qm_boden_user")]
        [Precision(7, 2)]
        public decimal? NVerpQmBodenUser { get; set; }
        [Column("n_fert_mat_proz_kleinteile_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatProzKleinteileVk { get; set; }
        [Column("n_fert_mat_preis_kleinteile_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatPreisKleinteileVk { get; set; }
        [Column("n_fert_kleinteile_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertKleinteileFinalVk { get; set; }
        [Column("n_fert_pers_schnitt3_ek")]
        [Precision(12, 5)]
        public decimal? NFertPersSchnitt3Ek { get; set; }
        [Column("n_fert_pers_schnitt3_vk")]
        [Precision(12, 5)]
        public decimal? NFertPersSchnitt3Vk { get; set; }
        [Column("n_fert_pers_platte_ek")]
        [Precision(12, 5)]
        public decimal? NFertPersPlatteEk { get; set; }
        [Column("n_fert_pers_zuschnitt_ek")]
        [Precision(12, 5)]
        public decimal? NFertPersZuschnittEk { get; set; }
        [Column("n_fert_pers_zuschnitt_vk")]
        [Precision(12, 5)]
        public decimal? NFertPersZuschnittVk { get; set; }
        [Column("n_fert_pers_bau_ek")]
        [Precision(12, 5)]
        public decimal? NFertPersBauEk { get; set; }
        [Column("n_fert_pers_bau_vk")]
        [Precision(12, 5)]
        public decimal? NFertPersBauVk { get; set; }
        [Column("n_fert_pers_platte_vk")]
        [Precision(12, 5)]
        public decimal? NFertPersPlatteVk { get; set; }
        [Column("n_fert_mat_schnitt_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittFinalEk { get; set; }
        [Column("n_fert_mat_schnitt_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittFinalVk { get; set; }
        [Column("n_fert_mat_schnitt_kvh_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittKvhFinalEk { get; set; }
        [Column("n_fert_mat_schnitt_kvh_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittKvhFinalVk { get; set; }
        [Column("n_fert_mat_schnitt_beipack_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackFinalEk { get; set; }
        [Column("n_fert_mat_schnitt_beipack_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackFinalVk { get; set; }
        [Column("n_fert_mat_sperrholz_beipack_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzBeipackFinalEk { get; set; }
        [Column("n_fert_mat_sperrholz_beipack_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzBeipackFinalVk { get; set; }
        [Column("n_fert_mat_osb_beipack_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbBeipackFinalEk { get; set; }
        [Column("n_fert_mat_siebdruck_beipack_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckBeipackFinalEk { get; set; }
        [Column("n_fert_mat_siebdruck_beipack_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckBeipackFinalVk { get; set; }
        [Column("n_fert_mat_osb_beipack_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbBeipackFinalVk { get; set; }
        [Column("n_fert_mat_schnitt_beipackK_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackKFinalEk { get; set; }
        [Column("n_fert_mat_schnitt_beipackK_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSchnittBeipackKFinalVk { get; set; }
        [Column("n_fert_mat_sperrholz_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzFinalEk { get; set; }
        [Column("n_fert_mat_sperrholz_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSperrholzFinalVk { get; set; }
        [Column("n_fert_mat_osb_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbFinalEk { get; set; }
        [Column("n_fert_mat_osb_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatOsbFinalVk { get; set; }
        [Column("n_fert_mat_siebdruck_final_ek")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckFinalEk { get; set; }
        [Column("n_fert_mat_siebdruck_final_vk")]
        [Precision(12, 5)]
        public decimal? NFertMatSiebdruckFinalVk { get; set; }
    }
}
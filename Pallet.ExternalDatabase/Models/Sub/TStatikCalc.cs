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
    /// Berechnete statische Werte einer Verpackung
    /// </summary>
    [Table("t_statik_calc")]
    public partial class TStatikCalc
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        [Column("n_verp_id")]
        public int? NVerpId { get; set; }
        [Column("n_verp_typ_id")]
        public int NVerpTypId { get; set; }
        [Column("n_lk_pos")]
        public short NLkPos { get; set; }
        [Column("n_lk_belastung_tara")]
        [Precision(10, 2)]
        public decimal NLkBelastungTara { get; set; }
        [Column("n_lk_belastung_brutto")]
        [Precision(10, 2)]
        public decimal NLkBelastungBrutto { get; set; }
        [Column("n_schwerpkt_x")]
        [Precision(10, 2)]
        public decimal NSchwerpktX { get; set; }
        [Column("n_schwerpkt_y")]
        [Precision(10, 2)]
        public decimal NSchwerpktY { get; set; }
        [Column("n_schwerpkt_z")]
        [Precision(10, 2)]
        public decimal NSchwerpktZ { get; set; }
        [Column("n_schwerpkt_lk_x")]
        [Precision(10, 2)]
        public decimal NSchwerpktLkX { get; set; }
        [Column("n_tara")]
        [Precision(10, 2)]
        public decimal NTara { get; set; }
        [Column("n_brutto")]
        [Precision(10, 2)]
        public decimal NBrutto { get; set; }
        [Column("n_lagerkraft_a")]
        [Precision(10, 2)]
        public decimal NLagerkraftA { get; set; }
        [Column("n_lagerkraft_b")]
        [Precision(10, 2)]
        public decimal NLagerkraftB { get; set; }
        [Column("n_max_biegemoment")]
        [Precision(10, 2)]
        public decimal NMaxBiegemoment { get; set; }
        [Column("n_max_biegemoment_quer")]
        [Precision(10, 2)]
        public decimal? NMaxBiegemomentQuer { get; set; }
        [Column("n_druck_sigma_user")]
        [Precision(10, 2)]
        public decimal? NDruckSigmaUser { get; set; }
        [Column("n_druck_sigma")]
        [Precision(10, 2)]
        public decimal? NDruckSigma { get; set; }
        [Column("n_druck_ausnutz_grad_user")]
        [Precision(10, 2)]
        public decimal? NDruckAusnutzGradUser { get; set; }
        [Column("n_druck_ausnutz_grad")]
        [Precision(10, 2)]
        public decimal? NDruckAusnutzGrad { get; set; }
        [Column("n_st_lamda")]
        [Precision(10, 2)]
        public decimal? NStLamda { get; set; }
        [Column("n_st_jx")]
        [Precision(10, 2)]
        public decimal? NStJx { get; set; }
        [Column("n_st_jy")]
        [Precision(10, 2)]
        public decimal? NStJy { get; set; }
        [Column("n_st_knick")]
        [Precision(10, 2)]
        public decimal? NStKnick { get; set; }
        [Column("n_st_knick_dmax")]
        [Precision(10, 2)]
        public decimal? NStKnickDmax { get; set; }
        [Column("n_st_knick_dmax_vergleich")]
        [Precision(10, 2)]
        public decimal? NStKnickDmaxVergleich { get; set; }
        [Column("n_st_festigkeit")]
        [Precision(10, 2)]
        public decimal? NStFestigkeit { get; set; }
        [Column("n_st_festigkeit_vergleich")]
        [Precision(10, 2)]
        public decimal? NStFestigkeitVergleich { get; set; }
        [Column("n_st_kc")]
        [Precision(10, 2)]
        public decimal? NStKc { get; set; }
        [Column("n_st_kh")]
        [Precision(10, 2)]
        public decimal? NStKh { get; set; }
        [Column("n_st_gamma_m")]
        [Precision(10, 2)]
        public decimal? NStGammaM { get; set; }
        [Column("n_st_gamma_f")]
        [Precision(10, 2)]
        public decimal? NStGammaF { get; set; }
        [Column("n_st_gamma_q")]
        [Precision(10, 2)]
        public decimal? NStGammaQ { get; set; }
        [Column("n_kranpos_a")]
        [Precision(10, 2)]
        public decimal NKranposA { get; set; }
        [Column("n_kranpos_b")]
        [Precision(10, 2)]
        public decimal NKranposB { get; set; }
        [Column("n_holzklasse_sigma_bruch")]
        [Precision(10, 2)]
        public decimal NHolzklasseSigmaBruch { get; set; }
        [Column("c_holzklasse_bez")]
        [StringLength(45)]
        public string CHolzklasseBez { get; set; }
        [Column("n_holzklasse_biegung")]
        [Precision(10, 2)]
        public decimal? NHolzklasseBiegung { get; set; }
        [Column("n_lk_sel_breite")]
        [Precision(10, 2)]
        public decimal? NLkSelBreite { get; set; }
        [Column("n_lk_sel_hoehe")]
        [Precision(10, 2)]
        public decimal? NLkSelHoehe { get; set; }
        [Column("n_lk_sel_wider_x")]
        [Precision(10, 2)]
        public decimal? NLkSelWiderX { get; set; }
        [Column("n_lk_sel_wider_y")]
        [Precision(10, 2)]
        public decimal? NLkSelWiderY { get; set; }
        [Column("n_boden_sel_staerke")]
        [Precision(10, 2)]
        public decimal? NBodenSelStaerke { get; set; }
        [Column("n_lk_sel_breite_user")]
        [Precision(10, 2)]
        public decimal? NLkSelBreiteUser { get; set; }
        [Column("n_lk_sel_hoehe_user")]
        [Precision(10, 2)]
        public decimal? NLkSelHoeheUser { get; set; }
        [Column("n_boden_sel_staerke_user")]
        [Precision(10, 2)]
        public decimal? NBodenSelStaerkeUser { get; set; }
        [Column("n_lk_sel_wider_x_user")]
        [Precision(10, 2)]
        public decimal? NLkSelWiderXUser { get; set; }
        [Column("n_lk_sel_wider_y_user")]
        [Precision(10, 2)]
        public decimal? NLkSelWiderYUser { get; set; }
        [Column("n_lk_auslastung")]
        [Precision(6, 2)]
        public decimal? NLkAuslastung { get; set; }
        [Column("n_boden_auslastung")]
        [Precision(6, 2)]
        public decimal? NBodenAuslastung { get; set; }
        [Column("n_lk_auslastung_user")]
        [Precision(6, 2)]
        public decimal? NLkAuslastungUser { get; set; }
        [Column("n_boden_auslastung_user")]
        [Precision(6, 2)]
        public decimal? NBodenAuslastungUser { get; set; }
        [Column("n_holzklasse_biegung_user")]
        [Precision(10, 2)]
        public decimal NHolzklasseBiegungUser { get; set; }
        [Column("c_holzklasse_bez_user")]
        [StringLength(45)]
        public string CHolzklasseBezUser { get; set; }
        [Column("c_weg_bez")]
        [StringLength(45)]
        public string CWegBez { get; set; }
        /// <summary>
        /// Teilsicherheitsbeiwert Gamma F
        /// </summary>
        [Column("n_weg_gamma_f")]
        [Precision(10, 3)]
        public decimal NWegGammaF { get; set; }
        /// <summary>
        /// Teilsicherheitsbeiwert Gamma Q
        /// </summary>
        [Column("n_weg_gamma_q")]
        [Precision(10, 3)]
        public decimal NWegGammaQ { get; set; }
        /// <summary>
        /// Teilsicherheitsbeiwert Gamma M
        /// </summary>
        [Column("n_weg_gamma_m")]
        [Precision(10, 3)]
        public decimal NWegGammaM { get; set; }
        /// <summary>
        /// Querdruckbeiwert bei Schwellendruck für Nadelholz
        /// </summary>
        [Column("n_weg_schwellendruck_k_90")]
        [Precision(10, 3)]
        public decimal NWegSchwellendruckK90 { get; set; }
        /// <summary>
        /// Querdruckbeiwert bei Auflagendruck für Nadelholz
        /// </summary>
        [Column("n_weg_auflagedruck_k_90")]
        [Precision(10, 3)]
        public decimal NWegAuflagedruckK90 { get; set; }
        /// <summary>
        /// Modifikationsbeiwert
        /// </summary>
        [Column("n_weg_k_mod")]
        [Precision(10, 3)]
        public decimal NWegKMod { get; set; }
        [Column("n_biegung_din1052")]
        [Precision(10, 2)]
        public decimal? NBiegungDin1052 { get; set; }
        /// <summary>
        /// Tabelle 4
        /// </summary>
        [Column("n_biegung_kennwert")]
        [Precision(10, 2)]
        public decimal NBiegungKennwert { get; set; }
        [Column("n_laengsdruck")]
        [Precision(10, 2)]
        public decimal NLaengsdruck { get; set; }
        [Column("n_querdruck")]
        [Precision(10, 2)]
        public decimal NQuerdruck { get; set; }
        [Column("n_rohdichte")]
        [Precision(10, 2)]
        public decimal NRohdichte { get; set; }
        [Column("n_dh_sel_breite_user")]
        [Precision(7, 2)]
        public decimal? NDhSelBreiteUser { get; set; }
        [Column("n_dh_sel_hoehe_user")]
        [Precision(7, 2)]
        public decimal? NDhSelHoeheUser { get; set; }
        [Column("n_dh_sel_anzahl_user")]
        public int? NDhSelAnzahlUser { get; set; }
        [Column("n_dh_sel_lastqm_user")]
        public int? NDhSelLastqmUser { get; set; }
        [Column("n_dh_widermoment")]
        [Precision(10, 2)]
        public decimal? NDhWidermoment { get; set; }
        [Column("n_dh_widermoment_erf")]
        [Precision(10, 2)]
        public decimal? NDhWidermomentErf { get; set; }
        [Column("n_dh_sel_breite")]
        [Precision(7, 2)]
        public decimal? NDhSelBreite { get; set; }
        [Column("n_dh_sel_hoehe")]
        [Precision(7, 2)]
        public decimal? NDhSelHoehe { get; set; }
        [Column("n_dh_sel_anzahl")]
        public int? NDhSelAnzahl { get; set; }
        [Column("n_dh_sel_lastqm")]
        public int? NDhSelLastqm { get; set; }
        [Column("n_qk_pos")]
        public short? NQkPos { get; set; }
        [Column("c_qk_kran_a")]
        [StringLength(2)]
        public string CQkKranA { get; set; }
        [Column("c_qk_kran_b")]
        [StringLength(2)]
        public string CQkKranB { get; set; }
        [Column("n_qk_sel_breite_user")]
        [Precision(10, 2)]
        public decimal? NQkSelBreiteUser { get; set; }
        [Column("n_qk_sel_hoehe_user")]
        [Precision(10, 2)]
        public decimal? NQkSelHoeheUser { get; set; }
        [Column("n_st_sel_breite_user")]
        [Precision(10, 2)]
        public decimal? NStSelBreiteUser { get; set; }
        [Column("n_st_sel_hoehe_user")]
        [Precision(10, 2)]
        public decimal? NStSelHoeheUser { get; set; }
        [Column("n_st_sel_breite")]
        [Precision(10, 2)]
        public decimal? NStSelBreite { get; set; }
        [Column("n_st_sel_hoehe")]
        [Precision(10, 2)]
        public decimal? NStSelHoehe { get; set; }
        [Column("n_seitenleisten_hoehe")]
        [Precision(10, 2)]
        public decimal? NSeitenleistenHoehe { get; set; }
    }
}
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
    /// Konstruktionsoptionen zu einem Verpackungstypen
    /// </summary>
    [Table("t_init_konstr_value")]
    [Index("NVerpTypId", Name = "IDX_VERP_TYP")]
    public partial class TInitKonstrValue
    {
        [Key]
        [Column("n_verp_typ_id")]
        public int NVerpTypId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(25)]
        public string CName { get; set; }
        /// <summary>
        /// 1=Ist Standrad -&gt; wird geladen, wenn der Verpackungstyp ausgewählt wurde
        /// </summary>
        [Column("n_is_standard")]
        public bool? NIsStandard { get; set; }
        /// <summary>
        /// Kistentyp-Steckkiste für VTT Verbinder
        /// </summary>
        [Column("n_kstr_is_steckkiste")]
        public bool? NKstrIsSteckkiste { get; set; }
        [Column("n_kstr_kl_senkrecht")]
        public bool? NKstrKlSenkrecht { get; set; }
        /// <summary>
        /// Kopfleisten um Seite und SeitenL einruecken
        /// </summary>
        [Column("n_kstr_kl_um_s_und_sl_einrueck")]
        public bool? NKstrKlUmSUndSlEinrueck { get; set; }
        /// <summary>
        /// Obere ZL nicht nutzen
        /// </summary>
        [Column("n_kstr_zl_ohne_oben")]
        public int? NKstrZlOhneOben { get; set; }
        /// <summary>
        /// Kopfleisten auf Zwischenlage bei 4-Wegeboden
        /// </summary>
        [Column("n_kstr_kl_auf_zwischenl")]
        public bool? NKstrKlAufZwischenl { get; set; }
        /// <summary>
        /// Kopf und Seit mit Z-Fuge verbinden
        /// </summary>
        [Column("n_kstr_k_s_zfuge")]
        public bool? NKstrKSZfuge { get; set; }
        /// <summary>
        /// Kopf zwischen Seiten (Z-Fuge)
        /// </summary>
        [Column("n_kstr_k_zwischen_s")]
        public bool? NKstrKZwischenS { get; set; }
        /// <summary>
        /// Wenn Qk einruecken 0 Kopf bis auf Boden
        /// </summary>
        [Column("n_kstr_k_qk0_kopf_auf_boden")]
        public bool? NKstrKQk0KopfAufBoden { get; set; }
        [Column("n_kstr_kl_oben_ueberstehen")]
        public bool? NKstrKlObenUeberstehen { get; set; }
        /// <summary>
        /// Kopfleisten bis Boden bei Qk-Einrueck=0
        /// </summary>
        [Column("n_kstr_kl_bis_boden_bei_qk0")]
        public bool? NKstrKlBisBodenBeiQk0 { get; set; }
        [Column("n_kstr_kl_unten_ueberstehen")]
        public bool? NKstrKlUntenUeberstehen { get; set; }
        /// <summary>
        /// KL bis unten zu QK wenn diese buendig
        /// </summary>
        [Column("n_kstr_kl_bis_qk_wenn_qkeinrueck_null")]
        public bool? NKstrKlBisQkWennQkeinrueckNull { get; set; }
        /// <summary>
        /// Kopfleisten nach aussen
        /// </summary>
        [Column("n_kstr_k_la")]
        public bool? NKstrKLa { get; set; }
        /// <summary>
        /// KL oben buendig
        /// </summary>
        [Column("n_kstr_kl_oben_buendig")]
        public bool? NKstrKlObenBuendig { get; set; }
        [Column("n_kstr_k_ohne_zl")]
        public bool? NKstrKOhneZl { get; set; }
        [Column("n_kstr_seite_auf_qk")]
        public bool? NKstrSeiteAufQk { get; set; }
        /// <summary>
        /// Nur 2 waag. laufende Seitenleisten
        /// </summary>
        [Column("n_kstr_sl_2_waag")]
        public bool? NKstrSl2Waag { get; set; }
        /// <summary>
        /// Seite ohne Futterleisten
        /// </summary>
        [Column("n_kstr_ohne_fl")]
        public bool? NKstrOhneFl { get; set; }
        /// <summary>
        /// Seitenleisten aussen
        /// </summary>
        [Column("n_kstr_li_laseiten")]
        public bool? NKstrLiLaseiten { get; set; }
        /// <summary>
        /// Seite bis auf den Boden
        /// </summary>
        [Column("n_kstr_s_bis_auf_boden")]
        public bool? NKstrSBisAufBoden { get; set; }
        /// <summary>
        /// Seitenleisten neben Qk
        /// </summary>
        [Column("n_kstr_seite_sl_neben_qk")]
        public bool? NKstrSeiteSlNebenQk { get; set; }
        /// <summary>
        /// QK immer um SL kuerzen
        /// </summary>
        [Column("n_kstr_seite_sl_waag_innen")]
        public bool? NKstrSeiteSlWaagInnen { get; set; }
        /// <summary>
        /// Deckel ohne Deckelkopfleisten
        /// </summary>
        [Column("n_kstr_seite_sl_waag_aussen")]
        public bool? NKstrSeiteSlWaagAussen { get; set; }
        /// <summary>
        /// QK immer um SL kuerzen
        /// </summary>
        [Column("n_kstr_qk_um_sl_kuerzen")]
        public bool? NKstrQkUmSlKuerzen { get; set; }
        /// <summary>
        /// Seitenruecksprung bei LK
        /// </summary>
        [Column("n_kstr_seite_ruecksprung")]
        public int? NKstrSeiteRuecksprung { get; set; }
        [Column("n_kstr_seite_sl_innen")]
        public bool? NKstrSeiteSlInnen { get; set; }
        [Column("n_kstr_bbretter_seitlich_einrueck")]
        public bool? NKstrBbretterSeitlichEinrueck { get; set; }
        [Column("n_kstr_fl_ueber_qk")]
        public bool? NKstrFlUeberQk { get; set; }
        [Column("n_kstr_deckel_dl_ueber_sl")]
        public bool? NKstrDeckelDlUeberSl { get; set; }
        /// <summary>
        /// Deckelleisten quer laufen lassen
        /// </summary>
        [Column("n_kstr_deckell_quer")]
        public bool? NKstrDeckellQuer { get; set; }
        /// <summary>
        /// Deckelleisten quer laufen lassen
        /// </summary>
        [Column("n_kstr_deckell_aussen")]
        public bool? NKstrDeckellAussen { get; set; }
        /// <summary>
        /// Deckel ohne Deckelkopfleisten
        /// </summary>
        [Column("n_kstr_deckelkl_ohne")]
        public bool? NKstrDeckelklOhne { get; set; }
        /// <summary>
        /// Anzahl Deckelseitenleiste wie LK Anzahl
        /// </summary>
        [Column("n_kstr_deckelslanz_wie_lkanz")]
        public bool? NKstrDeckelslanzWieLkanz { get; set; }
        /// <summary>
        /// Deckel zwischen den Seiten
        /// </summary>
        [Column("n_kstr_deckel_zwischen_s")]
        public bool? NKstrDeckelZwischenS { get; set; }
        /// <summary>
        /// Deckel zwischen den Koepfen
        /// </summary>
        [Column("n_kstr_deckel_zwischen_k")]
        public bool? NKstrDeckelZwischenK { get; set; }
        /// <summary>
        /// Deckelholz nutzen
        /// </summary>
        [Column("n_kstrh_deckel_dh")]
        public bool? NKstrhDeckelDh { get; set; }
        /// <summary>
        /// Deckelkopfleisten wie QK einruecken
        /// </summary>
        [Column("n_kstr_dkl_ueber_qk")]
        public bool? NKstrDklUeberQk { get; set; }
        [Column("n_kstr_bbretter_unter_k")]
        public bool? NKstrBbretterUnterK { get; set; }
        /// <summary>
        /// Bodenbretter bis unter KL
        /// </summary>
        [Column("n_kstr_bbretter_unter_kl")]
        public bool? NKstrBbretterUnterKl { get; set; }
        /// <summary>
        /// Bodenbretter bis unter Bh
        /// </summary>
        [Column("n_kstr_bbretter_unter_bh")]
        public bool? NKstrBbretterUnterBh { get; set; }
        [Column("n_kstr_kl_einrueck_bh")]
        public bool? NKstrKlEinrueckBh { get; set; }
        [Column("n_kstr_qklaenge_aussenbreite")]
        public bool? NKstrQklaengeAussenbreite { get; set; }
        [Column("n_kstr_qk_ueber_lk")]
        public bool? NKstrQkUeberLk { get; set; }
        /// <summary>
        /// Kopfleistenstaerke
        /// </summary>
        [Column("n_kstrh_kopf_l_staerke")]
        public int? NKstrhKopfLStaerke { get; set; }
        /// <summary>
        /// Kopfleistenstaerke
        /// </summary>
        [Column("n_kstrh_kopf_l_breite")]
        public int? NKstrhKopfLBreite { get; set; }
        /// <summary>
        /// ZL Raster
        /// </summary>
        [Column("n_kstrh_zl_raster")]
        public int? NKstrhZlRaster { get; set; }
        /// <summary>
        /// Seitenleistenstaerke
        /// </summary>
        [Column("n_kstrh_seite_l_staerke")]
        public int? NKstrhSeiteLStaerke { get; set; }
        /// <summary>
        /// Seitenleistenstaerke
        /// </summary>
        [Column("n_kstrh_seite_l_breite")]
        public int? NKstrhSeiteLBreite { get; set; }
        /// <summary>
        /// FL Raster
        /// </summary>
        [Column("n_kstrh_fl_raster")]
        public int? NKstrhFlRaster { get; set; }
        /// <summary>
        /// Deckelleistenstaerke
        /// </summary>
        [Column("n_kstrh_deckel_l_staerke")]
        public int? NKstrhDeckelLStaerke { get; set; }
        /// <summary>
        /// Deckelleistenstaerke
        /// </summary>
        [Column("n_kstrh_deckel_l_breite")]
        public int? NKstrhDeckelLBreite { get; set; }
        /// <summary>
        /// Innenbreite zwischen Wand zu Wand. 1=dann wird die eingegeben Innenbreite anders interpretiert.
        /// </summary>
        [Column("n_kstr_ibreite_wand_zu_wand")]
        public bool? NKstrIbreiteWandZuWand { get; set; }
    }
}
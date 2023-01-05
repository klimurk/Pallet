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
    /// Transportaufträge
    /// </summary>
    [Table("t_transport_auftrag")]
    public partial class TTransportAuftrag
    {
        [Key]
        [Column("n_auftrag_id")]
        public int NAuftragId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_nr")]
        [StringLength(25)]
        public string CNr { get; set; }
        [Column("n_kunde_ansprechp_id")]
        public int? NKundeAnsprechpId { get; set; }
        [Column("n_is_anfrage")]
        public bool NIsAnfrage { get; set; }
        [Column("c_transporteur")]
        [StringLength(50)]
        public string CTransporteur { get; set; }
        [Column("c_transporteur_strasse")]
        [StringLength(40)]
        public string CTransporteurStrasse { get; set; }
        [Column("c_transporteur_strassenr")]
        [StringLength(20)]
        public string CTransporteurStrassenr { get; set; }
        [Column("c_transporteur_plz")]
        [StringLength(15)]
        public string CTransporteurPlz { get; set; }
        [Column("c_transporteur_ort")]
        [StringLength(40)]
        public string CTransporteurOrt { get; set; }
        [Column("c_transporteur_zu_haenden")]
        [StringLength(40)]
        public string CTransporteurZuHaenden { get; set; }
        [Column("c_lade_firma")]
        [StringLength(50)]
        public string CLadeFirma { get; set; }
        [Column("c_lade_strasse")]
        [StringLength(40)]
        public string CLadeStrasse { get; set; }
        [Column("c_lade_strassenr")]
        [StringLength(20)]
        public string CLadeStrassenr { get; set; }
        [Column("c_lade_plz")]
        [StringLength(15)]
        public string CLadePlz { get; set; }
        [Column("c_lade_ort")]
        [StringLength(40)]
        public string CLadeOrt { get; set; }
        [Column("n_lade_staat_id")]
        public int? NLadeStaatId { get; set; }
        [Column("c_lade_adr_zusatz_1")]
        [StringLength(45)]
        public string CLadeAdrZusatz1 { get; set; }
        [Column("d_lade_termin", TypeName = "datetime")]
        public DateTime? DLadeTermin { get; set; }
        [Column("c_lade_termin_prefix")]
        [StringLength(10)]
        public string CLadeTerminPrefix { get; set; }
        [Column("c_lade_zeit")]
        [StringLength(25)]
        public string CLadeZeit { get; set; }
        [Column("c_empf_firma")]
        [StringLength(50)]
        public string CEmpfFirma { get; set; }
        [Column("c_empf_strasse")]
        [StringLength(40)]
        public string CEmpfStrasse { get; set; }
        [Column("c_empf_strassenr")]
        [StringLength(20)]
        public string CEmpfStrassenr { get; set; }
        [Column("c_empf_plz")]
        [StringLength(15)]
        public string CEmpfPlz { get; set; }
        [Column("c_empf_ort")]
        [StringLength(40)]
        public string CEmpfOrt { get; set; }
        [Column("n_empf_staat_id")]
        public int? NEmpfStaatId { get; set; }
        [Column("c_empf_adr_zusatz_1")]
        [StringLength(45)]
        public string CEmpfAdrZusatz1 { get; set; }
        [Column("d_empf_termin", TypeName = "datetime")]
        public DateTime? DEmpfTermin { get; set; }
        [Column("c_empf_zeit")]
        [StringLength(25)]
        public string CEmpfZeit { get; set; }
        [Column("c_empf_termin_prefix")]
        [StringLength(10)]
        public string CEmpfTerminPrefix { get; set; }
        [Column("c_frachtzahler")]
        [StringLength(50)]
        public string CFrachtzahler { get; set; }
        [Column("c_frankatur")]
        [StringLength(50)]
        public string CFrankatur { get; set; }
        [Column("c_frachtvereinbarung")]
        [StringLength(60)]
        public string CFrachtvereinbarung { get; set; }
        [Column("m_bemerkung", TypeName = "mediumtext")]
        public string MBemerkung { get; set; }
        [Column("m_bemerkung_kunde", TypeName = "text")]
        public string MBemerkungKunde { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
        [Column("d_gedruckt", TypeName = "datetime")]
        public DateTime? DGedruckt { get; set; }
        [Column("n_user_id")]
        public int NUserId { get; set; }
        [Column("d_deleted", TypeName = "datetime")]
        public DateTime? DDeleted { get; set; }
        [Column("c_transporteur_faxnr")]
        [StringLength(20)]
        public string CTransporteurFaxnr { get; set; }
        [Column("c_auftraggeber")]
        [StringLength(40)]
        public string CAuftraggeber { get; set; }
        [Column("c_lade_ansprechpartner")]
        [StringLength(40)]
        public string CLadeAnsprechpartner { get; set; }
        [Column("c_lade_telefon")]
        [StringLength(30)]
        public string CLadeTelefon { get; set; }
        [Column("m_lade_bemerkung", TypeName = "mediumtext")]
        public string MLadeBemerkung { get; set; }
        [Column("n_einzelpreis")]
        [Precision(19, 2)]
        public decimal? NEinzelpreis { get; set; }
        [Column("n_einzelpreis_ek")]
        [Precision(19, 2)]
        public decimal? NEinzelpreisEk { get; set; }
        /// <summary>
        /// kat_grp=25
        /// </summary>
        [Column("n_waehrung_id")]
        public int? NWaehrungId { get; set; }
        [Required]
        [Column("n_aktiv")]
        public bool? NAktiv { get; set; }
        [Column("c_empf_ansprechpartner")]
        [StringLength(40)]
        public string CEmpfAnsprechpartner { get; set; }
        [Column("c_empf_telefon")]
        [StringLength(30)]
        public string CEmpfTelefon { get; set; }
        [Column("m_empf_bemerkung", TypeName = "mediumtext")]
        public string MEmpfBemerkung { get; set; }
        [Column("n_anzahl_rechnung")]
        public int? NAnzahlRechnung { get; set; }
        [Column("n_anzahl_nicht_verrechenbar")]
        public int NAnzahlNichtVerrechenbar { get; set; }
        [Column("n_anzahl_auftragbestaet")]
        public int NAnzahlAuftragbestaet { get; set; }
        [Column("n_anzahl_angeboten")]
        public int NAnzahlAngeboten { get; set; }
        [Column("n_anzahl_freigabe")]
        public int? NAnzahlFreigabe { get; set; }
        [Column("n_transporteur_id")]
        public int? NTransporteurId { get; set; }
        [Column("n_ladestelle_id")]
        public int? NLadestelleId { get; set; }
        [Column("n_entladestelle_id")]
        public int? NEntladestelleId { get; set; }
        [Column("c_lade_termin")]
        [StringLength(45)]
        public string CLadeTermin { get; set; }
        [Column("c_empf_termin")]
        [StringLength(45)]
        public string CEmpfTermin { get; set; }
        /// <summary>
        /// 1-Lieferung, 1-Abholung
        /// </summary>
        [Column("n_trans_art")]
        public int? NTransArt { get; set; }
        /// <summary>
        /// kat_grp = 140
        /// </summary>
        [Column("n_transport_weg_id")]
        public int? NTransportWegId { get; set; }
        /// <summary>
        /// Transfered to external application
        /// </summary>
        [Column("d_transfered", TypeName = "datetime")]
        public DateTime? DTransfered { get; set; }
        /// <summary>
        /// user who start the transfer
        /// </summary>
        [Column("n_transfered_user_id")]
        public uint? NTransferedUserId { get; set; }
        [Column("n_user_id_created")]
        public int? NUserIdCreated { get; set; }
        [Column("n_user_id_updated")]
        public int? NUserIdUpdated { get; set; }
        [Column("n_user_id_printed")]
        public int? NUserIdPrinted { get; set; }
        /// <summary>
        /// t_kostenstelle
        /// </summary>
        [Column("n_kostenstelle_id")]
        public int? NKostenstelleId { get; set; }
    }
}
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
    /// SOLAS Dokument für den Containerstau
    /// </summary>
    [Table("t_solas_dok")]
    public partial class TSolasDok
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        /// <summary>
        /// t_auftrag.n_id
        /// </summary>
        [Column("n_auftrag_id")]
        public int NAuftragId { get; set; }
        /// <summary>
        /// n_lieferschein.n_id Referenz zum Lieferschein aus dem die Daten geladen wurden.
        /// </summary>
        [Column("n_lieferschein_id")]
        public int? NLieferscheinId { get; set; }
        [Column("c_cont_nr")]
        [StringLength(25)]
        public string CContNr { get; set; }
        [Column("c_cont_tuersiegel")]
        [StringLength(25)]
        public string CContTuersiegel { get; set; }
        [Column("c_cont_dachsiegel")]
        [StringLength(25)]
        public string CContDachsiegel { get; set; }
        [Column("n_cont_tara_gewicht")]
        public int? NContTaraGewicht { get; set; }
        [Column("n_cont_gewicht_abgelesen")]
        public bool? NContGewichtAbgelesen { get; set; }
        [Column("n_ladung_verpmat_gewicht")]
        public int? NLadungVerpmatGewicht { get; set; }
        [Column("n_ladung_verpmat_gewicht_gewogen")]
        public bool? NLadungVerpmatGewichtGewogen { get; set; }
        [Column("n_verpmat_gewicht")]
        public int? NVerpmatGewicht { get; set; }
        [Column("n_verpmat_gewicht_gewogen")]
        public bool? NVerpmatGewichtGewogen { get; set; }
        [Column("n_ladung_gewicht")]
        public int? NLadungGewicht { get; set; }
        [Column("n_ladung_gewicht_gewogen")]
        public bool? NLadungGewichtGewogen { get; set; }
        [Column("n_ladung_sich_gewicht")]
        public int? NLadungSichGewicht { get; set; }
        [Column("n_ladung_sich_theo_zurrgurte_gewicht")]
        public int? NLadungSichTheoZurrgurteGewicht { get; set; }
        [Column("n_ladung_sich_theo_zurrgurte_anzahl")]
        public int? NLadungSichTheoZurrgurteAnzahl { get; set; }
        [Column("n_ladung_sich_theo_saecke_gewicht")]
        public int? NLadungSichTheoSaeckeGewicht { get; set; }
        [Column("n_ladung_sich_theo_saecke_anzahl")]
        public int? NLadungSichTheoSaeckeAnzahl { get; set; }
        [Column("n_ladung_sich_theo_antirutsch_gewicht")]
        public int? NLadungSichTheoAntirutschGewicht { get; set; }
        [Column("n_ladung_sich_theo_antirutsch_anzahl")]
        public int? NLadungSichTheoAntirutschAnzahl { get; set; }
        [Column("n_ladung_sich_theo_holz_gewicht")]
        public int? NLadungSichTheoHolzGewicht { get; set; }
        [Column("n_ladung_sich_theo_sonstige_gewicht")]
        public int? NLadungSichTheoSonstigeGewicht { get; set; }
        [Column("n_user_id")]
        public int? NUserId { get; set; }
        [Column("d_datum")]
        public DateOnly? DDatum { get; set; }
        [Column("d_erstellt", TypeName = "datetime")]
        public DateTime? DErstellt { get; set; }
        [Column("d_geaendert", TypeName = "datetime")]
        public DateTime? DGeaendert { get; set; }
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
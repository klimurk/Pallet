﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pallet.ExternalDatabase.Models
{
    [Table("t_packliste_packstuecke_main")]
    [Index("NKundeId", Name = "IDX_KUNDE")]
    public partial class TPacklistePackstueckeMain
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("n_auftrag_id")]
        public int? NAuftragId { get; set; }
        [Column("c_nr")]
        [StringLength(15)]
        public string CNr { get; set; }
        [Column("c_bezeichnung")]
        [StringLength(40)]
        public string CBezeichnung { get; set; }
        [Column("c_kunde_nr")]
        [StringLength(25)]
        public string CKundeNr { get; set; }
        [Column("c_kunde_projekt")]
        [StringLength(40)]
        public string CKundeProjekt { get; set; }
        [Column("m_bemerkung", TypeName = "text")]
        public string MBemerkung { get; set; }
        [Column("n_cont_typ_id")]
        public int? NContTypId { get; set; }
        [Column("c_cont_tuersiegel")]
        [StringLength(25)]
        public string CContTuersiegel { get; set; }
        [Column("c_cont_dachsiegel")]
        [StringLength(25)]
        public string CContDachsiegel { get; set; }
        [Column("c_cont_nr")]
        [StringLength(25)]
        public string CContNr { get; set; }
        [Column("n_cont_tara")]
        public int? NContTara { get; set; }
        [Column("c_lkw_kennz")]
        [StringLength(25)]
        public string CLkwKennz { get; set; }
        [Column("n_ladung_sich_gewicht")]
        public int? NLadungSichGewicht { get; set; }
        [Column("n_user_id")]
        public int? NUserId { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
    }
}
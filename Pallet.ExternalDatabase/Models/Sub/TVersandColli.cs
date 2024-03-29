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
    /// Losteilsystem - Aktuell deaktiviert
    /// </summary>
    [Table("t_versand_colli")]
    public partial class TVersandColli
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("n_auftrag_id")]
        public int NAuftragId { get; set; }
        [Column("c_lkw_transporteur")]
        [StringLength(45)]
        public string CLkwTransporteur { get; set; }
        [Column("c_lkw_fahrer")]
        [StringLength(100)]
        public string CLkwFahrer { get; set; }
        [Column("c_lkw_kennzeichen")]
        [StringLength(20)]
        public string CLkwKennzeichen { get; set; }
        [Column("c_lkw_anhaneger_kennzeichen")]
        [StringLength(20)]
        public string CLkwAnhanegerKennzeichen { get; set; }
        [Column("n_lkw_tara")]
        public int? NLkwTara { get; set; }
        [Column("c_cont_nr")]
        [StringLength(45)]
        public string CContNr { get; set; }
        [Column("n_cont_typ_id")]
        public int? NContTypId { get; set; }
        [Column("n_cont_tara")]
        public int? NContTara { get; set; }
        [Column("c_cont_tuersiegel")]
        [StringLength(25)]
        public string CContTuersiegel { get; set; }
        [Column("c_cont_dachsiegel")]
        [StringLength(25)]
        public string CContDachsiegel { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
        [Column("d_geloescht", TypeName = "datetime")]
        public DateTime? DGeloescht { get; set; }
        [Column("d_ladedatum")]
        public DateOnly? DLadedatum { get; set; }
        [Column("c_kunde_refnr")]
        [StringLength(45)]
        public string CKundeRefnr { get; set; }
    }
}
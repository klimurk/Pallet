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
    /// Noch in Planung. MA die mit dem Worker arbeiten sollen Bestellung von Artikeln dem Büro mitteilen können. Hier werden die einzelnen Position der Bestellung gespeichert.
    /// </summary>
    [Table("t_worker_bestellung_pos")]
    public partial class TWorkerBestellungPo
    {
        [Key]
        [Column("n_bestellung_id")]
        public int NBestellungId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
        [Column("c_artikel")]
        [StringLength(300)]
        public string CArtikel { get; set; }
        [Column("n_anzahl")]
        [Precision(10, 2)]
        public decimal NAnzahl { get; set; }
        [Column("n_einheit_id")]
        public int? NEinheitId { get; set; }
        [Column("n_status_id")]
        public int NStatusId { get; set; }
    }
}
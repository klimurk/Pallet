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
    /// Baufeld zum Kunden oder Projekt
    /// </summary>
    [Table("t_baufeld")]
    public partial class TBaufeld
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kunde_id")]
        public int? NKundeId { get; set; }
        [Column("n_projekt_id")]
        public int? NProjektId { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
        [Column("c_kurz_bem")]
        [StringLength(255)]
        public string CKurzBem { get; set; }
        [Column("c_name")]
        [StringLength(100)]
        public string CName { get; set; }
        [Column("c_ort")]
        [StringLength(100)]
        public string COrt { get; set; }
        [Column("c_str")]
        [StringLength(50)]
        public string CStr { get; set; }
        [Column("c_strnr")]
        [StringLength(10)]
        public string CStrnr { get; set; }
        [Column("c_plz")]
        [StringLength(15)]
        public string CPlz { get; set; }
        [Column("n_staat_id")]
        public int? NStaatId { get; set; }
        [Column("c_firma")]
        [StringLength(50)]
        public string CFirma { get; set; }
        [Column("c_avbem", TypeName = "mediumtext")]
        public string CAvbem { get; set; }
        [Column("c_telefon")]
        [StringLength(50)]
        public string CTelefon { get; set; }
        /// <summary>
        /// Baufeld innerhalb der Firma und steht somit bei allen Kunden zur Auswahl
        /// </summary>
        [Column("n_intern")]
        public bool? NIntern { get; set; }
        /// <summary>
        /// Dient für Filter 1= Baufeld für Verpackung
        /// </summary>
        [Column("n_verpackung")]
        public bool? NVerpackung { get; set; }
        /// <summary>
        /// Dient für Filter 1= Baufeld für Fertigung
        /// </summary>
        [Column("n_fertigung")]
        public bool? NFertigung { get; set; }
    }
}
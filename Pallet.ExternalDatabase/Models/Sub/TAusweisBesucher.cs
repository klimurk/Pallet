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
    /// Ausweis für Fremde die nichtt zu einer Firma als Ansprechpartner erfasst wurden.
    /// </summary>
    [Table("t_ausweis_besucher")]
    public partial class TAusweisBesucher
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
        [Column("d_ausgestellt")]
        public DateOnly? DAusgestellt { get; set; }
        [Column("c_klasse")]
        [StringLength(5)]
        public string CKlasse { get; set; }
        [Column("c_name")]
        [StringLength(45)]
        public string CName { get; set; }
        [Column("c_vorname")]
        [StringLength(45)]
        public string CVorname { get; set; }
        [Column("n_ausgestellt_von")]
        public int NAusgestelltVon { get; set; }
        [Column("c_firma")]
        [StringLength(50)]
        public string CFirma { get; set; }
        [Column("m_grund", TypeName = "text")]
        public string MGrund { get; set; }
        [Column("m_bem", TypeName = "text")]
        public string MBem { get; set; }
    }
}
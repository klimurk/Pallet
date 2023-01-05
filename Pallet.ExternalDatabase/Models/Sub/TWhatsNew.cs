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
    /// Speichert alle Änderungen die in einem Update von Cratemaker erwähneswert sind.
    /// </summary>
    [Table("t_whats_new")]
    public partial class TWhatsNew
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_version")]
        [StringLength(25)]
        public string CVersion { get; set; }
        /// <summary>
        /// 2=new feature, 1=error solved
        /// </summary>
        [Column("n_type")]
        public short? NType { get; set; }
        /// <summary>
        /// 0=Allg, 1=Basis, 2=Lager, 3=Versand, 4=Auftrag, 5=3D, 6=Bestell, 7=combit, 8=Kalk, 9=Termin, 10= Container, 11=Design, 12=Stat
        /// </summary>
        [Column("n_module")]
        public int? NModule { get; set; }
        [Column("c_subject")]
        [StringLength(150)]
        public string CSubject { get; set; }
        [Column("m_text", TypeName = "mediumtext")]
        public string MText { get; set; }
    }
}
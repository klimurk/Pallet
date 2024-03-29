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
    /// Speichert die Uebersetzungen
    /// </summary>
    [Table("t_translation")]
    public partial class TTranslation
    {
        [Key]
        [Column("c_lang")]
        [StringLength(3)]
        public string CLang { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_text")]
        [StringLength(255)]
        public string CText { get; set; }
    }
}
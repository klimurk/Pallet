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
    /// Optionale Bemerkungen zum Kunden die auf die Rechnungen des Kunden gedrcukt werden.
    /// </summary>
    [Table("t_assistent_rechnung")]
    public partial class TAssistentRechnung
    {
        [Key]
        [Column("n_kunde_id")]
        public int NKundeId { get; set; }
        [Column("m_bem", TypeName = "text")]
        public string MBem { get; set; }
    }
}
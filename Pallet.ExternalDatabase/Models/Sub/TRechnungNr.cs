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
    /// Wird für die Generieung der Rechnungsnr. benutzt
    /// </summary>
    [Table("t_rechnung_nr")]
    public partial class TRechnungNr
    {
        [Key]
        [Column("n_year")]
        public int NYear { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_rechnung_id")]
        public int NRechnungId { get; set; }
    }
}
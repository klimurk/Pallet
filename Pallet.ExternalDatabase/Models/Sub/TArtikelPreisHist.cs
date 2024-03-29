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
    /// Preishistorie der Lieferantenartikel
    /// </summary>
    [Table("t_artikel_preis_hist")]
    public partial class TArtikelPreisHist
    {
        [Key]
        [Column("n_artikel_id")]
        public int NArtikelId { get; set; }
        [Key]
        [Column("n_firma_id")]
        public int NFirmaId { get; set; }
        [Key]
        [Column("d_datum")]
        public DateOnly DDatum { get; set; }
        [Column("n_preis")]
        [Precision(13, 2)]
        public decimal NPreis { get; set; }
    }
}
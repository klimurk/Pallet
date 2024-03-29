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
    /// Für die Generierung von Barcodes zur Verpackung und Holzprodukte.
    /// </summary>
    [Table("t_barcode_verpackung")]
    [Index("NBarcodeId", Name = "IDX_BARCODE_ID")]
    public partial class TBarcodeVerpackung
    {
        [Key]
        [Column("n_verp_id")]
        public int NVerpId { get; set; }
        [Key]
        [Column("n_verp_typ_id")]
        public int NVerpTypId { get; set; }
        [Key]
        [Column("n_verp_sub_id")]
        public int NVerpSubId { get; set; }
        [Key]
        [Column("c_nr")]
        [StringLength(50)]
        public string CNr { get; set; }
        /// <summary>
        /// Diese Id faengt bei 0 an und wird nie groesser als 99999999
        /// </summary>
        [Column("n_barcode_id")]
        public int NBarcodeId { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime DCreated { get; set; }
    }
}
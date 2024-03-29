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
    /// Initiale Werte für bestimmte Dimension. Aktuel wird es nur für den Glasverschlag bei den seitlichen Distanzhölzer genutzt.
    /// </summary>
    [Table("t_initdimvalue")]
    public partial class TInitdimvalue
    {
        [Key]
        [Column("val_verp_typ")]
        public int ValVerpTyp { get; set; }
        [Key]
        [Column("val_mat_typ")]
        public int ValMatTyp { get; set; }
        [Key]
        [Column("val_val_typ")]
        public int ValValTyp { get; set; }
        [Column("val_dim")]
        [StringLength(10)]
        public string ValDim { get; set; }
        [Column("val_anzahl")]
        public int? ValAnzahl { get; set; }
        [Column("val_hoch")]
        public bool? ValHoch { get; set; }
        [Column("val_flach")]
        public bool? ValFlach { get; set; }
    }
}
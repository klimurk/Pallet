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
    /// Anordnung der Querkufen bei der Bodekonstruktion. Wird benötigt wenn die Querkufendimensionen unterschiedlich sind.
    /// </summary>
    [Table("t_bodenkonstranordnung_qk")]
    public partial class TBodenkonstranordnungQk
    {
        [Key]
        [Column("bd_an_verp_id")]
        public int BdAnVerpId { get; set; }
        [Key]
        [Column("bd_an_verp_typ")]
        public int BdAnVerpTyp { get; set; }
        [Column("bd_an_qk1")]
        [StringLength(50)]
        public string BdAnQk1 { get; set; }
        [Column("bd_an_qk2")]
        [StringLength(50)]
        public string BdAnQk2 { get; set; }
    }
}
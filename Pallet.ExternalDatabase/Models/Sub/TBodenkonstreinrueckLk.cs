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
    /// Einrückmaße der Längskufen bei der Bodenkonstruktion
    /// </summary>
    [Table("t_bodenkonstreinrueck_lk")]
    public partial class TBodenkonstreinrueckLk
    {
        [Key]
        [Column("bd_ein_verp_id")]
        public int BdEinVerpId { get; set; }
        [Key]
        [Column("bd_ein_verp_typ")]
        public int BdEinVerpTyp { get; set; }
        [Key]
        [Column("bd_ein_pos")]
        public int BdEinPos { get; set; }
        [Column("bd_ein_links_mm")]
        public int? BdEinLinksMm { get; set; }
        [Column("bd_ein_rechts_mm")]
        public int? BdEinRechtsMm { get; set; }
    }
}
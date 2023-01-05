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
    /// Liste von Bezeichnungen die beim Beipackholz immer wieder vorkommen wie z.B. Lagerholz
    /// </summary>
    [Table("t_beipack_holz_bez")]
    public partial class TBeipackHolzBez
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(50)]
        public string CName { get; set; }
    }
}
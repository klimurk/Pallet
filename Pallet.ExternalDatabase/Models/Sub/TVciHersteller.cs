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
    /// VCI Hersteller
    /// </summary>
    [Table("t_vci_hersteller")]
    public partial class TVciHersteller
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(45)]
        public string CName { get; set; }
    }
}
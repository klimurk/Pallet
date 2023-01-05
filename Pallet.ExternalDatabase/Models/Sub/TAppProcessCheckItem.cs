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
    /// item list for each process
    /// </summary>
    [Table("t_app_process_check_item")]
    [Index("CProcessItemId", "CProcessId", Name = "IDX_PROCESS_ITEM")]
    public partial class TAppProcessCheckItem
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        [Required]
        [Column("c_process_id")]
        [StringLength(45)]
        public string CProcessId { get; set; }
        /// <summary>
        /// in case of t_app_process_check.n_process_type_id=1 
        /// c_process_item_id = t_wareneingangpackstueck.c_id
        /// </summary>
        [Required]
        [Column("c_process_item_id")]
        [StringLength(45)]
        public string CProcessItemId { get; set; }
        /// <summary>
        /// indicates when the item is processed
        /// </summary>
        [Column("n_done")]
        public bool NDone { get; set; }
        [Column("d_done", TypeName = "datetime")]
        public DateTime? DDone { get; set; }
    }
}
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
    /// Verknüpft ein Ereignis/Termin mit einer Resource
    /// </summary>
    [Table("t_event_calendar_resources")]
    public partial class TEventCalendarResource
    {
        [Key]
        [Column("c_id")]
        [StringLength(45)]
        public string CId { get; set; }
        /// <summary>
        /// t_resource.n_id
        /// </summary>
        [Key]
        [Column("n_resource_id")]
        public int NResourceId { get; set; }
        [Column("c_bem")]
        [StringLength(255)]
        public string CBem { get; set; }
    }
}
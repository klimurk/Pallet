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
    /// Protkolldatei über Ein-und Ausloggen über die Anwendung von Cratemaker Worker
    /// </summary>
    [Table("t_user_checkin")]
    public partial class TUserCheckin
    {
        [Key]
        [Column("n_firma_id")]
        public int NFirmaId { get; set; }
        [Key]
        [Column("n_user_id")]
        public int NUserId { get; set; }
        [Key]
        [Column("d_checkin_time", TypeName = "datetime")]
        public DateTime DCheckinTime { get; set; }
        [Column("d_checkout_time", TypeName = "datetime")]
        public DateTime? DCheckoutTime { get; set; }
        /// <summary>
        /// t_abteilung
        /// </summary>
        [Column("n_dept_id")]
        public int? NDeptId { get; set; }
    }
}
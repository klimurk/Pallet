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
    /// Protkoll über Pause Start und Ende des Benutzers bei Cratemaler Worker
    /// </summary>
    [Table("t_user_pause")]
    public partial class TUserPause
    {
        [Key]
        [Column("n_firma_id")]
        public int NFirmaId { get; set; }
        [Key]
        [Column("n_user_id")]
        public int NUserId { get; set; }
        [Key]
        [Column("d_pause_start", TypeName = "datetime")]
        public DateTime DPauseStart { get; set; }
        [Column("d_pause_end", TypeName = "datetime")]
        public DateTime? DPauseEnd { get; set; }
    }
}
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
    /// Mobilfunkgerät der Anspechpartner. Wird genutzt für die Crate Apps.
    /// </summary>
    [Table("t_ansprechpartner_handy")]
    public partial class TAnsprechpartnerHandy
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_firma_id")]
        public int NFirmaId { get; set; }
        [Column("n_anp_id")]
        public int NAnpId { get; set; }
        [Column("c_id")]
        [StringLength(30)]
        public string CId { get; set; }
        [Column("c_nr")]
        [StringLength(20)]
        public string CNr { get; set; }
        [Column("c_bez")]
        [StringLength(25)]
        public string CBez { get; set; }
        [Column("d_uebergeben", TypeName = "datetime")]
        public DateTime? DUebergeben { get; set; }
        [Column("d_ausgeschieden", TypeName = "datetime")]
        public DateTime? DAusgeschieden { get; set; }
        [Column("m_ausgeschieden", TypeName = "text")]
        public string MAusgeschieden { get; set; }
        [Column("d_created", TypeName = "datetime")]
        public DateTime? DCreated { get; set; }
    }
}
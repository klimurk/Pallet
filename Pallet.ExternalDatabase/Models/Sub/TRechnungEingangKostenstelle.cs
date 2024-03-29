﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pallet.ExternalDatabase.Models
{
    [Table("t_rechnung_eingang_kostenstelle")]
    [Index("NKostenstelleId", Name = "IDX_KOSTENSTELLE_ID")]
    [Index("NRechnungEingangId", Name = "IDX_RECHNUNG_EING_ID")]
    public partial class TRechnungEingangKostenstelle
    {
        [Key]
        [Column("n_rechnung_eingang_id")]
        public int NRechnungEingangId { get; set; }
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("n_kostenstelle_id")]
        public int NKostenstelleId { get; set; }
        [Column("n_betrag")]
        [Precision(19, 2)]
        public decimal NBetrag { get; set; }
    }
}
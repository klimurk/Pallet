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
    /// Cratemaker Benutzer
    /// </summary>
    [Table("t_user")]
    public partial class TUser
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(25)]
        public string CName { get; set; }
        [Column("c_vorname")]
        [StringLength(25)]
        public string CVorname { get; set; }
        [Column("c_passwort", TypeName = "mediumtext")]
        public string CPasswort { get; set; }
        [Column("d_geloescht", TypeName = "datetime")]
        public DateTime? DGeloescht { get; set; }
        [Column("n_laenge")]
        public int? NLaenge { get; set; }
        [Column("d_last_check", TypeName = "datetime")]
        public DateTime? DLastCheck { get; set; }
        [Column("n_logged_in")]
        public bool? NLoggedIn { get; set; }
        [Column("c_ip_address")]
        [StringLength(50)]
        public string CIpAddress { get; set; }
        [Column("c_license")]
        [StringLength(255)]
        public string CLicense { get; set; }
    }
}
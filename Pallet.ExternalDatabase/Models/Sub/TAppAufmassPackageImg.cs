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
    /// Aufmas App Bilder zum Objekt Packstück
    /// </summary>
    [Table("t_app_aufmass_package_img")]
    public partial class TAppAufmassPackageImg
    {
        [Key]
        [Column("n_main_id")]
        public long NMainId { get; set; }
        [Key]
        [Column("n_package_id")]
        public long NPackageId { get; set; }
        [Key]
        [Column("n_id")]
        public long NId { get; set; }
        [Column("c_filename")]
        [StringLength(45)]
        public string CFilename { get; set; }
        [Column("b_data")]
        public byte[] BData { get; set; }
    }
}
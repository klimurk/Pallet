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
    /// Enthaelt alle in der Anwendung verfuegbaren Rollen
    /// </summary>
    [Table("t_user_roles")]
    public partial class TUserRole
    {
        [Key]
        [Column("n_id")]
        public int NId { get; set; }
        [Column("c_name")]
        [StringLength(50)]
        public string CName { get; set; }
        [Column("m_bem", TypeName = "mediumtext")]
        public string MBem { get; set; }
    }
}
﻿using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Profiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Products;

[Table("WPROD_DEF")]
public class Product : NamedEntity
{
    //[ConcurrencyCheck]
    [Column("WPROD_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(200)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(200)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(200)")]
    public string Description3 { get; set; }

    [Column("STEP1")]
    public bool Step1 { get; set; }

    [Column("STEP2")]
    public bool Step2 { get; set; }

    [Column("SIZE1_X", TypeName = "int")]
    public double Size1X { get; set; }

    [Column("SIZE1_Y", TypeName = "int")]
    public double Size1Y { get; set; }

    [Column("SIZE1_Z", TypeName = "int")]
    public double Size1Z { get; set; }

    [Column("SIZE2_X", TypeName = "int")]
    public double Size2X { get; set; }

    [Column("SIZE2_Y", TypeName = "int")]
    public double Size2Y { get; set; }

    [Column("SIZE2_Z", TypeName = "int")]
    public double Size2Z { get; set; }

    [Column("PRESET")]
    public bool Preset { get; set; }

    [Column("TYPE")]
    public int Type { get; set; }

    [Column("PROD")]
    public int Prod { get; set; }

    [InverseProperty("Product")]
    public ICollection<Nail> Nails { get; set; } = new HashSet<Nail>();

    [InverseProperty("Product")]
    public ICollection<ElementPosition> Elements { get; set; } = new HashSet<ElementPosition>();

    //[ForeignKey("PROFILES")]
    [InverseProperty("Product")]
    public ICollection<ProfileProducts> ProfileProducts { get; set; }

    //[InverseProperty("Product")]
    //public virtual ICollection<Nail> Nails { get; set; } = new HashSet<Nail>();

    //[InverseProperty("Product")]
    //public virtual ICollection<ElementPosition> Elements { get; set; } = new HashSet<ElementPosition>();
}
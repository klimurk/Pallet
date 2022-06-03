using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Tables;
using Pallet.Database.Entities.Change.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Profiles;

[Table("PROFILE_DEF")]
public class Profile : NamedEntity
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    //[ConcurrencyCheck]
    [Column("PROFILE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(200)")]
    public string Description1 { get; set; }

    [Column("DESC2", TypeName = "nvarchar(200)")]
    public string Description2 { get; set; }

    [Column("DESC3", TypeName = "nvarchar(200)")]
    public string Description3 { get; set; }

    [Column("DT_CREA")]
    public DateTime DateCreate { get; set; } = DateTime.Now;

    [Column("DT_CHNG")]
    public DateTime DateLastModified { get; set; }

    [Column("DT_OPEN")]
    public DateTime? DateLastUse { get; set; }

    [Column("CREA_BY", TypeName = "nvarchar(50)")]
    public string Author { get; set; }

    [ForeignKey("WTABLE_ID")]
    [InverseProperty("Profiles")]
    public Table Table { get; set; }

    //[ForeignKey("WPROD_ID1")]
    //public Product Prod1 { get; set; }

    //[ForeignKey("WPROD_ID2")]
    //public Product Prod2 { get; set; }

    //[ForeignKey("WPROD_ID3")]
    //public Product Prod3 { get; set; }

    //[ForeignKey("WPROD_ID4")]
    //public Product Prod4 { get; set; }

    //[ForeignKey("WPRODS")]
    [InverseProperty("Profile")]
    public ICollection<ProfileProducts> ProfileProducts { get; set; }

    //[ForeignKey("TOOL")]
    [InverseProperty("Profile")]
    public ICollection<ProfileTools> ProfileTools { get; set; }

    //public ProfileTools ProfileToolsN { get; set; }

    //[ForeignKey("TOOL_GID")]
    //public ProfileTools ProfileToolsG { get; set; }

    //[ForeignKey("TOOL_NID1")]
    //public Tool Tool_N1 { get; set; }

    //[ForeignKey("TOOL_NID2")]
    //public Tool Tool_N2 { get; set; }

    //[ForeignKey("TOOL_NID3")]
    //public Tool Tool_N3 { get; set; }

    //[ForeignKey("TOOL_NID4")]
    //public Tool Tool_N4 { get; set; }

    //[ForeignKey("TOOL_GID1")]
    //public Tool Tool_G1 { get; set; }

    //[ForeignKey("TOOL_GID2")]
    //public Tool Tool_G2 { get; set; }

    //[ForeignKey("TOOL_GID3")]
    //public Tool Tool_G3 { get; set; }

    //[ForeignKey("TOOL_GID4")]
    //public Tool Tool_G4 { get; set; }
}
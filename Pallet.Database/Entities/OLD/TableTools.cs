using Pallet.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("WTABLE_TOOLS")]
public class TableTools : Entity
{
    [Column("WTABLE_ID")]
    public int TableID { get; set; }

    [Column("TOOL_NID1")]
    public int Tool_N1 { get; set; }

    [Column("TOOL_NID2")]
    public int Tool_N2 { get; set; }

    [Column("TOOL_NID3")]
    public int Tool_N3 { get; set; }

    [Column("TOOL_NID4")]
    public int Tool_N4 { get; set; }

    [Column("TOOL_GID1")]
    public int Tool_G1 { get; set; }

    [Column("TOOL_GID2")]
    public int Tool_G2 { get; set; }

    [Column("TOOL_GID3")]
    public int Tool_G3 { get; set; }

    [Column("TOOL_GID4")]
    public int Tool_G4 { get; set; }
}
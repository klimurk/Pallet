using Pallet.Database.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.OLD;

[Table("PROFILE_DEF")]
public class Profile : NamedEntity
{
    [Column("PROFILE_NAME", TypeName = "varchar(20)")]
    public new string Name { get; set; }

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

    [Column("WTABLE_ID")]
    public TableTemplate Table { get; set; }
}

//public virtual ICollection<ProfileNails> ProfileNails { get; set; } = new HashSet<ProfileNails>();
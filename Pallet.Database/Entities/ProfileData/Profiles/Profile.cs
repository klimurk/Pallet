using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Base.Interfaces;
using Pallet.Database.Entities.ProfileData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.ProfileData.Profiles;

[Table("PROFILE_DEF")]
public class Profile : NamedEntity, IDBTranslateble
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    //[ConcurrencyCheck]
    [Column("PROFILE_NAME", TypeName = "varchar(20)")]
    public override string Name { get; set; }

    [Column("DESC1", TypeName = "nvarchar(200)")]
    public string DescriptionEn { get; set; }

    [Column("DESC2", TypeName = "nvarchar(200)")]
    public string DescriptionDe { get; set; }

    [Column("DESC3", TypeName = "nvarchar(200)")]
    public string DescriptionLocal { get; set; }

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

    [InverseProperty("Profile")]
    public ICollection<ProfileProducts> ProfileProducts { get; set; }

    [InverseProperty("Profile")]
    public ICollection<ProfileTools> ProfileTools { get; set; }

    public struct OPCData
    {
        public struct Nails
        {
            public static string DBName { get; } = "Empfang";
            public static string DBVar { get; } = "Data";
            public static int DBNamespace { get; } = 3;

            public struct Fields
            {
                public static string CoorX { get; } = "coor X";
                public static string CoorY { get; } = "coor Y";
                public static string CoorZ { get; } = "coor Z";
                public static string Checksum { get; } = "checksum";
                public static string Active { get; } = "active";
                public static string Done { get; } = "done";
                public static string Angle1 { get; } = "angle1";
                public static string Angle2 { get; } = "angle2";
                public static string NailType { get; } = "NailType";
                public static string NailID { get; } = "NailID";
                public static string NailGRP { get; } = "NailGRP";
            }
        }
    }
}
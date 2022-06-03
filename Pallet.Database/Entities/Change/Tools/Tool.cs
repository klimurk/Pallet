using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Profiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Change.Tools;

[Table("WTABLE_TOOLS")]
public class Tool : Entity
{
    //[ForeignKey("PROFILES")]
    [InverseProperty("Tool")]
    public ICollection<ProfileTools> ProfileTools { get; set; }
}
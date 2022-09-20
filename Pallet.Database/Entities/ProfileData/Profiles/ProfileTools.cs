using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.ProfileData.Tools;

namespace Pallet.Database.Entities.ProfileData.Profiles;

public class ProfileTools : Entity
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
    public int ToolId { get; set; }
    public Tool Tool { get; set; }
}
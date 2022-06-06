using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Tools;

namespace Pallet.Database.Entities.Change.Profiles;

public class ProfileTools : Entity
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
    public int ToolId { get; set; }
    public Tool Tool { get; set; }
}
using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.Change.Products;

namespace Pallet.Database.Entities.Change.Profiles;

public class ProfileProducts : Entity
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
using Pallet.Database.Entities.Base;
using Pallet.Database.Entities.ProfileData.Products;
using System.ComponentModel.DataAnnotations;

namespace Pallet.Database.Entities.ProfileData.Profiles;

public class ProfileProducts : Entity
{
    public int Position { get; set; }
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
    public int ProductId { get; set; }

    [Key]
    public Product Product { get; set; }
}
using Pallet.Database.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Base;

public abstract class Entity : IEntity
{
    [Column(Order = 0)]
    public int ID { get; set; }
}
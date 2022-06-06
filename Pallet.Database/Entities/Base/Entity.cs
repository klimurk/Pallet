using Pallet.Database.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.Database.Entities.Base;

/// <summary>
/// Base entity for database which include ID column.
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// Id of row in db.
    /// </summary>
    [Column(Order = 0)]
    public int ID { get; set; }
}
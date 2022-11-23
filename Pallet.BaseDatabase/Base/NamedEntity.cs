using Pallet.InternalDatabase.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.BaseDatabase.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Column(Order = 1)]
        [Required]
        public virtual string Name { get; set; }
    }

}

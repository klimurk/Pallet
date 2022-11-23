using Pallet.InternalDatabase.Entities.Base.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.BaseDatabase.Base
{

    /// <summary>
    /// Base entity for database which include ID column.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Id of row in db.
        /// </summary>
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public object ID { get; set; }
    }
}

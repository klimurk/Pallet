﻿namespace Pallet.InternalDatabase.Entities.Base.Interfaces
{
    /// <summary>
    /// Base entity with id and name for database.
    /// </summary>
    public interface INamedEntity : IEntity
    {
        /// <summary>
        /// Name of row in database.
        /// </summary>
        public string Name { get; set; }
    }

}

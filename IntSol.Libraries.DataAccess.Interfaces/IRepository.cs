using System;
using System.Collections.Generic;

namespace IntSol.Libraries.DataAccess.Interfaces
{
    public interface IRepository<Entity, EntityKey> : IDisposable
    {
        IEnumerable<Entity> GetEntities();
        Entity GetEntityByKey(EntityKey entityKey);
        bool AddEntity(Entity entity);
    }
}

using System.Collections.Generic;

namespace Core.Intefaces
{
    public interface IUpdateRepository<T> where T : class //BaseEntity
    {
        #region Update
         void Update(T entity);
        #endregion
    }
}

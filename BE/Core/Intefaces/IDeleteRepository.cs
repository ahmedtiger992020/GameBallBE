using System;
using System.Linq.Expressions;

namespace Core.Intefaces
{
    public interface IDeleteRepository<T> where T : class //BaseEntity
    {
        #region Delete
        void BulkHardDelete(Expression<Func<T, bool>> filter = null);
        #endregion
    }
}

using System.Collections.Generic;
using Core.Entities;

namespace Core.Intefaces
{
    public interface IRepository<T> : IInsertRepository<T>, IUpdateRepository<T>, IDeleteRepository<T>, IRetreiveRepository<T> where T : class //BaseEntity
    {

    }
}

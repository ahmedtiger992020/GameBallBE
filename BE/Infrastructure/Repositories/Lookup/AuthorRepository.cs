
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Intefaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        DbSet<Author> entity;
        #region CTRS
        public AuthorRepository(GBSampleContext context) : base(context)
        {
            entity = context.Set<Author>();
        }
        #endregion

    }
}

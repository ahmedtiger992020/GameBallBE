
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Intefaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        DbSet<Book> entity;
        #region CTRS
        public BookRepository(GBSampleContext context) : base(context)
        {
            entity = context.Set<Book>();
        }
        #endregion

    }
}


using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Intefaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        DbSet<Review> entity;
        #region CTRS
        public ReviewRepository(GBSampleContext context) : base(context)
        {
            entity = context.Set<Review>();
        }
        #endregion

    }
}

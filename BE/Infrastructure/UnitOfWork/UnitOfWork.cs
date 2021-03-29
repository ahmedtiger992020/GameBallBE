
using System;
using System.Threading.Tasks;
using Core.Intefaces;
using Infrastructure.Context;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Defines the Context
        /// </summary>
        public GBSampleContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="GBSampleContext"/></param>
        public UnitOfWork(GBSampleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        public async Task<int> Commit()
        {
            try
            {
                int status = await _context.SaveChangesAsync();
                #region comment code give errror "Database operation expected to affect 1 row(s) but actually affected 0 row(s). Data may have been modified or deleted since entities were loaded"
                //var changedEntriesCopy = Context.ChangeTracker.Entries()
                //                              .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted || e.State == EntityState.Unchanged)
                //                              .ToList();
                //foreach (var entry in changedEntriesCopy)
                //{
                //    entry.State = EntityState.Detached;
                //}
                #endregion
                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

     
    }
}

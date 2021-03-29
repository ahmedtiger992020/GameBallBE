using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IUnitOfWork 
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        Task<int> Commit();


    }
}

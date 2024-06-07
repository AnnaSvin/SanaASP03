using SanaToDoLIST.Models;
using System.Threading.Tasks;

namespace SanaToDoLIST.Repository
{
    public interface ITasksRepository
    {
        Task Create(Tasks task);
        Task<Tasks> GetById(int id);
        Task Update(Tasks task);
        Task DeleteById(int id);
        Task<IEnumerable<Tasks>> GetAll();
    }
}

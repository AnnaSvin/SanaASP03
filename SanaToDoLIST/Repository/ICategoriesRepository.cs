using SanaToDoLIST.Models;

namespace SanaToDoLIST.Repository
{
    public interface ICategoriesRepository
    {
        Task Create(Categories category);

        Task<Categories> GetById(int id);

        Task<IEnumerable<Categories>> GetAll();

        Task Update(Categories category);

        Task DeleteById(int id);
    }
}

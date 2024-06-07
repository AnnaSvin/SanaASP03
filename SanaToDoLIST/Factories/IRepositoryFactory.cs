using SanaToDoLIST.Repository;

namespace SanaToDoLIST.Factories
{
    public interface IRepositoryFactory
    {
        ICategoriesRepository GetCategoriesRepository();

        ITasksRepository GetTasksRepository();
    }
}

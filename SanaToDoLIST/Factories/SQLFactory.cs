using SanaToDoLIST.Factories;
using SanaToDoLIST.Repository.SQLTaskRepository;
using SanaToDoLIST.Repository;

namespace SanaToDoLIST.Factories
{
    public class SQLFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        public ICategoriesRepository GetCategoriesRepository()
        {
            return serviceProvider.GetRequiredService<SQLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return serviceProvider.GetRequiredService<SQLTasksRepository>();
        }
    }
}

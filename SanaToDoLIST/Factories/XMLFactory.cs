using SanaToDoLIST.Repository;
using SanaToDoLIST.Repository.XMLTaskRepository;

namespace SanaToDoLIST.Factories
{
    public class XMLFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        public ICategoriesRepository GetCategoriesRepository()
        {
            return serviceProvider.GetRequiredService<XMLCategoriesRepository>();
        }

        public ITasksRepository GetTasksRepository()
        {
            return serviceProvider.GetRequiredService<XMLTasksRepository>();
        }
    }
}

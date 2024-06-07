using SanaToDoLIST.Factories;

namespace SanaToDoLIST.Models
{
    public class TaskViewModel
    {
        public Tasks NewTask { get; set; }
        public Categories NewCategory { get; set; }
        public IEnumerable<Tasks> TaskList { get; set; }
        public IEnumerable<Categories> CategoriesList { get; set; }
        public StorageType StorageTypeSelected { get; set; }

    }
}

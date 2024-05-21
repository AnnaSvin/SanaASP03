using System.Collections.Generic;

namespace SanaSDB3.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<Tasks> TaskList { get; set; } = new List<Tasks>();
        public Tasks NewTask { get; set; }
    }
}

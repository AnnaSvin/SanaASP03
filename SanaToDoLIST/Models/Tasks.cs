﻿namespace SanaToDoLIST.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate{ get; set; }
        public int? CategoryId { get; set; }

        public Categories? Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Person> AssignedTo { get; set; }
        public List<TaskPart> Parts { get; set; }
        public List<Comment> Comments { get; set; }

        public TaskItem()
        {
            AssignedTo = new List<Person>();
            Parts = new List<TaskPart>();
            Comments = new List<Comment>();
        }
    }

}

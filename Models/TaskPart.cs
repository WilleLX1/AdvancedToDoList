using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class TaskPart
    {
        public Guid Id { get; set; }
        public string Title { get; set; }      // Titel/namn på del
        public bool IsCompleted { get; set; }  // Om denna del är avklarad
        public string Description { get; set; } = "";
    }
}

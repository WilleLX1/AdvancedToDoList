using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Row { get; set; } = 0;       // Ny egenskap
        public List<string> AttachedFilePaths { get; set; } = new List<string>();
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }

}

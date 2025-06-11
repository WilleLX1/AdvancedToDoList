using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class ProjectDataStore
    {
        public List<Person> AllPersons { get; set; } = new List<Person>();
        public List<Project> AllProjects { get; set; } = new List<Project>();
    }
}

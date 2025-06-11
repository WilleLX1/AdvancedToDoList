using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class Person
    {
        public Guid Id { get; set; }           // Unikt ID
        public string Name { get; set; }       // “Namn”
        public string Email { get; set; }      // E-post
    }
}

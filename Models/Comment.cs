using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoList.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }     // referens till Person.Id (eller null om anonym)
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
    }
}

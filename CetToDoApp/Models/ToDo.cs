using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetToDoApp.Models
{
    public class ToDo
    {

        public int Id { get; set; }

        public string Title { get; set; }


        public bool IsCompleted { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set;  }


        public DateTime CompletedDate { get; set; }
        
            
        
    }
}

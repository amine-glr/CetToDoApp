using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetToDoApp.Models
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }

        public bool ShowAll { get; set; }

        public bool SearchWithDesc { get; set; }


        public List<Category> Result2 { get; set; }

        public List<ToDoItem> Result { get; set; }

    }
}

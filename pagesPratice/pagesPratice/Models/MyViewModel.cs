using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pagesPratice.Models
{

    public class MyViewModel
    {
        public List<book> Books { get; set; }
        public List<book_category> Categories { get; set; }
    }
}

namespace pagesPratice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class book
    {
        public int id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_summary { get; set; }
    }
}

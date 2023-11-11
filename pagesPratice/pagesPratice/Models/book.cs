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
        public Nullable<int> book_category { get; set; }
        public Nullable<int> book_page { get; set; }
        public Nullable<bool> release { get; set; }
        public Nullable<System.DateTime> release_date { get; set; }
    
        public virtual book_category book_category1 { get; set; }
    }
}

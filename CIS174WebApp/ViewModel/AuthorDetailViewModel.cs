using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CIS174WebApp.Entity;

namespace CIS174WebApp.ViewModel
{
    public class AuthorDetailViewModel
    {
        public int AuthorId { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Display(Name="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public string City { get; set; }

        public string State { get; set; }
      
        public bool CanManageAuthor { get; set; }
        public IEnumerable<Book> Record { get; set; }

        public class Book
        {
            public string BookTitle { get; set; }
            public DateTime PublishDate { get; set; }
            public DateTime NumberOfBooks { get; set; }
        }

    }
}

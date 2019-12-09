using System;
using System.ComponentModel.DataAnnotations;
using CIS174WebApp.Entity;

namespace CIS174WebApp.ViewModel
{
    public class CreateBookCommand
    {
        [Required, StringLength(100)]
        public string BookTitle { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public Books ToBooks()
        {
            return new Books

            {
                BookTitle = BookTitle,
                PublishDate = PublishDate
            };
        }
    }
}

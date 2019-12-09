using System;
using System.ComponentModel.DataAnnotations;

namespace CIS174WebApp.Entity
{
    public class Books
    {
        //create accomplishment entity
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(70)]
        public string BookTitle { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
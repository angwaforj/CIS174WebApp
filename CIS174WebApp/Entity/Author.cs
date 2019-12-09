using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174WebApp.Entity
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        [StringLength(70)]
        public string City { get; set; }
        [StringLength(70)]
        public string State { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedById { get; set; }

        public ICollection<Books> NumberOfBooks { get; set; }
    }
}

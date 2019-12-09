using System.Collections.Generic;
using System.Linq;
using CIS174WebApp.Entity;

namespace CIS174WebApp.ViewModel
{
    public class CreateAuthorCommand : EditAuthor
    {
        public IList<CreateBookCommand> BooksList { get; set; } = new List<CreateBookCommand>();

        public Author ToAuthor(AppUser createdBy)
        {
            return new Author
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthdate = Birthdate,
                City = City,
                State = State,
                NumberOfBooks = BooksList?.Select(x => x.ToBooks()).ToList(),
                CreatedById = createdBy.Id
            };
        }
    }
}

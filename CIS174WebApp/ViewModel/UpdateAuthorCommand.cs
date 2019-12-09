using CIS174WebApp.Entity;

namespace CIS174WebApp.ViewModel
{
    public class UpdateAuthorCommand : EditAuthor
    {
        public int AuthorId { get; set; }

        public void UpdateAuthor(Author author)
        {
            author.FirstName = FirstName;
            author.LastName = LastName;
            author.Birthdate = Birthdate;
            author.City = City;
            author.State = State;

        }
    }
}

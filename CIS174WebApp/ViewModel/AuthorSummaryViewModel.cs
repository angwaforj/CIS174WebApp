using CIS174WebApp.Entity;

namespace CIS174WebApp.ViewModel
{
    public class AuthorSummaryViewModel
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string NumberOfBooks { get; set; }


        public static AuthorSummaryViewModel FromAuthor(Author author, Books book)
        {
            return new AuthorSummaryViewModel
            {
                AuthorId = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                NumberOfBooks = book.BookTitle
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174WebApp.Entity;
using CIS174WebApp.ViewModel;

namespace CIS174WebApp
{
    public class AuthorServices
    {
         private readonly ApplicationDbContext _dbContext;
        

        public AuthorServices(ApplicationDbContext context)
        {
            _dbContext = context;
           
        }

        //Create a summary of all person
        public ICollection<AuthorSummaryViewModel> GetAuthor()
        {
            return _dbContext.Author
                .Where(r => !r.IsDeleted)
                .Select(x => new AuthorSummaryViewModel()
                {
                    AuthorId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NumberOfBooks = x.NumberOfBooks.Count().ToString()
                })
                .ToList();
        }
        //Boolean to check if person for update exist
        public bool DoesPersonExist(int id)
        {
            return _dbContext.Author
                .Where(r => !r.IsDeleted)
                .Any(r => r.Id == id);
        }

        public bool DoesNameMatch(string name)
        {
            return _dbContext.Author
                .Where(x => !x.IsDeleted)
                .Any(x => x.FirstName == name && x.LastName == name);
        }

        //Show detail of one Author
        public AuthorDetailViewModel GetAuthorDetails(int id)
        {
            return _dbContext.Author
                .Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .Select(x => new AuthorDetailViewModel()
                {
                    AuthorId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthdate = x.Birthdate,
                    City = x.City,
                    State = x.State,
                    Record = x.NumberOfBooks
                        .Select(item => new AuthorDetailViewModel.Book
                        {
                            BookTitle = item.BookTitle,
                            NumberOfBooks = item.PublishDate
                        })
                }).SingleOrDefault();
        }
        public Author GetAuthor(int authorId)
        {
            return _dbContext.Author
                .Where(x => x.Id == authorId)
                .SingleOrDefault();
        }

        //Find person to update by id
        public UpdateAuthorCommand GetAuthorForUpdate(int id)
        {
            return _dbContext.Author
                .Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .Select(x => new UpdateAuthorCommand
                {
                    AuthorId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthdate = x.Birthdate,
                    City = x.City,
                    State = x.State,
                }).SingleOrDefault();
        }
        //Create Person 
        public int CreateAuthor(CreateAuthorCommand cmd, AppUser createdBy)
        {
            var author = cmd.ToAuthor(createdBy);
            _dbContext.Add(author);
            _dbContext.SaveChanges();
            return author.Id;
        }


        /// <summary>
        /// Update person using id
        /// </summary>
        /// <param name="cmd"></param>
        public void UpdateAuthor(UpdateAuthorCommand cmd)
        {
            var person = _dbContext.Author.Find(cmd.AuthorId);
            if (person == null) { throw new Exception("Unable to find the person"); }
            if (person.IsDeleted) { throw new Exception("Unable to update a deleted person"); }

            cmd.UpdateAuthor(person);
            _dbContext.SaveChanges();
        }

        public void DeleteAuthor(int authorId)
        {
            var person = _dbContext.Author.Find(authorId);
            if (person.IsDeleted) { throw new Exception("Unable to delete a deleted person"); }

            person.IsDeleted = true;
            _dbContext.SaveChanges();
        }
    }
}

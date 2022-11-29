using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPRNver2._0.EF;
using ProjectPRNver2._0.ViewModel;
using PagedList;

namespace Model.DAO
{
   public class ProductDao
    {
        ProjectPRNDbContext db = null;
        public ProductDao()
        {
            db = new ProjectPRNDbContext();
        }
        public List<BookViewModel> ListNewBook(int top)
        {
            var newBook = from a in db.Book_Authors
                          join b in db.Books on a.AuthorId equals b.AuthorId
                          into bok
                          from b in bok.Take(top)
                          select new BookViewModel
                          {
                              BookId = b.BookId,
                              BookName = b.BookName,
                              BookPrice = b.BookPrice,
                              AuthorName = a.AuthorName,
                              BookDescription = a.Description,
                              ReleaseDate = (DateTime)b.ReleaseDate,
                              ImgPath = b.imgPath
                          };

            return newBook.OrderByDescending(x => x.ReleaseDate).Take(top).ToList();
        }
        public List<BookViewModel> ListManga(int top)
        {
            var mangaBook = from a in db.Book_Authors
                            join b in db.Books on a.AuthorId equals b.AuthorId
                            into bok
                            from b in bok.Take(top)
                            select new BookViewModel
                            {
                                BookId = b.BookId,
                                BookName = b.BookName,
                                BookPrice = b.BookPrice,
                                AuthorName = a.AuthorName,
                                BookDescription = a.Description,
                                ReleaseDate = (DateTime)b.ReleaseDate,
                                ImgPath = b.imgPath,
                                CategoryId = b.CategoryId
                            };

            return mangaBook.OrderByDescending(x => x.CategoryId == 3).Take(top).ToList();
        }

        public List<Book_Authors> ListAuthor()
        {
            return db.Book_Authors.Take(1).ToList();
        }
        public Book ViewDetail(int id)
        {
            return db.Books.Find(id);
        }
        public IEnumerable<Book_Authors> ListAllAuthor(int page, int pageSize)
        {
            return db.Book_Authors.OrderByDescending(x => x.AuthorName).ToPagedList(page, pageSize);

        }

        public Book_Authors ViewAuthor(int id)
        {
            return db.Book_Authors.Find(id);
        }
        public List<BookViewModel> ListWishList(int id)
        {
            var wishBook = from a in db.Book_Authors
                           join b in db.Books on a.AuthorId equals b.AuthorId
                           join c in db.WishLists on b.BookId equals c.BookId
                           where c.UserId == id
                           select new BookViewModel()
                           {
                               BookId = b.BookId,
                               BookName = b.BookName,
                               AuthorName = a.AuthorName,
                               BookPrice = b.BookPrice,
                               ImgPath = b.imgPath
                           };

            return wishBook.OrderByDescending(x => x.BookId).ToList();
        }
    }
}

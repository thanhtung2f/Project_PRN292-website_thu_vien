using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;
using PagedList.Mvc;
using Model.Phap;
using Model;

namespace Model.Phap
{
    public class CategoryModel
    {
        ProjectPRNDbContext db = null;
        public CategoryModel()
        {
            db = new ProjectPRNDbContext();
        }
        public List<Book> ListAll()
        {
            var list = db.Database.SqlQuery<Book>("Book_Category_ListAll").ToList();
            return list;
        }   
        public IEnumerable<BookPhap> ListAllPaging(int id, int page, int pageSize)
        {
            return GetBookViewModels(id).OrderBy(x => x.BookId).ToPagedList(page, pageSize);
        }
        public List<BookPhap> GetBookViewModels(int id)
        {
            var model = from a in db.Books
                        where a.CategoryId.Equals(id)
                        join b in db.Book_Authors on a.AuthorId equals b.AuthorId
                        select new Phap.BookPhap()
                        {
                            BookId = a.BookId,
                            BookName = a.BookName,
                            BookPrice = a.BookPrice,
                            imgPath = a.imgPath,
                            AuthorName = b.AuthorName
                        };
            return model.ToList();
        }
    }
}

using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class WishListDao
    {
        ProjectPRNDbContext db = null;

        public WishListDao()
        {
            db = new ProjectPRNDbContext();
        }

        public int Insert(WishList entity)
        {
            db.WishLists.Add(entity);
            db.SaveChanges();
            return entity.BookId;
        }
    }
}

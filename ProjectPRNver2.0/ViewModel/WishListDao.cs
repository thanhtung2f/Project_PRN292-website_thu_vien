using ProjectPRNver2._0.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.ViewModel
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
using ProjectPRNver2._0.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserDao
    {
        ProjectPRNDbContext db = null;
        public UserDao()
        {
            db = new ProjectPRNDbContext();
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserId;
        }
        public User GetUserById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public bool Login(string userName, string password)
        {
            var result = db.Users.Count(x => x.UserName == userName && x.PassWord == password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.UserMail == email) > 0;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.UserId);
                user.FullName = entity.FullName;
                user.PassWord = entity.PassWord;
                user.Address = entity.Address;
                user.UserPhone = entity.UserPhone;
                user.UserMail = entity.UserMail;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public User ViewUserDetail(int id)
        {
            return db.Users.Find(id);
        }

    }
}

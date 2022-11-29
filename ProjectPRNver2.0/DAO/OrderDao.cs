using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPRNver2._0.EF;
using ProjectPRNver2._0.ViewModel;


namespace Model.DAO
{
    public class OrderDao
    {
        ProjectPRNDbContext db = null;

        public OrderDao()
        {
            db = new ProjectPRNDbContext();
        }

        public List<OrderViewModel> OrderList(int id)
        {
            var orderDetail = from a in db.OrderDetails
                              join b in db.Orders on a.OrderId equals b.OrderId
                              join c in db.Books on a.BookId equals c.BookId
                              where b.UserId == id
                              select new OrderViewModel()
                              {
                                  OrderDetailId = a.OrderDetailId,
                                  BookName = c.BookName,
                                  BookId = b.OrderId,
                                  OrderId = b.OrderId,
                                  Quantity = a.Quantity,
                                  price = c.BookPrice,
                                  image = c.imgPath
                              };

            return orderDetail.OrderByDescending(x => x.OrderDetailId).ToList();
        }
        public int InsertOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderId;
        }

        //public class Insert
        //{
        //    private Order order;

        //    public Insert(Order order)
        //    {
        //        this.order = order;
        //    }
        //}
    }
}

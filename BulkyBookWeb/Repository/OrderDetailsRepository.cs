using Bulky.DataAccess.Data;
using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyBookWeb.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetail>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}

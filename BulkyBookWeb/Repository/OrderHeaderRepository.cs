using Bulky.DataAccess.Data;
using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyBookWeb.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }
    }
}

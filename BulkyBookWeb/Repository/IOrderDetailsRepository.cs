using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
        //void Save();
    }
}

using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        //void Save();
    }
}

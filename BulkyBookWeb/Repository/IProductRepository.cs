using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}

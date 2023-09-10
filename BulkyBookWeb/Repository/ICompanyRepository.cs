using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}

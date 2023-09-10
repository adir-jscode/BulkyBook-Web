using Bulky.DataAccess.Data;
using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace BulkyBookWeb.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
        public void Update(Company obj)
        {
            _db.companies.Update(obj);
        }
    }
}

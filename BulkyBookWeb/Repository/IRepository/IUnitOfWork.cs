namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //all the repositoty
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        void Save();

    }
}

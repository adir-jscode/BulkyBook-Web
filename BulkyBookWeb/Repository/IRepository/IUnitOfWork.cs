namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //all the repositoty
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();

    }
}

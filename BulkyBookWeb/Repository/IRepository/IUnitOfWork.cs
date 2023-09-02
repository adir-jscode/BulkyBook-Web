namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //all the repositoty
        ICategoryRepository Category { get; }
        void Save();

    }
}

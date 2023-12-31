﻿namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //all the repositoty
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }

        IApplicationUserRepository ApplicationUserRepository { get; }

        IOrderHeaderRepository OrderHeader { get; }

        IOrderDetailsRepository OrderDetails { get; }
        void Save();

    }
}

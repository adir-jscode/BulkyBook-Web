using Bulky.Models;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }






        #region APICALLS

        public IActionResult GetAll()
        {
            List<OrderHeader> objList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objList });
        }

        //[HttpDelete]
        //public IActionResult Remove(int? id)
        //{
        //    Product ProductToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);
        //    if (ProductToBeDeleted == null)
        //    {
        //        return Json(new { success = false, message = "Error While Deleting" });
        //    }

        //    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, ProductToBeDeleted.ImageUrl.Trim('\\'));
        //    if (System.IO.File.Exists(oldImagePath))
        //    {
        //        System.IO.File.Delete(oldImagePath);
        //    }

        //    _unitOfWork.Product.Remove(ProductToBeDeleted);
        //    _unitOfWork.Save();

        //    return Json(new { success = true, message = "Delete Successfully" });
        //}

        #endregion


    }
}

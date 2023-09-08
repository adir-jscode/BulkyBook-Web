using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using BulkyBookWeb.Repository;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objList = _unitOfWork.Product.GetAll(includeProperties : "Category").ToList();

            return View(objList);
        }
        //public IActionResult Create()
        //{
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.CategoryId.ToString()
        //    });

        //    //ViewBag.CategoryList = CategoryList;
        //    ViewData["CategoryList"] = CategoryList;

        //    ProductVM productVM = new()
        //    {
        //        CategoryList = CategoryList,
        //        Product = new Product()
        //    };
        //    return View(productVM);
        //}

        //working with upsert

        public IActionResult Upsert(int?id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            if(id == null || id ==0)
            {
                //create
                return View(productVM);
            }

            else
            {
                //update

                productVM.Product = _unitOfWork.Product.Get(u=>u.ProductId == id);
                return View(productVM);
            }

           
        }





        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            //custom validation
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The display order cannot exactly the same");
            //}

            //if (obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is not valid");
            //}



            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old Image
                        var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImageUrl.Trim('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }


                    using (var FileSteam = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(FileSteam);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

               
                _unitOfWork.Save();
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });

                return RedirectToAction("Index");
            }
           

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? objFormDb = _unitOfWork.Product.Get(u => u.CategoryId == id);
            //Product? obj2 = _db.Categories.FirstOrDefault();
            //Product? obj3 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            if (objFormDb == null)
            {
                return NotFound();
            }


            return View(objFormDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }


            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ObjectFromDb = _unitOfWork.Product.Get(u => u.CategoryId == id);
            if (ObjectFromDb == null)
            {
                return NotFound();
            }


            return View(ObjectFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? obj = _unitOfWork.Product.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }


        #region APICALLS

        public IActionResult GetAll()
        {
            List<Product> objList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objList});
        }

        [HttpDelete]
        public IActionResult Remove(int?id)
        {
            Product ProductToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);
            if(ProductToBeDeleted == null)
            {
                return Json(new {success  = false,message="Error While Deleting"});
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, ProductToBeDeleted.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(ProductToBeDeleted);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfully" });
        }

        #endregion

    }
}

using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objList =_db.Categories.ToList();

            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //custom validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly the same");
            }

            //if (obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is not valid");
            //}

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
            Category? objFormDb = _db.Categories.Find(id);
            Category? obj2 = _db.Categories.FirstOrDefault(u=> u.Id == id);
            Category? obj3 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            if(objFormDb == null)
            {
                return NotFound();
            }


            return View(objFormDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }


            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? ObjectFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
            if(ObjectFromDb == null)
            {
                return NotFound();
            }


            return View(ObjectFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int?id)
        {

            Category? obj = _db.Categories.FirstOrDefault(u=>u.Id ==id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

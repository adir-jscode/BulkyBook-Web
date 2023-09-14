using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using BulkyBookWeb.Repository;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public CompanyController(IUnitOfWork db)
        {
            _unitOfWork = db;
           
        }
        public IActionResult Index()
        {
            List<Company> objList = _unitOfWork.Company.GetAll().ToList();

            return View(objList);
        }
       

        //working with upsert

        public IActionResult Upsert(int?id)
        {
           

            if(id == null || id ==0)
            {
                //create
                return View(new Company());
            }

            else
            {
                //update

                Company obj = _unitOfWork.Company.Get(u=>u.CompanyId == id);
                return View(obj);
            }

           
        }





        [HttpPost]
        public IActionResult Create(Company companyobj)
        {

            if (ModelState.IsValid)
            {
               
             

                if(companyobj.CompanyId == 0)
                {
                    _unitOfWork.Company.Add(companyobj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyobj);
                }

               
                _unitOfWork.Save();
                TempData["success"] = "Company Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {


                return View(companyobj);
            }
           

        }


        [HttpPost]
        public IActionResult Edit(Company obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company Updated Successfully";
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
            Company? ObjectFromDb = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (ObjectFromDb == null)
            {
                return NotFound();
            }


            return View(ObjectFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Company? obj = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company Deleted Successfully";
            return RedirectToAction("Index");
        }


        #region APICALLS

        public IActionResult GetAll()
        {
            List<Company> objList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = objList});
        }

        [HttpDelete]
        public IActionResult Remove(int?id)
        {
            Company CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if(CompanyToBeDeleted == null)
            {
                return Json(new {success  = false,message="Error While Deleting"});
            }


            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfully" });
        }

        #endregion

    }
}

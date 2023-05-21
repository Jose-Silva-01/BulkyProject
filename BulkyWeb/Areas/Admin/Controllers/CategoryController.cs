using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {

        // Since we added some lines to the services container in "Program.cs", we are adding that to dependency injection.
        // Therefore, we don't need to create a new object of AppDbContext, since .net core already knows how to instantiate a db context
        // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        // To create a page we first have to create an action method that will be invoked and that will call the View
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Custom Validation --> Name and DisplayOrder can't be the same
            if (!obj.Name.IsNullOrEmpty() &&
                obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }

            // Custom Validation --> Name can't be equal to "Test
            // However, this time we won't pass a "key" to the AddModelError, therefore the error is not attached to any propertie
            //if (!obj.Name.IsNullOrEmpty() &&
            //    obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Category Name can't be equal to 'test'");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Category created successfully";
                // --- COOL FEATURE ---
                //    You can redirect to another controller, you just need to specify which on the second entre
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        // ? sign to let c# now that it can be null
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id); // can be done with every property
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["sucess"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}

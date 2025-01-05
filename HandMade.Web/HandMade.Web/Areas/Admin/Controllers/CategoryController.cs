using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Categories= unitOfWork.CategoryRepo.GetAll();
            if(Categories == null)
            {
                NotFound();
            }
            return View("Index",Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.CategoryRepo.Add(category);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View("Create", category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category= unitOfWork.CategoryRepo.GetOne(c=>c.ID==id);
            return View("Edit",category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInAction(Category category)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.CategoryRepo.Update(category);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = unitOfWork.CategoryRepo.GetOne(c => c.ID == id);
            return View("Delete", category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteInAction(Category category)
        {
            unitOfWork.CategoryRepo.Remove(category);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}


using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetProductsJson()
        {
            var productsWithCategory = unitOfWork.ProductRepo.GetAll(include: "Category");
            return Json(new {data=productsWithCategory});
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var productCategoryListVM = unitOfWork.ProductRepo.PrepareProdCatViewModel();
            if(productCategoryListVM == null)
            {
               return NotFound();
            }
            return View("Create",productCategoryListVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryListViewModel productCategoryListViewModel, IFormFile img)
        {
            if(ModelState.IsValid)
            {
                string RootPath = webHostEnvironment.WebRootPath;
                if(img != null)
                {
                    string imgName= Guid.NewGuid().ToString();
                    string UploadPath = Path.Combine(RootPath, @"Images\Products");
                    string extension = Path.GetExtension(img.FileName);
                    using(FileStream filestream = new FileStream(Path.Combine(UploadPath,imgName+extension),FileMode.Create))
                    {
                        img.CopyTo(filestream);
                    }
                    productCategoryListViewModel.product.ImgUrl= @"Images\Products\"+imgName +extension;
                }
                unitOfWork.ProductRepo.Add(productCategoryListViewModel.product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            productCategoryListViewModel.categories = unitOfWork.CategoryRepo.GetAll();
            return View("Create", productCategoryListViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productInDb= unitOfWork.ProductRepo.GetOne(p=>p.ID==id);
            if(productInDb == null)
            {
                return NotFound();
            }
            var prodcutwithCategories = unitOfWork.ProductRepo.PrepareProdCatViewModel(productInDb);
            return View("Edit", prodcutwithCategories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInAction(ProductCategoryListViewModel productVM , IFormFile? img)
        {
            if(ModelState.IsValid)
            {
                string RootPath = webHostEnvironment.WebRootPath;
                if (img != null)
                {
                    string imgName = Guid.NewGuid().ToString();
                    string UploadPath = Path.Combine(RootPath, @"Images\Products");
                    string extension = Path.GetExtension(img.FileName);
                    using (FileStream filestream = new FileStream(Path.Combine(UploadPath, imgName + extension), FileMode.Create))
                    {
                        img.CopyTo(filestream);
                    }
                    productVM.product.ImgUrl = @"Images\Products\" + imgName + extension;
                }
                unitOfWork.ProductRepo.Update(productVM.product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            productVM.categories = unitOfWork.CategoryRepo.GetAll();
            return View("Edit", productVM);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product= unitOfWork.ProductRepo.GetOne(p=>p.ID == id);
            if(product == null)
            {
                return NotFound();
            }
            return View("Delete",product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteInAction(int id)
        {
            var product = unitOfWork.ProductRepo.GetOne(p => p.ID == id);
            unitOfWork.ProductRepo.Remove(product);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}

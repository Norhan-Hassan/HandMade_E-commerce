
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
        public IActionResult Index()
        {
            var products= unitOfWork.ProductRepo.GetAll();
            if(products == null)
            {
                NotFound();
            }
            return View("Index",products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var productCategoryListVM = unitOfWork.ProductRepo.PrepareViewModel();
            if(productCategoryListVM == null)
            {
                NotFound();
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
    }
}

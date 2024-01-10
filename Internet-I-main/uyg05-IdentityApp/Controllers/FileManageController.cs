using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Data;
using System.IO;
using uyg05_IdentityApp.Models;
using uyg05_IdentityApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace uyg05_IdentityApp.Controllers
{
    public class FileManageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;

        public FileManageController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, AppDbContext appDbcontext, IFileProvider fileProvider, AppDbContext context)
        {
            _context = appDbcontext;
            _roleManager = roleManager;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var fileManageModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,
                CategoryName = x.Category.Name,

            }).ToList();
            return View(fileManageModel);
        }

        public IActionResult Insert()
        {
            List<SelectListItem> kategoriname = (from x in _context.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.CategoryId.ToString()
                                                 }).ToList();
            ViewBag.kategoriname = kategoriname;
            return View();
        }

        [HttpPost]
        public IActionResult Insert(FileManageModel model)
        {
            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var fileUrl = "-";
            if (model.File.Length > 0 && model.File != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
                var dosyayolu = Path.Combine(rootfolder.First(x => x.Name == "Files").PhysicalPath, fileName);
                using var stream = new FileStream(dosyayolu, FileMode.Create);
                model.File.CopyTo(stream);
                fileUrl = fileName;
            }
            else
            {
                Console.WriteLine("Dosya seçilmedi veya link boş.");
            }



            var fileManage = new FileManage();
            fileManage.Name = model.Name;
            fileManage.Description = model.Description;
            fileManage.Link = fileUrl;
            fileManage.CategoryId = model.CategoryId;
            _context.FileManagers.Add(fileManage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            var fileModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,

            }).SingleOrDefault(x => x.Id == id);
            return View(fileModel);
        }

        [HttpPost]
        public IActionResult Edit(FileManageModel model)
        {
            var filemanage = _context.FileManagers.SingleOrDefault(x => x.Id == model.Id);
            filemanage.Name = model.Name;
            filemanage.Description = model.Description;
            filemanage.Link = model.Link;


            _context.FileManagers.Update(filemanage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var fileManageModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,

            }).SingleOrDefault(x => x.Id == id);
            return View(fileManageModel);
        }

        [HttpPost]
        public IActionResult Delete(FileManageModel model)
        {
            var filemanage = _context.FileManagers.SingleOrDefault(x => x.Id == model.Id);
            _context.FileManagers.Remove(filemanage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            var fileModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,

            }).SingleOrDefault(x => x.Id == id);
            return View(fileModel);

        }
        public IActionResult Users()
        {
            return View();
        }

        [Authorize]

        public IActionResult IndexForUser()
        {
            var filesModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,

            }).ToList();
            return View(filesModel);
        }

        public IActionResult DisplayForUser(int id)
        {
            var fileModel = _context.FileManagers.Select(x => new FileManageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Link = x.Link,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,

            }).SingleOrDefault(x => x.Id == id);
            return View(fileModel);

        }
        public IActionResult GetCategoryList()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category model)
        {
            var categoryExists = _context.Categories.FirstOrDefault(c => c.Name == model.Name);
            if (categoryExists == null)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
            }

            return RedirectToAction("GetCategoryList");
        }

    }
}

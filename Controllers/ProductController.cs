using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PhaseOne.Models;
using PhaseOne.viewModel;
using System.IO;

namespace PhaseOne.Controllers
{
    public class ProductController : Controller
    {

        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Product
        public ActionResult Index(String filtercategory)
        {
            List<Product> products = new List<Product>();
            if (filtercategory == null || filtercategory == "All")
            {
                products = _context.Products.Where(x => x.Id >0 ).ToList();
            }
            else
            {
                int categoryid = _context.Categories.Where(x => x.Name == filtercategory).Select(l => l.Id).SingleOrDefault();
                products = _context.Products.Where(x => x.CategoryId == categoryid).ToList();
                return View(products);
            }
            return View(products);
        }

        public ActionResult addProduct ()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new NewProductViewModel
            {
                Categories = categories,
                Product = new Product()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add ( Product product , HttpPostedFileBase upload )
        {
             bool NoPic = false;
             int categorychanged = 0;
             if(!ModelState.IsValid)
             {
                 var viewModel = new NewProductViewModel
                 {
                     Categories = _context.Categories.ToList(),
                     Product = product
                 };
                 return View(viewModel);
             }
             if (upload != null)
             {
                 String path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                 upload.SaveAs(path);
                 product.Image = upload.FileName;
             }
             else
                 NoPic = true;

             if (product.Id == 0)
             {
                 if(NoPic)
                     product.Image = "intial.png";
                 else
                     product.Image = upload.FileName;
                 _context.Products.Add(product);
             }
             else
             {
                 var productInDb = _context.Products.Single(p => p.Id == product.Id);
                 productInDb.Name = product.Name;
                if (!NoPic)
                {
                    if (productInDb.Image != "intial.png")
                    {
                        var filename = Server.MapPath("~/Uploads/" + productInDb.Image);
                        if ((System.IO.File.Exists(filename)))
                            System.IO.File.Delete(filename);
                    }
                    productInDb.Image = product.Image;
                }
                 productInDb.Description = product.Description;
                 productInDb.Price = product.Price;
                 if (productInDb.CategoryId != product.CategoryId)
                     categorychanged = productInDb.CategoryId;
                 productInDb.CategoryId = product.CategoryId;
             }
             if (categorychanged != 0 || product.Id == 0)
             {
                 Category Newcategory = new Category();
                 Newcategory = _context.Categories.Where(x => x.Id == product.CategoryId).Single();
                 Newcategory.No_Of_Products = Newcategory.No_Of_Products + 1;
                 _context.Categories.Attach(Newcategory);
                 _context.Entry(Newcategory).Property(x => x.No_Of_Products).IsModified = true;
                 _context.SaveChanges();
                 if(categorychanged !=0)
                 {
                     Category old = new Category();
                     old = _context.Categories.Where(x => x.Id == categorychanged).Single();
                     old.No_Of_Products = old.No_Of_Products - 1;
                     _context.Categories.Attach(old);
                     _context.Entry(old).Property(x => x.No_Of_Products).IsModified = true;
                 }
             }
             _context.SaveChanges();
             return RedirectToAction("Index", "Product");
        }

        public ActionResult Details ( int id)
        {
            var product = _context.Products.Include(p => p.Category)
                .SingleOrDefault(p => p.Id == id);

            return View(product);
        }

        public ActionResult Edit (int id)
        {
            var product = _context.Products.Single(p => p.Id == id);

            var viewModel = new NewProductViewModel
            {
                Product = product,
                Categories = _context.Categories.ToList()
            };

            return View("Edit", viewModel);
        }

        public ActionResult Delete ( int id)
        {
            var productInDb = _context.Products.SingleOrDefault(c => c.Id == id);

            if (productInDb == null)
                return  HttpNotFound();
            if (productInDb.Image != "intial.png")
            {
                var filename = Server.MapPath("~/Uploads/" + productInDb.Image);
                if ((System.IO.File.Exists(filename)))
                    System.IO.File.Delete(filename);
            }
            _context.Products.Remove(productInDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
       



    }
}
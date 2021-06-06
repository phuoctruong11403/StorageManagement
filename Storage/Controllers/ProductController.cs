using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Storage.Models;
using Storage.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Storage.Controllers
{
    public class ProductController : Controller
    {
        private IDbRepository _DbRepository;
        private IHostingEnvironment _environment;

        public ProductController (IDbRepository repository, IHostingEnvironment environment)
        {
            _DbRepository = repository;
            _environment = environment;
        }

        public IActionResult ShowAll()
        {
            return View(_DbRepository.GetProducts());
        }

        public IActionResult Details (int id)
        {
            var product = _DbRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        private void PopulateClassesDropDownList(int? selectedClass = null)
        {
            var classes = _DbRepository.PopulateClassesDropDownList();
            ViewBag.ClassID = new SelectList(classes.AsNoTracking(), "ClassID", "ClassName", selectedClass);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateClassesDropDownList();
            return View();
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost(Product product)
        {
            if (ModelState.IsValid)
            {
                _DbRepository.CreateProduct(product);
                return RedirectToAction(nameof(ShowAll));
            }
            PopulateClassesDropDownList(product.ClassID);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _DbRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateClassesDropDownList(product.ClassID);
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var productToUpdate = _DbRepository.GetProductById(id);
            bool isUpdated = await TryUpdateModelAsync<Product>(
                                    productToUpdate,
                                    "",
                                    c => c.ProductType,
                                    c => c.ProductName,
                                    c => c.Brand,
                                    c => c.Price);
            if (isUpdated == true)
            {
                _DbRepository.SaveChanges();
                return RedirectToAction(nameof(ShowAll));
            }
            PopulateClassesDropDownList(productToUpdate.ClassID);
            return View(productToUpdate);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _DbRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _DbRepository.DeleteProduct(id);
            return RedirectToAction(nameof(ShowAll));
        }

        public IActionResult GetImage(int id)
        {
            Product requestedProduct = _DbRepository.GetProductById(id);
            if (requestedProduct != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requestedProduct.ImageName;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedProduct.ImageMimeType);
                }
                else
                {
                    if (requestedProduct.PhotoFile.Length > 0)
                    {
                        return File(requestedProduct.PhotoFile, requestedProduct.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}

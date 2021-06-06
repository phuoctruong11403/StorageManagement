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
    public class ClassController : Controller
    {
        private IDbRepository _DbRepository;
        private IHostingEnvironment _environment;

        public ClassController(IDbRepository repository, IHostingEnvironment environment)
        {
            _DbRepository = repository;
            _environment = environment;
        }

        public IActionResult ShowAll()
        {
            return View(_DbRepository.GetClasses());
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
        public IActionResult CreatePost(Class aClass)
        {
            if (ModelState.IsValid)
            {
                _DbRepository.CreateClass(aClass);
                return RedirectToAction(nameof(ShowAll));
            }
            PopulateClassesDropDownList(aClass.ClassID);
            return View(aClass);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _DbRepository.GetClassById(id);
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
            var productToUpdate = _DbRepository.GetClassById(id);
            bool isUpdated = await TryUpdateModelAsync<Class>(
                                    productToUpdate,
                                    "",
                                    c => c.ClassID,
                                    c => c.ClassName);
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
            var aClass = _DbRepository.GetClassById(id);
            if (aClass == null)
            {
                return NotFound();
            }
            return View(aClass);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _DbRepository.DeleteClass(id);
            return RedirectToAction(nameof(ShowAll));
        }
    }
}

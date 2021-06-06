using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Storage.Data;
using Storage.Models;
using Microsoft.EntityFrameworkCore;
namespace Storage.Repositories
{
    public class DbRepository : IDbRepository
    {
        private ProductContext _context;

        public DbRepository (ProductContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts ()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById (int id)
        {
            return _context.Products.Include(c => c.Class).SingleOrDefault(p => p.ProductID == id);
        }

        public void CreateProduct(Product product)
        {
            if (product.PhotoAvatar != null && product.PhotoAvatar.Length > 0)
            {
                product.ImageMimeType = product.PhotoAvatar.ContentType;
                product.ImageName = Path.GetFileName(product.PhotoAvatar.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    product.PhotoAvatar.CopyTo(memoryStream);
                    product.PhotoFile = memoryStream.ToArray();
                }
                _context.Add(product);
                _context.SaveChanges();
            }
        }

        public void DeleteProduct (int id)
        {
            // Get the product from the database
            var product = _context.Products.SingleOrDefault(p => p.ProductID == id);
            // Remove corresponding product
            _context.Products.Remove(product);
            // Save changes
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Class> GetClasses()
        {
            return _context.Classes.ToList();
        }

        public Class GetClassById(int id)
        {
            return _context.Classes.SingleOrDefault(p => p.ClassID == id);
        }

        public void CreateClass(Class aClass)
        {
            var result = from s in _context.Classes
                         where s.ClassID == aClass.ClassID
                         select s;

            if (aClass != null && result.Count() == 0) //Include(c => c.ClassID).SingleOrDefault(p => p.ClassID == aClass.ClassID) == null)
            {
                _context.Add(aClass);
                _context.SaveChanges();
            }
        }

        public void DeleteClass(int id)
        {
            // Get the product from the database
            var aClass = _context.Classes.SingleOrDefault(c => c.ClassID == id);
            // Remove corresponding product
            _context.Classes.Remove(aClass);
            // Save changes
            _context.SaveChanges();
        }

        public IQueryable<Class> PopulateClassesDropDownList()
        {
            var classesQuery = from b in _context.Classes
                               orderby b.ClassName
                               select b;
            return classesQuery;
        }
    }
}

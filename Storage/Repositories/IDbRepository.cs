using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;

namespace Storage.Repositories
{
    public interface IDbRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void DeleteProduct(int id);
        void SaveChanges();

        IEnumerable<Class> GetClasses();
        Class GetClassById(int id);
        void CreateClass(Class aClass);
        void DeleteClass(int id);
        IQueryable<Class> PopulateClassesDropDownList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Class> Classes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Add data to Class table
            modelBuilder.Entity<Class>().HasData(
                new Class
                {
                    ClassID = 1,
                    ClassName = "Phones"
                },

                 new Class
                 {
                     ClassID = 2,
                     ClassName = "Appliances"
                 },

                  new Class
                  {
                      ClassID = 3,
                      ClassName = "Laptops"
                  },

                   new Class
                   {
                       ClassID = 4,
                       ClassName = "Tablets"
                   },

                    new Class
                    {
                        ClassID = 5,
                        ClassName = "Consoles"
                    }
            );

            // Add data to Product table
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductType = ProductType.iOS,
                    ProductName = "IphoneX",
                    Brand = "Apple",
                    Price = 399.99,
                    ClassID = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "iphoneX.jpg"
                },

                new Product
                {
                    ProductID = 2,
                    ProductType = ProductType.iOS,
                    ProductName = "IphoneXS",
                    Brand = "Apple",
                    Price = 499.99,
                    ClassID = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "iphoneXS.jpg"
                },

                new Product
                {
                    ProductID = 3,
                    ProductType = ProductType.iOS,
                    ProductName = "IphoneXSMax",
                    Brand = "Apple",
                    Price = 599.99,
                    ClassID = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "iphoneXSMax.jpg"
                },

                new Product
                {
                    ProductID = 4,
                    ProductType = ProductType.Android,
                    ProductName = "Samsung Galaxy Z Fold 2",
                    Brand = "Samsung",
                    Price = 1499.99,
                    ClassID = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "SamsungZFold2.jpg"
                },

                new Product
                {
                    ProductID = 5,
                    ProductType = ProductType.Android,
                    ProductName = "Samsung Galaxy S21+",
                    Brand = "Samsung",
                    Price = 1199.99,
                    ClassID = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "SamsungS21Plus.jpg"
                }
            ) ;

        }
    }


}

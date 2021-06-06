using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please select a product type")]
        [Display(Name = "Product Type:")]
        public ProductType? ProductType { get; set; }

        [Required(ErrorMessage = "Please enter a product description")]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Name:")]
        public string ProductName { get; set; }

        [Display(Name = "Brand:")]
        public String Brand { get; set; }

 
        [Required(ErrorMessage = "Please enter a valid price: ")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price:")]
        public double? Price { get; set; }

        [NotMapped]
        [Display(Name = "Product Picture:")]
        public IFormFile PhotoAvatar { get; set; }

        public string ImageName { get; set; }

        public byte[] PhotoFile { get; set; }

        public string ImageMimeType { get; set; }

        [Required(ErrorMessage = "Please select a class")]
        [ForeignKey("ClassID")]
        public int? ClassID { get; set; }

        public virtual Class Class { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Display(Name ="List of price")]
        [Range(1,1000)]
        public int ListPrice { get; set; }
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public int Price { get; set; }
        [Display(Name = "Price for 50 +")]
        [Range(1, 1000)]
        public int Price50 { get; set; }

        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public int Price100 { get; set; }
       
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        //[ValidateNever]
        public Category? Category { get; set; }

        //[ValidateNever]
        public string? ImageUrl { get; set; }    

    }
}

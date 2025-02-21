﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPDotNetCoreMvcRestaurantWebApp.Models
{
    public class Product
    {
        public Product()
        {
            ProductIngredients = new List<ProductIngredient>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
        [ValidateNever]
        public Category? Category { get; set; }
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }
        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients { get; set; }
    }
}
﻿using MiWebObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MiWebObj.Controllers
{
    public class ProductController : ApiController
    {
        Product[] products = new Product[] {
            new Product {Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1},
            new Product {Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.7M},
            new Product {Id = 3, Name="Hammer", Category = "Hardware", Price = 16.99M}
        };
        public IEnumerable<Product> GetAllProducts() {
            return products;
        }
        public IHttpActionResult GetProduct(int Id) {
            var Product = products.FirstOrDefault((p) => p.Id == Id);
            if (Product == null) return NotFound();
            return Ok(Product);
        }
    }
}
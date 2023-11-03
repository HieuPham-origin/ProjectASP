﻿using System.Collections.Generic;

namespace Fashion.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<IFormFile> ProductImages { get; set; }
    }
}
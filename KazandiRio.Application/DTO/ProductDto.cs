using KazandiRio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}

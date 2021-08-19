using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.DTO
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public int[] ProductList { get; set; }
    }
}

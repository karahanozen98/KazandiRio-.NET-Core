﻿using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.ProductModule.Queries
{
    public class GetProductByIdQuery: IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}

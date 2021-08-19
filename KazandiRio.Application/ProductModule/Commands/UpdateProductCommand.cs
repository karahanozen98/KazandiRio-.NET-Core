using KazandiRio.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.ProductModule.Commands
{
    class UpdateProductCommand : IRequest<Boolean>
    {
        public Product Product { get; set; }
    }
}

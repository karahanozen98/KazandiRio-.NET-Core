using KazandiRio.Application.DTO;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.ProductModule.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Boolean>
    {
        public UpdateProductDto Product { get; set; }
    }
}

using KazandiRio.Application.DTO;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.ProductModule.Commands.CreateProduct
{
    public class CreateProductCommand: IRequest<Boolean>
    {
        public CreateProductDto Product { get; set; }
    }
}

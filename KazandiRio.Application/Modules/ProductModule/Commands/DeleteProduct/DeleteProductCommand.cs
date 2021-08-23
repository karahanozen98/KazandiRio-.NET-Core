using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.Modules.ProductModule.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<Boolean>
    {
        public int ProductId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.ProductModule.Commands
{
    public class DeleteProductCommand: IRequest<Boolean>
    {
        public int ProductId { get; set; }
    }
}

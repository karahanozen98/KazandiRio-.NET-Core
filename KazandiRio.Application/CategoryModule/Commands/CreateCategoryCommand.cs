using KazandiRio.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.CategoryModule.Commands
{
    public class CreateCategoryCommand:IRequest<Boolean>
    {
        public Category Category { get; set; }
    }
}

using KazandiRio.Domain.Entities;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.CategoryModule.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Boolean>
    {
        public Category Category { get; set; }
    }
}

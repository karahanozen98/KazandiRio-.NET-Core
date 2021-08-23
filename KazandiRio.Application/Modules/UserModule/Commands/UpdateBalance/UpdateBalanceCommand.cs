using MediatR;
using System;

namespace KazandiRio.Application.Modules.UserModule.Commands.UpdateBalance
{
    public class UpdateBalanceCommand : IRequest<Boolean>
    {
        public int UserId { get; set; }
        public float Amount { get; set; }
    }
}

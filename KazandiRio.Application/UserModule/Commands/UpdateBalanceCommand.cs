using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Commands
{
    public class UpdateBalanceCommand: IRequest<Boolean>
    {
        public int UserId { get; set; }
        public float Amount { get; set; }
    }
}

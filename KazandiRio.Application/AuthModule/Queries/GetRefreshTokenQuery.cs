using KazandiRio.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.AuthModule.Queries
{
    public class GetRefreshTokenQuery: IRequest<RefreshToken>
    {
        public string TokenString { get; set; }
    }
}

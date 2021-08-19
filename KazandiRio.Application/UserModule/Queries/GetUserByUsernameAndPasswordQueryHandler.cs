﻿using KazandiRio.Application.AuthModule;
using KazandiRio.Application.AuthModule.Commands;
using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.UserModule.Queries
{
    class GetUserByUsernameAndPasswordQueryHandler : IRequestHandler<GetUserByUsernameAndPasswordCommand, UserDto>
    {
        private readonly ApplicationDBContext _db;
        private readonly IMediator _mediatr;

        public GetUserByUsernameAndPasswordQueryHandler(ApplicationDBContext db, IMediator mediatr)
        {
            _db = db;
            _mediatr = mediatr;
        }
        public async Task<UserDto> Handle(GetUserByUsernameAndPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.User.FirstOrDefaultAsync(m => m.Username == request.Username && m.Password == request.Password);
            if (user == null)
                throw new Exception("Wrong username or password");
        
            // Authentication && create JWT token.
            RefreshToken refreshToken = await _mediatr.Send(new GenerateJwtTokenCommand { User = user, IpAdress = request.IpAddress });
            user.TokenId = refreshToken.Id; // assign new token to the user
            _db.Update(user);
            await _db.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Balance = user.Balance,
                Rewards = user.Rewards,
                Token = refreshToken.Token,
            };
        }
    }
}

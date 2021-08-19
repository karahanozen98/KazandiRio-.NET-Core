using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public float Balance { get; set; }
        public float Rewards { get; set; }
        public string Token { get; set; }
    }
}

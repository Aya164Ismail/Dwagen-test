using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dwagen.DTO.Users
{
    public class LoginStateDto
    {
        public bool LoginSuccessfully { get; set; }
        public string Token { get; set; }
        public List<Claim> Permissions { get; set; }
        public string ErrorMessage { get; set; } = "Can Not Login .. Invalid Username Or Password !!";
    }
}

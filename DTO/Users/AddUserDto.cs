using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Users
{
    public class AddUserDto
    {
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        public string Password { get; set; }
        public int Wallet { get; set; }
        public int Earnings { get; set; }
        public IFormFile File { get; set; }

    }
}

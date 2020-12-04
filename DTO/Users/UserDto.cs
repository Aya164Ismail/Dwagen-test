using Dwagen.Model.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Users
{
    public class UserDto : BaseEntity
    {
        public string NumberPhone { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Wallet { get; set; }
        public int? Earnings { get; set; }
    }
}

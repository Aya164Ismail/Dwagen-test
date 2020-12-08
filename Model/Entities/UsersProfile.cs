using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Dwagen.Model.BaseService;

namespace Dwagen.Model.Entities
{
    public class UsersProfile : BaseEntity
    {
        public string NumberPhone { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Wallet { get; set; }
        public int Earnings { get; set; }
    }
}

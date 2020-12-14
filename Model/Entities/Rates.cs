using Dwagen.Model.BaseService;
using Dwagen.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Model.Entities
{
    public class Rates : BaseEntity
    {
        public RateValues ProductRate { get; set; }
        public RateValues DelevaryTimeRate { get; set; }
        public string Discription { get; set; }
        public Guid? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Products Products { get; set; }

        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UsersProfile UsersProfile { get; set; }
    }
}

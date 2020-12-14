using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwagen.Model.Enum;
using Dwagen.Model.BaseService;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwagen.Model.Entities
{
    public class Products : BaseEntity
    {
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public float? KiloOfProduct { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }

        //## Relation starts ##
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UsersProfile UsersProfile { get; set; }
    }
}

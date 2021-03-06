using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Dwagen.Model.BaseService;
using Dwagen.Model.Enum;

namespace Dwagen.Model.Entities
{
    public class Orders : BaseEntity
    {
        public int OrderQuantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public float OrderPrice { get; set; }
        public OrderStatues OrderStatues { get; set; }
        public DeliveryState DeliveryState { get; set; }
        public float? KiloOfOrder { get; set; }
        public float Shipping { get; set; }

        //### Relation starts ###
        public Guid? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Products Products { get; set; }

        public Guid? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UsersProfile UsersProfile { get; set; }
    }
}

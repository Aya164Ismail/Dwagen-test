using Dwagen.DTO.Products;
using Dwagen.DTO.Users;
using Dwagen.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Orders
{
    public class AddOrderDto
    {
        public int OrderQuantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public float OrderPrice { get; set; }
        public float? KiloOfOrder { get; set; }
        public OrderStatues OrderStatues { get; set; }
        public DeliveryState DeliveryState { get; set; }

        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
    }
}

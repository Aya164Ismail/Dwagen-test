using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Orders
{
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime DeliveryDate { get; set; } 
    }
}

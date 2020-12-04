using Dwagen.DTO.Products;
using Dwagen.DTO.Users;
using Dwagen.Model.BaseService;
using Dwagen.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Orders
{
    public class OrderDto : BaseEntity
    {
        public int OrderQuantity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int OrderPrice { get; set; }
        public OrderStatues OrderStatues { get; set; }
        public DeliveryState DeliveryState { get; set; }

        public Guid? ProductId { get; set; }
        public ProductDto Products { get; set; }
        public Guid? UserId { get; set; }
        public UserDto Users { get; set; }
    }
}

using Dwagen.DTO.Users;
using Dwagen.Model.BaseService;
using Dwagen.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Products
{
    public class ProductDto : BaseEntity
    {
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPrice { get; set; }
        public Guid? UserId { get; set; }
        public UserDto Users { get; set; }
    }
}

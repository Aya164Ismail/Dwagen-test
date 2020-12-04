using Dwagen.Model.BaseService;
using Dwagen.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.DTO.Products
{
    public class UpdateProductDto : BaseEntity
    {
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPrice { get; set; }
    }
}

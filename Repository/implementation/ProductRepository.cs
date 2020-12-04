using Dwagen.Model;
using Dwagen.Model.Entities;
using Dwagen.Repository.Generic;
using Dwagen.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Repository.implementation
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        public ProductRepository(DwagenContext context)
            : base(context)
        {
        }
    }
}

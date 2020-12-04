using Dwagen.Model.Entities;
using Dwagen.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Products>
    {
    }
}

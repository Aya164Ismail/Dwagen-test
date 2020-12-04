using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dwagen.Repository.Generic
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        /// <summary>
        /// Create Function For Create Entity Asynchronous
        /// </summary>
        /// <param name="entity"></param>
        Task CreateAsync(Entity entity);
        /// <summary>
        /// Update Function If Entity Excist => Update , If Not => Create
        /// </summary>
        /// <param name="entity"></param>
        void Update(Entity entity);
        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Entity entity);
        /// <summary>
        /// Find Item By ID Asynchronous
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Entity> FindByIdAsync(Guid? Id);
        /// <summary>
        /// Get Single Item Using Expression Asynchronous
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="Includes"></param>
        /// <returns></returns>
        Task<Entity> FindElementAsync(Expression<Func<Entity, bool>> expression , string includes = null);
        /// <summary>
        /// Get More Items Using Expression Asynchronous
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="Includes"></param>
        /// <returns></returns>
        Task<IEnumerable<Entity>> GetElementsAsync(Expression<Func<Entity, bool>> expression , string includes = null);
    }
}

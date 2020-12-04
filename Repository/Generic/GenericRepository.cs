using Dwagen.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dwagen.Repository.Generic
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected  DwagenContext _context { get; set; }
        private readonly DbSet<Entity> _entities;

        public GenericRepository(DwagenContext context)
        {
            _context = context ;
            _entities = _context.Set<Entity>();
        }

        public virtual async Task CreateAsync(Entity entity)
        {
            await _entities.AddAsync(entity);
        }

        public virtual void Update(Entity entity)
        {
            _entities.Update(entity);
        }

        public virtual void Delete(Entity entity)
        {
            _entities.Remove(entity);
        }
        public async Task<Entity> FindByIdAsync(Guid? Id)
        {
            return await _entities.FindAsync(Id);
        }

        public async Task<Entity> FindElementAsync(Expression<Func<Entity, bool>> expression, string includes = null)
        {
            var Element =  await _entities.Where(expression).MultiInclude(includes).FirstOrDefaultAsync();
            return Element;
        }

        public async Task<IEnumerable<Entity>> GetElementsAsync(Expression<Func<Entity, bool>> expression, string includes = null)
        {            
            var entities = await _entities.Where(expression).MultiInclude(includes).ToListAsync();
            return entities;
        }
    }

    public static class Include
    {
        /// <summary>
        /// Extention Method For Create Multi Include For Entities
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<Entity> MultiInclude <Entity>(this IQueryable<Entity> query,string includes = null) where Entity : class
        {
            if (!string.IsNullOrWhiteSpace(includes))
            {
                foreach (var item in includes.Split(','))
                {
                    query = query.Include(item);
                }
            }          
            return query;
        }
    }
}

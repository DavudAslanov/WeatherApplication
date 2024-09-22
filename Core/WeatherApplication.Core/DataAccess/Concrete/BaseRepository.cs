using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Core.DataAccess.Abstract;
using WeatherApplication.Core.Entities.Concrete;

namespace WeatherApplication.Core.DataAccess.Concrete
{
    public class BaseRepository<TEntity, TContext> : IBaseInterface<TEntity>
        where TEntity: BaseEntity, new()
        where TContext: DbContext, new()
    {
        public void AddAsync(TEntity entity)
        {
            using (TContext context =new TContext())
            {
                var Added = context.Entry(entity);
                Added.State = EntityState.Added;
                context.SaveChanges();
                
            }
        }
        public  void DeleteAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var Deleted = context.Entry(entity);
                Deleted.State = EntityState.Deleted;
                context.SaveChanges();
            }
            
        }
        public List<TEntity> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContext context = new TContext())
            {
                if (filter == null)
                {
                    return context.Set<TEntity>().ToList();
                }
                else
                {
                    return context.Set<TEntity>().Where(filter).ToList();
                }
            }
        }
        public TEntity GetById(int id)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(x=>x.Id==id);
            }
        }
        public void UpdateAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var Update = context.Entry(entity);
                Update.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}


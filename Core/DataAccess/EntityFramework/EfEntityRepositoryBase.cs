using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity: class, IEntity, new() //Kısıtlamalarımızı yaptık 
        where TContext: DbContext, new() // Kısıtlamalarımızı yaptık
    {
        public void Add(TEntity entity)
        {
            //IDispossible pattern implementation of c#
            using (TContext context = new TContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var addedEntity = context.Entry(entity);//Referansı yakala
                addedEntity.State = EntityState.Added;//Ekleme
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var deletedEntity = context.Entry(entity);//Referansı yakala
                deletedEntity.State = EntityState.Deleted;//Sil
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();//select*from Products ile aynı işlem
            }
        }

        public void Update(TEntity entitiy)
        {
            using (TContext context = new TContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var updatedEntity = context.Entry(entitiy);//Referansı yakala
                updatedEntity.State = EntityState.Modified;//Update 
                context.SaveChanges();
            }
        }
    }
}

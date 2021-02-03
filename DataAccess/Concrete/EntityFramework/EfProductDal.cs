using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        //NuGet
        public void Add(Product entity)
        {
            //IDispossible pattern implementation of c#
            using (NorthwindContext context = new NorthwindContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var addedEntity = context.Entry(entity);//Referansı yakala
                addedEntity.State = EntityState.Added;//Ekleme
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var deletedEntity = context.Entry(entity);//Referansı yakala
                deletedEntity.State = EntityState.Deleted;//Sil
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null 
                    ? context.Set<Product>().ToList() 
                    : context.Set<Product>().Where(filter).ToList();//select*from Products ile aynı işlem
            }
        }

        public void Update(Product entitiy)
        {
            using (NorthwindContext context = new NorthwindContext())//using nesneler içine yazılıp iş bittikten sonra bellekten atılacak
            {
                var updatedEntity = context.Entry(entitiy);//Referansı yakala
                updatedEntity.State = EntityState.Modified;//Update 
                context.SaveChanges();
            }
        }
    }
}

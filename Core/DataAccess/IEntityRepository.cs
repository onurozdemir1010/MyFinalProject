﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess // Core katmanı diğer katmanları referans almaz. Bir katmana bağımlı kalmaz
{
    //Generic constrain(kısıtlama)
    //class : referans tip olabilir
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new() : newlenebilir olmalı
    public interface IEntityRepository<T> where T:class,IEntity,new()//Generic
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);//filtreler yazabilmeni sağlıyor
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entitiy);
        void Delete(T entity);
    }
}

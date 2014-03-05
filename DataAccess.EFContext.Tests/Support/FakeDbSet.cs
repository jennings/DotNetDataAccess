using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFContext.Tests.Support
{
    class FakeDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private readonly ObservableCollection<TEntity> backingList = new ObservableCollection<TEntity>();

        public ICollection<TEntity> BackingCollection { get { return backingList; } }

        public TEntity Add(TEntity entity)
        {
            backingList.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            backingList.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            // I hope this works
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public TEntity Create()
        {
            // I hope this works
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            // I sure hope I never have a composite key
            var firstKeyValue = keyValues.First();
            var idProperty =
                typeof(TEntity).GetProperties()
                .First(prop => prop.GetCustomAttribute<KeyAttribute>() != null);

            return backingList.First(entity => idProperty.GetValue(entity) == firstKeyValue);
        }

        public System.Collections.ObjectModel.ObservableCollection<TEntity> Local
        {
            get { return backingList; }
        }

        public TEntity Remove(TEntity entity)
        {
            backingList.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)backingList).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(TEntity); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return backingList.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return backingList.AsQueryable().Provider; }
        }
    }
}

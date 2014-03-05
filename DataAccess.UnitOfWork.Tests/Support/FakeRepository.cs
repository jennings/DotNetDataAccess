using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.UnitOfWork.Data;

namespace DataAccess.UnitOfWork.Tests.Support
{
    class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ICollection<TEntity> BackingCollection { get; private set; }

        public FakeRepository()
        {
            BackingCollection = new List<TEntity>();
        }

        public void Add(TEntity entity)
        {
            BackingCollection.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            BackingCollection.Remove(entity);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)BackingCollection).GetEnumerator();
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
            get { return BackingCollection.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return BackingCollection.AsQueryable().Provider; }
        }
    }
}

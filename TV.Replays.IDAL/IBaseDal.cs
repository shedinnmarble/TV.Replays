using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TV.Replays.IDAL
{
    public interface IBaseDal<T> where T : class,new()
    {
        void Insert(T t);

        void Delete(Expression<Func<T, bool>> predicate);

        void Update(T t, Expression<Func<T, bool>> predicate);

        T Single(Expression<Func<T, bool>> predicate);

        List<T> FindAll(Expression<Func<T, bool>> predicate);

        List<T> FindAll<O>(int pageIndex, int pageSize, out int count,
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, O>> selector,
            bool isDesc);
    }
}

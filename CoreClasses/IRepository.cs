using System.Collections.Generic;

namespace IEduZimAPI.CoreClasses
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T Get<TKey>(TKey id);
        T Add(T item, string user = "");
        T Update<TKey>(TKey id, T item, string user = "");
        void Delete<TKey>(TKey id);
        Paginator<T> GetPaged(PageRequest request);
    }
}

using IEduZimAPI.Models;

namespace IEduZimAPI.CoreClasses
{
    public class BaseService<T> : Repository<T> where T : class
    {
        public BaseService(IEduContext context) : base(context) { }

        public BaseService() : this(new IEduContext()) { }
    }
}
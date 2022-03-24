using System.Collections.Generic;

namespace IEduZimAPI.CoreClasses
{
    public class Paginator<T>
    {
        public PageRequest PageRequest { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Paginator(PageRequest request, int total, IEnumerable<T> data)
        {
            PageRequest = request;
            Total = total;
            Data = data;
        }

    }
}

using System;

namespace IEduZimAPI.CoreClasses
{
    public class Pagination<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Data { set; get; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public object AdditionalData { get; set; }
        public Pagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public Pagination(T data)
        {
            Succeeded = true;
            Data = data.GetType().GetProperty("Data").GetValue(data);
            PageNumber = GetRequest(data).PageNumber;
            PageSize = GetRequest(data).PageSize;
            AdditionalData = GetRequest(data).AdditionalData;
            TotalItems = (int)data.GetType().GetProperty("Total").GetValue(data);
            TotalPages = (int)Math.Ceiling((decimal)TotalItems / PageSize);
        }

        private PageRequest GetRequest(T data) => (PageRequest)data.GetType().GetProperty("PageRequest").GetValue(data);

        public Pagination(string message) => Message = message;

        public static Pagination<T> FromException(Exception exception)
        {
            if (exception == null) return new Pagination<T>("Error trying to get these records");
            var rootException = exception.GetBaseException();
            return new Pagination<T>(rootException.Message);
        }

        public static Pagination<T> FromObject(T obj)
        {
            return new Pagination<T>(obj);
        }
    }
}

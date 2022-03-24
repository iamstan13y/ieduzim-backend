using System;

namespace IEduZimAPI.CoreClasses
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { set; get; }
        public static Result<T> Success(string message) => new Result<T> { Succeeded = true, Message = message };

        public Result() { }

        public Result(string message) => Message = message;

        public Result(bool succeeded, string message, T data)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
        }

        public static Result<T> FromException(Exception exception)
        {
            if (exception == null) return new Result<T>();
            var rootException = exception.GetBaseException();
            return new Result<T>(rootException.Message);
        }

        public static Result<T> FromObject(T obj, string message)
        {
            return new Result<T>(true, message, obj);
        }
    }
}

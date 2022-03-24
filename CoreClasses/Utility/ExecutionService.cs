
using System;

namespace IEduZimAPI.CoreClasses
{
    public static class ExecutionService<T>
    {
        public static Result<T> Execute(Func<T> function, string message = "")
        {
            try
            {
                return Result<T>.FromObject(function(), message);
            }
            catch (Exception e)
            {
                return Result<T>.FromException(e);
            }
        }
    }

    public static class ExecutionService
    {
        public static Result<bool> Execute(this Action function, string message = "")
        {
            try
            {
                function();
                return Result<bool>.Success(message);
            }
            catch (Exception e)
            {
                return Result<bool>.FromException(e);
            }
        }
    }

    public static class PagedExecution<T>
    {
        public static Pagination<T> Execute(Func<T> function)
        {
            try
            {
                return Pagination<T>.FromObject(function());
            }
            catch (Exception e)
            {
                return Pagination<T>.FromException(e);
            }
        }
    }
}

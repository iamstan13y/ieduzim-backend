using IEduZimAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IEduZimAPI.CoreClasses
{
    public class BaseController<T, U> : ControllerBase where T: class, new() where U:class
    {
        public IRepository<T> service;
        public BaseController() : this(new IEduContext()) { }
        public BaseController(IEduContext context) =>
            service = new BaseService<T>(context);

        public BaseController(IRepository<T> service) =>
            this.service = service;

        [HttpGet]
        public virtual Result<IEnumerable<T>> Get() =>
            ExecutionService<IEnumerable<T>>.Execute(() => service.Get());

        [HttpGet]
        [Route("{id}")]
        public virtual Result<T> Get(int id) =>
            ExecutionService<T>.Execute(() => service.Get(id));

        [HttpPost]
        public virtual Result<T> Post([FromBody] U item) => 
            ExecutionService<T>.Execute(() => service.Add(SetDBProperties(item), HttpContext.User?.Identity?.Name), $"{Regex.Replace(typeof(T).Name, "(\\B[A - Z])", " $1")} successfully added.");

        [HttpPut]
        [Route("{id}")]
        public virtual Result<T> Update([FromBody] U item, int id)=>
           ExecutionService<T>.Execute(() => service.Update(id, SetDBProperties(item)), $"{Regex.Replace(typeof(T).Name, "(\\B[A - Z])", " $1")} successfully updated.");
     

        [HttpDelete]
        [Route("{id}")]
        public virtual Result<bool> Delete(int id) =>
            ExecutionService.Execute(() => service.Delete(id), $"{Regex.Replace(typeof(T).Name, "(\\B[A - Z])", " $1")} successfully deleted");

        [HttpGet]
        [Route("paged")]
        public virtual Pagination<Paginator<T>> GetPaged([FromQuery] PageRequest request) =>
           PagedExecution<Paginator<T>>.Execute(() => service.GetPaged(request));

        private T SetDBProperties(U item)
        {
            var postProps = typeof(T).GetProperties();
            T dbProps = new T();
            foreach (var prop in postProps)
            {
                var property = item.GetType().GetProperty(prop.Name);
                if (property != null)
                {
                    var val = property.GetValue(item);
                    prop.SetValue(dbProps, val);
                }
            }
            return dbProps;
        }
    }
}
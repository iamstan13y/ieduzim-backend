using IEduZimAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.CoreClasses.BaseFiles
{
    public class DatabaseService
    {
        readonly IServiceProvider provider;
        public DatabaseService(IServiceProvider provider)
        {
            this.provider = provider;
            using (var context = provider.GetRequiredService<IEduContext>())
                context.Database.EnsureCreated();
        }

        public static void Initialize(IServiceProvider provider) =>
            new DatabaseService(provider).SeedAsync();

        public void SeedAsync() => Task.Run(() => Seed());

        private void Seed()
        {
        }
    }

    public static class DatabaseService<T> where T : class
    {
        public static string ReadFile() =>
              File.ReadAllText($"{Directory.GetCurrentDirectory()}/Model/Objects/{typeof(T).Name.ToLower()}.json");

        public static List<T> Read() => JsonConvert.DeserializeObject<List<T>>(ReadFile());

        public static void Seed(IEnumerable<T> data = default, string user = "")
        {
            try
            {
                var service = new BaseService<T>();
                if (service.Get().Any()) return;
                data = data ?? Read();
                data.ToList().ForEach(value => service.Add(value, user));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SeedAsync(IEnumerable<T> data = default)
            => Task.Run(() => Seed(data));
    }
}

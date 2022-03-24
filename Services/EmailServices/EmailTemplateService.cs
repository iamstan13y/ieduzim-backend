using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IEduZimAPI.Services.EmailServices
{
    public static class EmailTemplateService
    {
        private static string ReadTemplate(int body)
        {
            string name = Enum.GetName(typeof(Models.Enums.EmailType), body).ToLower();
            var path = $"{Directory.GetCurrentDirectory()}/Services/EmailServices/Template/{name}.html";
            return File.ReadAllText(path);
        }

        public static string ProcessEmailTemplate(Dictionary<string, string> bodyDictionary, Models.Enums.EmailType type) =>
             ProcessBody(ReadTemplate((int)type), bodyDictionary);

        private static string ProcessBody(string templateBody, Dictionary<string, string> proccessedBody) =>
           proccessedBody.Keys.Aggregate(templateBody, (current, key) => current.Replace(key, proccessedBody[key]));
    }
}
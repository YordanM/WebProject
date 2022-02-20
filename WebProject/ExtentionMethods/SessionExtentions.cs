using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebProject.ExtentionMethods
{
    public static class SessionExtentions
    {
        public static void SetObject<T>(this ISession instance, string key, T value)
        {
            if (value == null)
            {
                instance.Remove(key);
                return;
            }
            string jsonData = JsonSerializer.Serialize(value);
            instance.SetString(key, jsonData);
        }

        public static T GetObject<T>(this ISession instance, string key)
            where T : class
        {
            string jsonData = instance.GetString(key);

            if (!instance.Keys.Contains(key))
            {
                return null;
            }

            if (String.IsNullOrEmpty(jsonData))
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}

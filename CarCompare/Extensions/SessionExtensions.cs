using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace CarCompare.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Serialize the object to JSON and store it using SetString.
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

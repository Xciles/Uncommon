using System.Collections.Generic;
using System.Reflection;

namespace Xciles.Uncommon.Extensions
{
    public static class ObjectExtensions
    {
        public static List<KeyValuePair<string, string>> GetProperties(this object me)
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (var property in me.GetType().GetRuntimeProperties())
            {
                result.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(me).ToString()));
            }
            return result;
        }
    }
}

using System;
using System.Linq;
using System.Text;

namespace WPDevToolkit
{
    public static class Extensions
    {
        public static string XxHash(this string s)
        {
            var hash = new XxHash();
            var input = Encoding.UTF8.GetBytes(s);
            hash.Init();
            hash.Update(input, input.Count());
            return hash.Digest().ToString();
        }

        public static T CastToGeneric<T>(this object obj)
        {
            if (obj is T)
            {
                return (T)obj;
            }
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}

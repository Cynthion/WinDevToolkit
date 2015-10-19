using System.Collections.Concurrent;
using System.Collections.Generic;
using WPDevToolkit.Interfaces;

namespace WPDevToolkit
{
    public static class SingletonLocator
    {
        private static readonly IDictionary<string, ILocatable> Dictionary = new ConcurrentDictionary<string, ILocatable>();

        /*public static void Set<T>(T viewModel)
        {
            var type = typeof(T);
            Dictionary.Add(type.FullName, viewModel);
        }*/

        public static T Get<T>() where T : ILocatable, new()
        {
            var type = typeof(T);
            if (!Dictionary.ContainsKey(type.FullName))
            {
                var t = new T();
                Dictionary.Add(t.GetType().FullName, t);
            }
            return (T)Dictionary[type.FullName];
        }
    }
}

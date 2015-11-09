using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WPDevToolkit
{
    public static class Extensions
    {
        // TODO unit test
        /// <summary>
        /// Applies a sort on the original <see cref="ObservableCollection{T}"/> itself.
        /// </summary>
        /// <typeparam name="TSource">The type to be sorted on.</typeparam>
        /// <typeparam name="TKey">The key on which to sort on the type.</typeparam>
        /// <param name="oc"></param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">A <see cref="IComparer{T}"/> to compare keys.</param>
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> oc, Func<TSource, TKey> keySelector, IComparer<TKey> comparer = null) where TSource : IEquatable<TSource>
        {
            // partially inspired by results from http://stackoverflow.com/questions/1945461/how-do-i-sort-an-observable-collection

            // use LINQ to establish the sort order
            List<TSource> sorted;
            if (comparer != null)
            {
                sorted = oc.OrderBy(keySelector, comparer).ToList();
            }
            else
            {
                sorted = oc.OrderBy( keySelector).ToList();
            }

            // move the elements in the ObservableCollection to match the sort order
            var i = 0;
            while (i < sorted.Count)
            {
                // minimize changes to collection
                if (!oc[i].Equals(sorted[i]))
                {
                    // use binary search (log(n)) instead of IndexOf (n)
                    var newIndex = sorted.BinarySearch(oc[i]);
                    
                    // use move for single collection changed notification
                    oc.Move(i, newIndex);
                }
                else
                {
                    i++;
                }
            }
        }

        public static string XxHash(this string s)
        {
            var hash = new XxHash();
            var input = Encoding.UTF8.GetBytes(s);
            hash.Init();
            hash.Update(input, input.Count());
            return hash.Digest().ToString();
        }

        // TODO remove this
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

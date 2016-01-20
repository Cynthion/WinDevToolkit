using System.Collections.Generic;

namespace WinDevToolkit.Sorting
{
    /// <summary>
    /// Combines multiple comparers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiComparer<T> : IComparer<T>
    {
        public IList<IComparer<T>> Comparers { get; set; }

        private readonly IList<IComparer<T>> _comparers;

        public MultiComparer()
            : this(new IComparer<T>[0])
        { }

        public MultiComparer(IList<IComparer<T>> comparers)
        {
            _comparers = comparers;
        }

        /// <summary>
        /// Compares based on the comparer defined in the <see cref="Comparers"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            int res;
            var i = -1;
            do
            {
                i++;
                res = _comparers[i].Compare(x, y);

            } while (res == 0 && i < _comparers.Count);
            return res;
        }
    }
}

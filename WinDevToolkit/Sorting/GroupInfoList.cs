using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace WinDevToolkit.Sorting
{
    /// <summary>
    /// Used as <see cref="CollectionViewSource"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GroupInfoList<T> : List<T>
    {
        public object Key { get; set; }
    }
}

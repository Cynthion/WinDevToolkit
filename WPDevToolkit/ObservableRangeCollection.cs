using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace WPDevToolkit
{
    // from http://blogs.msdn.com/b/nathannesbit/archive/2009/04/20/addrange-and-observablecollection.aspx
    /// <summary> 
    /// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed. 
    /// </summary> 
    /// <typeparam name="T"></typeparam>
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (!collection.Any()) return;
            
            CheckReentrancy();
            var startingIndex = this.Count;
            foreach (var item in collection)
            {
                this.Items.Add(item);
            }
            //this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            //this.OnPropertyChanged(new PropertyChangedEventArgs("Items"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, startingIndex));
        }

        ///// <summary> 
        ///// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). 
        ///// </summary> 
        //public void RemoveRange(IEnumerable<T> collection)
        //{
        //    if (collection == null) throw new ArgumentNullException("collection");

        //    foreach (var i in collection)
        //    {
        //        Items.Remove(i);
        //    }
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
        //}

        ///// <summary> 
        ///// Clears the current collection and replaces it with the specified item. 
        ///// </summary> 
        //public void Replace(T item)
        //{
        //    ReplaceRange(new T[] { item });
        //}

        ///// <summary> 
        ///// Clears the current collection and replaces it with the specified collection. 
        ///// </summary> 
        //public void ReplaceRange(IEnumerable<T> collection)
        //{
        //    if (collection == null) throw new ArgumentNullException("collection");

        //    Items.Clear();
        //    foreach (var i in collection)
        //    {
        //        Items.Add(i);
        //    }
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //}

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class. 
        /// </summary> 
        public ObservableRangeCollection()
            : base() { }

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection. 
        /// </summary> 
        /// <param name="collection">collection: The collection from which the elements are copied.</param> 
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception> 
        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection) { }
    }
}

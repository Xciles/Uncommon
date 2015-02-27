using System.Collections.Generic;
using System.Collections.Specialized;

namespace Xciles.Uncommon.Collections
{
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}

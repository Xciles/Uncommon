using System.ComponentModel;

namespace Xciles.Uncommon.Collections
{
    public interface ISortedObservableCollection<T> : IObservableCollection<T>, INotifyPropertyChanged
    {
        void InsertItem(T item);
    }
}
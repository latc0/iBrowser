using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace iphone
{
    public class Item : ObservableCollection<FileOrFolder>
    {
        public Item(NotifyCollectionChangedEventHandler ne)
            : base()
        {
            this.CollectionChanged += ne;
        }
    }
}

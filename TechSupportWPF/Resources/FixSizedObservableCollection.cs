using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchProject.Resources
{
    public class FixSizedObservableCollection<T> : ObservableCollection<T>
    {
        private readonly int _maxSize;
        public FixSizedObservableCollection(int maxSize)
        {
            _maxSize = maxSize;
        }
        public void Put(T item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if(Count==_maxSize)
                    this.RemoveAt(_maxSize-1);
                this.Insert(0, item);
            });

        }
    }
}

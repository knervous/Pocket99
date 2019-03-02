using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Noesis
{

    public class FreezableCollection<T> : BaseFreezableCollection, IList<T>, INotifyCollectionChanged where T: DependencyObject
    {
        protected FreezableCollection()
        {
        }

        internal FreezableCollection(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
        }

        internal static HandleRef getCPtr(FreezableCollection<T> obj)
        {
            return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        public new T this[int index]
        {
            get { return (T)base[index]; }
            set
            {
                CheckReentrancy();

                T oldValue = (T)base[index];
                if (oldValue != value)
                {
                    base[index] = value;

                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace, value, oldValue, index));
                }
            }
        }

        int ICollection<T>.Count
        {
            get { return base.Count; }
        }

        public bool IsReadOnly
        {
            get { return base.IsFrozen; }
        }

        public void Add(T item)
        {
            CheckReentrancy();

            base.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, Count - 1));
        }

        public new void Clear()
        {
            CheckReentrancy();

            base.Clear();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return base.Contains(item);
        }

        public int IndexOf(T item)
        {
            return base.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            CheckReentrancy();

            base.Insert(index, item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index));
        }

        public bool Remove(T item)
        {
            int index = base.IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public new void RemoveAt(int index)
        {
            CheckReentrancy();

            T oldValue = this[index];
            base.RemoveAt(index);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, oldValue, index));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection)this).CopyTo(array, arrayIndex);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                using (BlockReentrancy())
                {
                    CollectionChanged(this, e);
                }
            }
        }

        #region Reentrancy checks
        private IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        private void CheckReentrancy()
        {
            if (_monitor.Busy)
            {
                throw new InvalidOperationException("FreezableCollection reentrant operation");
            }
        }

        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;

            public bool Busy
            {
                get { return this._busyCount > 0; }
            }

            public void Enter()
            {
                _busyCount++;
            }

            public void Dispose()
            {
                _busyCount--;
                GC.SuppressFinalize(this);
            }
        }

        private SimpleMonitor _monitor = new SimpleMonitor();
        #endregion

        #region Enumerator
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        private struct Enumerator : IEnumerator<T>
        {
            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public T Current
            {
                get { return this._collection[this._index]; }
            }

            public bool MoveNext()
            {
                if (++this._index >= this._collection.Count)
                {
                    return false;
                }
                return true;
            }
            public void Reset()
            {
                this._index = -1;
            }

            public void Dispose()
            {
            }

            public Enumerator(FreezableCollection<T> c)
            {
                this._collection = c;
                this._index = -1;
            }

            private FreezableCollection<T> _collection;
            private int _index;
        }
        #endregion
    }

}


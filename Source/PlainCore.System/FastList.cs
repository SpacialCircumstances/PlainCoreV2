using System;
using System.Collections;
using System.Collections.Generic;

namespace PlainCore.System
{
    /// <summary>
    /// A list that is fast for clearing because it does not delete the contained elements, instead overwrites them.
    /// This class is for internal use and does not implement all methods correctly. You probably don´t want to use this.
    /// </summary>
    /// <typeparam name="T">Type of the elements</typeparam>
    public class FastList<T> : IList<T>
    {
        public FastList(int initialCapacity)
        {
            internalList = new T[initialCapacity];
        }

        private T[] internalList;

        private int count;

        public int Capacity => internalList.Length;

        public T[] Buffer => internalList;

        public T this[int index] {
            get => internalList[index];
            set => throw new NotSupportedException();
        }

        public int Count => count;

        public bool IsReadOnly => false;

        protected void EnsureSize(int cap)
        {
            if (cap >= internalList.Length)
            {
                int newCapacity = internalList.Length + (internalList.Length / 2);
                Array.Resize(ref internalList, newCapacity);
            }
        }

        public void Add(T item)
        {
            internalList[count] = item;
            count++;
            EnsureSize(count);
        }

        public void Clear()
        {
            count = 0;
        }

        public bool Contains(T item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(internalList, arrayIndex, array, 0, internalList.Length - arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)internalList).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, T item)
        {
            internalList[index] = item;
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
    }
}

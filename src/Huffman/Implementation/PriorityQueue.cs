﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Huffman.Implementation
{
    public class PriorityQueue<T>
    {
        private readonly List<T> _items;

        private readonly PropertyInfo _priorityProperty;
        private readonly PropertyInfo _sortProperty;

        public int Length => _items.Count;

        public PriorityQueue()
        {
            _items = new List<T>();

            _priorityProperty = typeof(T).GetProperties().Single(p => Attribute.IsDefined(p, typeof(PriorityAttribute)));
            _sortProperty = typeof(T).GetProperties().Single(p => Attribute.IsDefined(p, typeof(SecondarySortAttribute)));
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public T PopMin()
        {
            // TODO: This works, but I don't like it. (int) should be a variant type.
            // Also, maybe have a secondary sort property for when priority values are equal.
            var items = _items.Where(i => (int) _priorityProperty.GetValue(i) == (int) _items.Min(x => _priorityProperty.GetValue(x)));

            var item = items.OrderBy(i => _sortProperty.GetValue(i)).First();

            _items.Remove(item);

            return item;
        }
    }
}
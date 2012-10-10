using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cadenza.Collections
{
    /// <summary>
    /// A mutable lookup implementing <see cref="ILookup{TKey,TElement}"/>
    /// </summary>
    /// <typeparam name="TKey">The lookup key.</typeparam>
    /// <typeparam name="TElement">The elements under each <typeparamref name="TKey"/>.</typeparam>
    internal class MutableLookup<TKey, TElement>
        : ILookup<TKey, TElement>
    {
        public MutableLookup()
            : this(EqualityComparer<TKey>.Default)
        {
        }

        public MutableLookup(IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            groupings = new Dictionary<TKey, MutableLookupGrouping>(comparer);
            if (!typeof (TKey).IsValueType)
                nullGrouping = new MutableLookupGrouping(default(TKey));
        }

        public MutableLookup(ILookup<TKey, TElement> lookup)
        {
            if (lookup == null)
                throw new ArgumentNullException("lookup");

            groupings = new Dictionary<TKey, MutableLookupGrouping>(lookup.Count);

            if (!typeof (TKey).IsValueType)
                nullGrouping = new MutableLookupGrouping(default(TKey), lookup[default(TKey)]);

            foreach (var grouping in lookup)
            {
                if (grouping.Key == null)
                    continue;

                groupings.Add(grouping.Key, new MutableLookupGrouping(grouping.Key, grouping));
            }
        }

        /// <summary>
        /// Adds <paramref name="element"/> under the specified <paramref name="key"/>. <paramref name="key"/> does not need to exist.
        /// </summary>
        /// <param name="key">The key to add <paramref name="element"/> under.</param>
        /// <param name="element">The element to add.</param>
        public void Add(TKey key, TElement element)
        {
            MutableLookupGrouping grouping;
            if (key == null)
                grouping = nullGrouping;
            else if (!groupings.TryGetValue(key, out grouping))
            {
                grouping = new MutableLookupGrouping(key);
                groupings.Add(key, grouping);
            }

            grouping.Add(element);
        }

        public void Add(TKey key, IEnumerable<TElement> elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            MutableLookupGrouping grouping;
            if (key == null)
                grouping = nullGrouping;
            else if (!groupings.TryGetValue(key, out grouping))
            {
                grouping = new MutableLookupGrouping(key);
                groupings.Add(key, grouping);
            }

            grouping.AddRange(elements);
        }

        /// <summary>
        /// Removes <paramref name="element"/> from the <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key that <paramref name="element"/> is located under.</param>
        /// <param name="element">The element to remove from <paramref name="key"/>. </param>
        /// <returns><c>true</c> if <paramref name="key"/> and <paramref name="element"/> existed, <c>false</c> if not.</returns>
        public bool Remove(TKey key, TElement element)
        {
            if (key == null)
                return nullGrouping.Remove(element);

            if (!groupings.ContainsKey(key))
                return false;

            if (groupings[key].Remove(element))
            {
                if (groupings[key].Count == 0)
                    groupings.Remove(key);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes <paramref name="key"/> from the lookup.
        /// </summary>
        /// <param name="key">They to remove.</param>
        /// <returns><c>true</c> if <paramref name="key"/> existed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                bool removed = (nullGrouping.Count > 0);
                nullGrouping.Clear();
                return removed;
            }

            return groupings.Remove(key);
        }

        public void Clear()
        {
            if (nullGrouping != null)
                nullGrouping.Clear();

            groupings.Clear();
        }

        public bool TryGetValues(TKey key, out IEnumerable<TElement> values)
        {
            values = null;

            if (key == null)
            {
                if (nullGrouping.Count != 0)
                {
                    values = nullGrouping;
                    return true;
                }
                else
                    return false;
            }

            MutableLookupGrouping grouping;
            if (!groupings.TryGetValue(key, out grouping))
                return false;

            values = grouping;
            return true;
        }

        #region ILookup Members

        /// <summary>
        /// Gets the number of groupings.
        /// </summary>
        public int Count
        {
            get { return groupings.Count + ((nullGrouping != null && nullGrouping.Count > 0) ? 1 : 0); }
        }

        /// <summary>
        /// Gets the elements for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to get the elements for.</param>
        /// <returns>The elements under <paramref name="key"/>.</returns>
        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                if (key == null)
                    return nullGrouping;

                MutableLookupGrouping grouping;
                if (groupings.TryGetValue(key, out grouping))
                    return grouping;

                return new TElement[0];
            }
        }

        /// <summary>
        /// Gets whether or not there's a grouping for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns><c>true</c> if <paramref name="key"/> is present.</returns>
        public bool Contains(TKey key)
        {
            if (key == null)
                return (nullGrouping.Count > 0);

            return groupings.ContainsKey(key);
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            foreach (var g in groupings.Values)
                yield return g;

            if (nullGrouping != null && nullGrouping.Count > 0)
                yield return nullGrouping;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private readonly Dictionary<TKey, MutableLookupGrouping> groupings;
        private readonly MutableLookupGrouping nullGrouping;

        private class MutableLookupGrouping : List<TElement>, IGrouping<TKey, TElement>
        {
            public MutableLookupGrouping(TKey key)
            {
                Key = key;
            }

            public MutableLookupGrouping(TKey key, IEnumerable<TElement> collection)
                : base(collection)
            {
                Key = key;
            }

            #region IGrouping<TKey,TElement> Members

            public TKey Key { get; private set; }

            #endregion
        }
    }
}
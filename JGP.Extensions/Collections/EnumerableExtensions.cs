// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-17-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-17-2022
// ***********************************************************************
// <copyright file="EnumerableExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Extensions.Collections
{
    /// <summary>
    ///     Class EnumerableExtensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Returns all elements in a sequence that follow the first occurrence of an element that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public static IEnumerable<T> AfterFirst<T>(this IEnumerable<T>? collection, Func<T, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return collection.SkipWhile(x => !predicate.Invoke(x)).Skip(1);
        }

        /// <summary>
        ///     Returns all elements in a sequence that precede the first occurrence of an element that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public static IEnumerable<T> BeforeFirst<T>(this IEnumerable<T>? collection, Func<T, bool> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return collection.TakeWhile(x => !predicate.Invoke(x));
        }

        /// <summary>
        ///     Returns different elements from a sequence using a specified <see cref="Func {TResult}" /> selector for comparing values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T>? collection, Func<T, TKey> keySelector)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var keys = new HashSet<TKey>();

            foreach (var element in collection)
            {
                var key = keySelector(element);

                var added = keys.Add(key);

                if (added) yield return element;
            }
        }

        /// <summary>
        ///     Invokes the specified action on each batch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <param name="batchAction">The batch action.</param>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        /// <exception cref="System.ArgumentNullException">batchAction</exception>
        public static void ForEachBatch<T>(this IEnumerable<T>? collection, int batchSize,
            Action<IEnumerable<T>> batchAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (batchAction == null) throw new ArgumentNullException(nameof(batchAction));

            var list = new List<T>(batchSize);

            foreach (var item in collection)
            {
                list.Add(item);

                if (list.Count < batchSize) continue;
                batchAction.Invoke(list.ToList());
                list.Clear();
            }

            if (list.Count > 0) batchAction.Invoke(list);
        }

        /// <summary>
        ///     Gets a set of random elements from the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="count">The count.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T>? collection, int count)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.OrderBy(x => Guid.NewGuid()).Take(count);
        }

        /// <summary>
        ///     Determines whether the specified collection is not null and has items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns><c>true</c> if the specified collection has items; otherwise, <c>false</c>.</returns>
        public static bool HasItems<T>(this IEnumerable<T>? collection)
        {
            return collection != null && collection.Any();
        }

        /// <summary>
        ///     Safely adds the specified item to the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="item">The item.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> SafeAdd<T>(this IEnumerable<T>? collection, T item)
        {
            collection ??= new List<T>();

            var list = collection.ToList();
            if (list.Contains(item)) return list;
            
            list.Add(item);
            return list;
        }

        /// <summary>
        ///     Converts to batches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="size">The size.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        public static IEnumerable<IEnumerable<T>> ToBatches<T>(this IEnumerable<T>? collection, int size)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            using var iterator = collection.GetEnumerator();
            while (iterator.MoveNext())
            {
                var batch = new T[size];
                var count = 1;
                batch[0] = iterator.Current;
                for (var i = 1; i < size && iterator.MoveNext(); i++)
                {
                    batch[i] = iterator.Current;
                    count++;
                }

                if (count < size) Array.Resize(ref batch, size);
                yield return batch;
            }
        }

        /// <summary>
        ///     Converts to groups.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="property">The property.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public static IEnumerable<IEnumerable<T>> ToGroups<T>(this IEnumerable<T> collection, string property)
        {
            var list = new List<IEnumerable<T>>();
            if (!collection.HasItems())
            {
                return list;
            }

            var groups = collection.GroupBy(x => x?.GetType()?.GetProperty(property)?.GetValue(x));
            list.AddRange(groups.Select(group => group.ToList()));
            return list;
        }
    }
}
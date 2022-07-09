// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-24-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-24-2022
// ***********************************************************************
// <copyright file="NameValueCollectionExtensions.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Specialized;

namespace JGP.Extensions.Collections;

/// <summary>
///     Class NameValueCollectionExtensions.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    ///     Tries the get value.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool TryGetValue(this NameValueCollection collection, string key, out string? value)
    {
        try
        {
            value = collection.Get(key);
            return true;
        }
        catch (Exception)
        {
            value = null;
            return false;
        }
    }

    /// <summary>
    ///     Converts to lookup.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>ILookup&lt;System.String, System.String&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">collection</exception>
    public static ILookup<string, string?> ToLookup(this NameValueCollection? collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        //var pairs = from key in collection.Cast<string>()
        //    from value in collection.GetValues(key)
        //    select new { key, value };

        //return pairs.ToLookup(pair => pair.key, pair => pair.value);

        return collection.ToPairs()
            .ToLookup(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    ///     Converts to pairs.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.Nullable&lt;System.String&gt;&gt;&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">collection</exception>
    public static IEnumerable<KeyValuePair<string, string?>> ToPairs(this NameValueCollection? collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        return collection.Cast<string>()
            .Select(key => new KeyValuePair<string, string?>(key, collection[key]));
    }
}
// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-21-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-21-2022
// ***********************************************************************
// <copyright file="DictionaryExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Extensions.Collections;

/// <summary>
///     Class DictionaryExtensions.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    ///     Adds if not contains.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>IDictionary&lt;TKey, TValue&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static IDictionary<TKey, TValue> AddIfNotContains<TKey, TValue>(
        this IDictionary<TKey, TValue>? dictionary, TKey key, TValue value, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        void AddIfNotContainsInternal()
        {
            if (!dictionary.ContainsKey(key)) dictionary[key] = value;
        }

        if (syncRoot != null)
        {
            lock (syncRoot)
            {
                AddIfNotContainsInternal();
            }
        }
        else
        {
            AddIfNotContainsInternal();
        }

        return dictionary;
    }

    /// <summary>
    ///     Updates an item or adds it if missing to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>IDictionary&lt;TKey, TValue&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static IDictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary,
        TKey key, TValue value, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        if (syncRoot != null)
        {
            lock (syncRoot)
            {
                dictionary[key] = value;
            }
        }
        else
        {
            dictionary[key] = value;
        }

        return dictionary;
    }

    /// <summary>
    ///     Finds the specified key.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>TValue?.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static TValue? Find<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key,
        object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        return dictionary.TryFind(key, out var value, syncRoot) ? value : default;
    }

    /// <summary>
    ///     Finds the specified key match function.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="keyMatchFunc">The key match function.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>TValue?.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static TValue? Find<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary,
        Func<TKey, bool> keyMatchFunc, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        return dictionary.TryFind(keyMatchFunc, out var value, syncRoot) ? value : default;
    }

    /// <summary>
    ///     Finds an item or adds it if missing to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="onGetValueForAdd">The on get value for add.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>TValue.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key,
        Func<TValue> onGetValueForAdd, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        return dictionary.FindOrAdd(key, onGetValueForAdd, null, syncRoot);
    }

    /// <summary>
    ///     Finds an item or adds it if missing to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="onGetValueForAdd">The on get value for add.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>TValue.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key,
        Func<TKey, TValue> onGetValueForAdd, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        return dictionary.FindOrAdd(key, () => onGetValueForAdd.Invoke(key), null, syncRoot);
    }

    /// <summary>
    ///     Finds an item or adds it if missing to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="onGetValueForAdd">The on get value for add.</param>
    /// <param name="onIsEqual">The on is equal.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns>TValue.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key,
        Func<TValue> onGetValueForAdd, Func<TValue, bool>? onIsEqual, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        var result = dictionary.Find(key, syncRoot);

        if (onIsEqual?.Invoke(result) ?? Equals(result, default))
        {
            result = onGetValueForAdd.Invoke();
            dictionary.AddOrUpdate(key, result, syncRoot);
        }

        return result;
    }

    /// <summary>
    ///     Gets the boolean value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool? GetBooleanValue(this IDictionary<string, string> dictionary, string key)
    {
        var stringValue = dictionary.GetStringValue(key);
        return bool.TryParse(stringValue, out var boolValue) ? boolValue : null;
    }

    /// <summary>
    ///     Gets the date time value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
    public static DateTime? GetDateTimeValue(this IDictionary<string, string> dictionary, string key)
    {
        var stringValue = dictionary.GetStringValue(key);
        return DateTime.TryParse(stringValue, out var dateTimeValue) ? dateTimeValue : null;
    }

    /// <summary>
    ///     Gets the integer value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
    public static int? GetIntegerValue(this IDictionary<string, string> dictionary, string key)
    {
        var stringValue = dictionary.GetStringValue(key);
        return int.TryParse(stringValue, out var intValue) ? intValue : null;
    }

    /// <summary>
    ///     Gets the string value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>System.Nullable&lt;System.String&gt;.</returns>
    public static string? GetStringValue(this IDictionary<string, string> dictionary, string key,
        string? defaultValue = null)
    {
        return dictionary.GetValue(key, defaultValue);
    }

    /// <summary>
    ///     Gets the value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>System.Nullable&lt;T&gt;.</returns>
    public static T? GetValue<T>(this IDictionary<T, T> dictionary, T key, T? defaultValue)
    {
        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }

    /// <summary>
    ///     Tries the find.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static bool TryFind<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key, out TValue? value,
        object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        bool result;
        TValue valueForKey;

        if (syncRoot != null)
        {
            lock (syncRoot)
            {
                result = dictionary.TryGetValue(key, out valueForKey);
            }
        }
        else
        {
            result = dictionary.TryGetValue(key, out valueForKey);
        }

        value = valueForKey;

        return result;
    }


    /// <summary>
    ///     Tries to find an item in the dictionary that matches the supplied function.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="keyMatchFunc">The key match function.</param>
    /// <param name="value">The value.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns><c>true</c> if found, <c>false</c> otherwise.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static bool TryFind<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary,
        Func<TKey, bool> keyMatchFunc, out TValue value, object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        bool result;
        TValue valueForKey = default;

        bool FindInternal()
        {
            var key = dictionary.Keys.FirstOrDefault(keyMatchFunc);
            return key != null && dictionary.TryGetValue(key, out valueForKey);
        }

        if (syncRoot != null)
            lock (syncRoot)
            {
                result = FindInternal();
            }
        else
            result = FindInternal();

        value = valueForKey;

        return result;
    }

    /// <summary>
    ///     Tries to remove an item from the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="syncRoot">The synchronize root.</param>
    /// <returns><c>true</c> if item is removed, <c>false</c> otherwise.</returns>
    /// <exception cref="System.ArgumentNullException">dictionary</exception>
    public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue>? dictionary, TKey key,
        object? syncRoot = null)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        bool result;

        if (syncRoot != null)
            lock (syncRoot)
            {
                result = dictionary.Remove(key);
            }
        else
            result = dictionary.Remove(key);

        return result;
    }
}
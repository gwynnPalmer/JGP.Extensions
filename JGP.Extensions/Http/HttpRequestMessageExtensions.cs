// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-24-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-24-2022
// ***********************************************************************
// <copyright file="HttpRequestMessageExtensions.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Web;
using JGP.Extensions.Collections;

namespace JGP.Extensions.Http;

/// <summary>
///     Class HttpRequestMessageExtensions.
/// </summary>
public static class HttpRequestMessageExtensions
{
    /// <summary>
    ///     Gets the query string bool.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="key">The key.</param>
    /// <returns>System.Nullable&lt;bool&gt;.</returns>
    public static bool? GetQueryStringBool(this HttpRequestMessage request, string key)
    {
        var stringValue = request.GetQueryStringValue(key);
        return bool.TryParse(stringValue, out var boolValue) ? boolValue : null;
    }

    /// <summary>
    ///     Gets the query string date time.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="key">The key.</param>
    /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
    public static DateTime? GetQueryStringDateTime(this HttpRequestMessage request, string key)
    {
        var stringValue = request.GetQueryStringValue(key);
        return DateTime.TryParse(stringValue, out var dateValue) ? dateValue : null;
    }

    /// <summary>
    ///     Gets the query string int.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="key">The key.</param>
    /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
    public static int? GetQueryStringInt(this HttpRequestMessage request, string key)
    {
        var stringValue = request.GetQueryStringValue(key);
        return int.TryParse(stringValue, out var intValue) ? intValue : null;
    }

    /// <summary>
    ///     Gets the query string value.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>System.Nullable&lt;System.String&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">request</exception>
    public static string? GetQueryStringValue(this HttpRequestMessage request, string key, string? defaultValue = null)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var requestUri = request.RequestUri;
        if (!requestUri.HasValue()) return defaultValue;

        var collection = HttpUtility.ParseQueryString(requestUri!.Query);
        return collection.ToPairs()
            .FirstOrDefault(kvp => string.Compare(kvp.Key, key, StringComparison.OrdinalIgnoreCase) == 0)
            .Value ?? defaultValue;
    }
}
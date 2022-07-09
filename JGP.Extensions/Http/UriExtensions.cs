// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-24-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-24-2022
// ***********************************************************************
// <copyright file="UriExtensions.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

using JGP.Extensions.Strings;

namespace JGP.Extensions.Http;

/// <summary>
///     Class UriExtensions.
/// </summary>
public static class UriExtensions
{
    /// <summary>
    ///     Determines whether the specified URI has value.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns><c>true</c> if the specified URI has value; otherwise, <c>false</c>.</returns>
    public static bool HasValue(this Uri? uri)
    {
        return uri != null && !uri.Query.IsNullOrEmpty();
    }
}
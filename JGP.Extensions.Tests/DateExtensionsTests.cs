// ***********************************************************************
// Assembly         : JGP.Packages.Tests
// Author           : Joshua Gwynn-Palmer
// Created          : 06-26-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 07-07-2022
// ***********************************************************************
// <copyright file="DateExtensionsTests.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using JGP.Extensions.Dates;
using NUnit.Framework;

namespace JGP.Extensions.Tests;

/// <summary>
///     Class DateExtensionsTests.
/// </summary>
public class DateExtensionsTests
{
    #region DateTimeExtensions

    /// <summary>
    ///     Defines the test method AddWeeks.
    /// </summary>
    [Test]
    public void AddWeeks()
    {
        var dateTime = DateTime.UtcNow;
        var test = dateTime.AddWeeks(1);
        test.Should().Be(7.Days().After(dateTime));
    }

    /// <summary>
    ///     Defines the test method EndOfDay.
    /// </summary>
    [Test]
    public void EndOfDay()
    {
        var dateTime = new DateTime(2020, 1, 1, 1, 1, 1);
        var endOfDay = dateTime.EndOfDay();
        endOfDay.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfDay_ValueNull.
    /// </summary>
    [Test]
    public void EndOfDay_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.EndOfDay().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method EndOfMonth.
    /// </summary>
    [Test]
    public void EndOfMonth()
    {
        var dateTime = new DateTime(2020, 1, 1, 1, 1, 1);
        var endOfMonth = dateTime.EndOfMonth();
        endOfMonth.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("31-01-2020 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfMonth_ValueNull.
    /// </summary>
    [Test]
    public void EndOfMonth_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.EndOfMonth().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method EndOfWeek.
    /// </summary>
    [Test]
    public void EndOfWeek()
    {
        var dateTime = new DateTime(2022, 7, 4, 1, 1, 1);
        var endOfWeek = dateTime.EndOfWeek(DayOfWeek.Monday);
        endOfWeek.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("10-07-2022 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfWeek_ValueNull.
    /// </summary>
    [Test]
    public void EndOfWeek_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.EndOfWeek(DayOfWeek.Monday).Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method GenerateRandom.
    /// </summary>
    [Test]
    public void GenerateRandom()
    {
        var from = new DateTime(2020, 1, 1);
        var to = new DateTime(2020, 1, 31);
        var random = DateTimeExtensions.GenerateRandom(from, to);

        using (new AssertionScope())
        {
            random.Should().BeOnOrAfter(from);
            random.Should().BeOnOrBefore(to);
        }
    }

    /// <summary>
    ///     Defines the test method StartOfDay.
    /// </summary>
    [Test]
    public void StartOfDay()
    {
        var dateTime = new DateTime(2020, 1, 1, 1, 1, 1);
        var startOfDay = dateTime.StartOfDay();
        startOfDay.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfDay_ValueNull.
    /// </summary>
    [Test]
    public void StartOfDay_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.StartOfDay().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method StartOfMonth.
    /// </summary>
    [Test]
    public void StartOfMonth()
    {
        var dateTime = new DateTime(2020, 1, 12, 1, 1, 1);
        var startOfMonth = dateTime.StartOfMonth();
        startOfMonth.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfMonth_ValueNull.
    /// </summary>
    [Test]
    public void StartOfMonth_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.StartOfMonth().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method StartOfWeek.
    /// </summary>
    [Test]
    public void StartOfWeek()
    {
        var dateTime = new DateTime(2022, 7, 7, 1, 1, 1);
        var startOfWeek = dateTime.StartOfWeek(DayOfWeek.Monday);
        startOfWeek.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("04-07-2022 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfWeek_ValueNull.
    /// </summary>
    [Test]
    public void StartOfWeek_ValueNull()
    {
        DateTime? dateTime = null;
        dateTime.StartOfWeek(DayOfWeek.Monday).Should().BeNull();
    }
    #endregion

    #region DateTimeOffsetExtensions

    /// <summary>
    ///     Defines the test method AddWeeks_DateTimeOffset.
    /// </summary>
    [Test]
    public void AddWeeks_DateTimeOffset()
    {
        var random = new Random();
        var count = Convert.ToDouble(random.Next(1, 52));
        var offset = DateTimeOffset.UtcNow;
        var test = offset.AddWeeks(count);
        test.Should().BeExactly((count * 7).Days()).After(offset);
    }

    /// <summary>
    ///     Defines the test method EndOfDay_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfDay_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
        var endOfDay = offset.EndOfDay();
        endOfDay.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfDay_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfDay_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.EndOfDay().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method EndOfMonth_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfMonth_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
        var endOfMonth = offset.EndOfMonth();
        endOfMonth.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("31-01-2020 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfMonth_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfMonth_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.EndOfMonth().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method EndOfWeek_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfWeek_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2022, 7, 4, 1, 1, 1, TimeSpan.Zero);
        var endOfWeek = offset.EndOfWeek(DayOfWeek.Monday);
        endOfWeek.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("10-07-2022 23:59:59.999");
    }

    /// <summary>
    ///     Defines the test method EndOfWeek_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void EndOfWeek_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.EndOfWeek(DayOfWeek.Monday).Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method GenerateRandom_DateTimeOffset.
    /// </summary>
    [Test]
    public void GenerateRandom_DateTimeOffset()
    {
        var from = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
        var to = new DateTimeOffset(2020, 1, 31, 1, 1, 1, TimeSpan.Zero);
        var dateTime = DateTimeOffsetExtensions.GenerateRandom(from, to);
        using (new AssertionScope())
        {
            dateTime.Should().BeOnOrAfter(from);
            dateTime.Should().BeOnOrBefore(to);
        }
    }

    /// <summary>
    ///     Defines the test method StartOfDay_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfDay_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);
        var startOfDay = offset.StartOfDay();
        startOfDay.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfDay_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfDay_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.StartOfDay().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method StartOfMonth_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfMonth_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2020, 1, 12, 1, 1, 1, TimeSpan.Zero);
        var startOfMonth = offset.StartOfMonth();
        startOfMonth.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("01-01-2020 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfMonth_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfMonth_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.StartOfMonth().Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method StartOfWeek_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfWeek_DateTimeOffset()
    {
        var offset = new DateTimeOffset(2022, 7, 7, 1, 1, 1, TimeSpan.Zero);
        var startOfWeek = offset.StartOfWeek(DayOfWeek.Monday);
        startOfWeek.ToString("dd-MM-yyyy HH:mm:ss.fff").Should().Be("04-07-2022 00:00:00.000");
    }

    /// <summary>
    ///     Defines the test method StartOfWeek_ValueNull_DateTimeOffset.
    /// </summary>
    [Test]
    public void StartOfWeek_ValueNull_DateTimeOffset()
    {
        DateTimeOffset? offset = null;
        offset.StartOfWeek(DayOfWeek.Monday).Should().BeNull();
    }

    #endregion
}
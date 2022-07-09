// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-17-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-17-2022
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Extensions.Dates
{
    /// <summary>
    ///     Class DateTimeExtensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     The random
        /// </summary>
        private static readonly Random Rnd = new();

        /// <summary>
        ///     Adds weeks to the specific date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="value">The value.</param>
        /// <returns>DateTime.</returns>
        public static DateTime AddWeeks(this DateTime dateTime, double value)
        {
            return dateTime.SafeAdd(TimeSpan.FromDays(value * 7));
        }

        /// <summary>
        ///     Gets the ceiling end of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.StartOfDay().AddDays(1).Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? EndOfDay(this DateTime? dateTime)
        {
            return dateTime.HasValue ? EndOfDay(dateTime.Value) : null;
        }

        /// <summary>
        ///     Gets the ceiling end of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            var value = dateTime.StartOfMonth().AddMonths(1);
            return value == DateTime.MaxValue ? value : value.Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? EndOfMonth(this DateTime? dateTime)
        {
            return dateTime.HasValue ? EndOfMonth(dateTime.Value) : null;
        }

        /// <summary>
        ///     Gets the ceiling end of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns>DateTime.</returns>
        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            var value = dateTime.StartOfWeek(startOfWeek).AddWeeks(1);
            return value == DateTime.MaxValue ? value : value.Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? EndOfWeek(this DateTime? dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            return dateTime.HasValue ? EndOfWeek(dateTime.Value, startOfWeek) : null;
        }

        /// <summary>
        ///     Generates a random date time between 2 datetime values.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GenerateRandom(DateTime from, DateTime to)
        {
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(Rnd.NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }

        /// <summary>
        ///     Gets the floored start of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Gets the floored start of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? StartOfDay(this DateTime? dateTime)
        {
            return dateTime.HasValue ? StartOfDay(dateTime.Value) : null;
        }
        /// <summary>
        ///     Gets the floored start of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return dateTime.StartOfDay().SafeSubtract(TimeSpan.FromDays(dateTime.Date.Day - 1));
        }

        /// <summary>
        ///     Gets the floored start of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? StartOfMonth(this DateTime? dateTime)
        {
            return dateTime.HasValue ? StartOfMonth(dateTime.Value) : null;
        }

        /// <summary>
        ///     Gets the floored start of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns>DateTime.</returns>
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            var diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0)
                diff += 7;

            return dateTime.StartOfDay().SafeSubtract(TimeSpan.FromDays(diff));
        }

        /// <summary>
        ///     Starts the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? StartOfWeek(this DateTime? dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            return dateTime.HasValue ? StartOfWeek(dateTime.Value, startOfWeek) : null;
        }
        /// <summary>
        ///     Safes the add.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="value">The value.</param>
        /// <returns>DateTime.</returns>
        private static DateTime SafeAdd(this DateTime dateTime, TimeSpan value)
        {
            if (dateTime.Ticks + value.Ticks < DateTime.MinValue.Ticks)
                return DateTime.MinValue;

            return dateTime.Ticks + value.Ticks > DateTime.MaxValue.Ticks ? DateTime.MaxValue : dateTime.Add(value);
        }

        /// <summary>
        ///     Safes the subtract.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="value">The value.</param>
        /// <returns>DateTime.</returns>
        private static DateTime SafeSubtract(this DateTime dateTime, TimeSpan value)
        {
            if (dateTime.Ticks - value.Ticks < DateTime.MinValue.Ticks)
                return DateTime.MinValue;

            return dateTime.Ticks - value.Ticks > DateTime.MaxValue.Ticks
                ? DateTime.MaxValue
                : dateTime.Subtract(value);
        }
    }
}
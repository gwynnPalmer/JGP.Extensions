// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-17-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-17-2022
// ***********************************************************************
// <copyright file="DateTimeOffsetExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Extensions.Dates
{
    /// <summary>
    ///     Class DateTimeOffsetExtensions.
    /// </summary>
    public static class DateTimeOffsetExtensions
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
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset AddWeeks(this DateTimeOffset dateTime, double value)
        {
            return dateTime.SafeAdd(TimeSpan.FromDays(value * 7));
        }

        /// <summary>
        ///     Gets the ceiling end of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset EndOfDay(this DateTimeOffset dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.StartOfDay(ignoreUtcOffset).AddDays(1).Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? EndOfDay(this DateTimeOffset? dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? EndOfDay(dateTime.Value, ignoreUtcOffset) : null;
        }

        /// <summary>
        ///     Gets the ceiling end of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset EndOfMonth(this DateTimeOffset dateTime, bool ignoreUtcOffset = false)
        {
            var value = dateTime.StartOfMonth(ignoreUtcOffset).AddMonths(1);
            return value == DateTimeOffset.MaxValue ? value : value.Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> [ignore UTC offset].</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? EndOfMonth(this DateTimeOffset? dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? EndOfMonth(dateTime.Value, ignoreUtcOffset) : null;
        }

        /// <summary>
        ///     Gets the ceiling end of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset EndOfWeek(this DateTimeOffset dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday,
            bool ignoreUtcOffset = false)
        {
            var value = dateTime.StartOfWeek(startOfWeek, ignoreUtcOffset).AddWeeks(1);
            return value == DateTimeOffset.MaxValue ? value : value.Subtract(TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        ///     Gets the ceiling end of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> [ignore UTC offset].</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? EndOfWeek(this DateTimeOffset? dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday,
            bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? EndOfWeek(dateTime.Value, startOfWeek, ignoreUtcOffset) : null;
        }

        /// <summary>
        ///     Generates a random date time offset between the start and end date.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset GenerateRandom(DateTimeOffset from, DateTimeOffset to)
        {
            var range = to - from;
            var randTimeSpan = new TimeSpan((long)(Rnd.NextDouble() * range.Ticks));
            return from + randTimeSpan;
        }

        /// <summary>
        ///     Gets the floored start of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset StartOfDay(this DateTimeOffset dateTime, bool ignoreUtcOffset = false)
        {
            var offset = ignoreUtcOffset ? TimeSpan.Zero : dateTime.Offset;
            return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0, offset);
        }

        /// <summary>
        ///     Gets the floored start of the day.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? StartOfDay(this DateTimeOffset? dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? StartOfDay(dateTime.Value, ignoreUtcOffset) : null;
        }
        /// <summary>
        ///     Gets the floored start of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset StartOfMonth(this DateTimeOffset dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.StartOfDay(ignoreUtcOffset).SafeSubtract(TimeSpan.FromDays(dateTime.Date.Day - 1));
        }

        /// <summary>
        ///     Gets the floored start of the month.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> [ignore UTC offset].</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? StartOfMonth(this DateTimeOffset? dateTime, bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? StartOfMonth(dateTime.Value, ignoreUtcOffset) : null;
        }

        /// <summary>
        ///     Gets the floored start of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> Any offsets are ignored.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset StartOfWeek(this DateTimeOffset dateTime, DayOfWeek startOfWeek = DayOfWeek.Sunday,
            bool ignoreUtcOffset = false)
        {
            var diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0)
                diff += 7;

            return dateTime.StartOfDay(ignoreUtcOffset).SafeSubtract(TimeSpan.FromDays(diff));
        }

        /// <summary>
        ///     Gets the floored start of the of week.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <param name="ignoreUtcOffset">if set to <c>true</c> [ignore UTC offset].</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? StartOfWeek(this DateTimeOffset? dateTime,
            DayOfWeek startOfWeek = DayOfWeek.Sunday, bool ignoreUtcOffset = false)
        {
            return dateTime.HasValue ? StartOfWeek(dateTime.Value, startOfWeek, ignoreUtcOffset) : null;
        }
        /// <summary>
        ///     Safes the add.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="value">The value.</param>
        /// <returns>DateTimeOffset.</returns>
        private static DateTimeOffset SafeAdd(this DateTimeOffset dateTime, TimeSpan value)
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
        /// <returns>DateTimeOffset.</returns>
        private static DateTimeOffset SafeSubtract(this DateTimeOffset dateTime, TimeSpan value)
        {
            if (dateTime.Ticks - value.Ticks < DateTime.MinValue.Ticks)
                return DateTime.MinValue;

            return dateTime.Ticks - value.Ticks > DateTime.MaxValue.Ticks
                ? DateTime.MaxValue
                : dateTime.Subtract(value);
        }
    }
}
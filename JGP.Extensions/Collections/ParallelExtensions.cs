// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-17-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-17-2022
// ***********************************************************************
// <copyright file="ParallelExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace JGP.Extensions.Collections
{
    using System.Threading.Tasks.Dataflow;

    /// <summary>
    ///     Class ParallelExtension.
    /// </summary>
    public static class ParallelExtension
    {
        /// <summary>
        ///     Parallelizes asynchronous executions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="body">The body.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
        /// <param name="scheduler">The scheduler.</param>
        /// <returns>Task.</returns>
        public static Task AsyncParallelForEach<T>(this IEnumerable<T> source, Func<T, Task> body,
            int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler? scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            if (scheduler != null)
            {
                options.TaskScheduler = scheduler;
            }

            var block = new ActionBlock<T>(body, options);
            foreach (var item in source) block.Post(item);
            block.Complete();
            return block.Completion;
        }

        /// <summary>
        ///     Parallelizes asynchronous executions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="body">The body.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
        /// <param name="scheduler">The scheduler.</param>
        public static async Task AsyncParallelForEach<T>(this IAsyncEnumerable<T> source, Func<T, Task> body,
            int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler? scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            if (scheduler != null)
            {
                options.TaskScheduler = scheduler;
            }

            var block = new ActionBlock<T>(body, options);
            await foreach (var item in source) block.Post(item);
            block.Complete();
            await block.Completion;
        }
    }
}
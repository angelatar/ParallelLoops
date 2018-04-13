using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLoops
{
    /// <summary>
    /// Provides support for parallel loop
    /// </summary>
    public static class ParallelLoop
    {

        /// <summary>
        /// Executes a for loop in which iterations may run in parallel
        /// </summary>
        /// <param name="fromInclusive"> The start index</param>
        /// <param name="toExclusive"> The end index</param>
        /// <param name="body"> The delegate that is invoked once per iteration</param>
        /// <returns></returns>
        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body)
        {
            if(toExclusive<= fromInclusive || body==null)
            {
                return;
            }
            Task[] tArr = new Task[toExclusive - fromInclusive];
            int j = 0;
            for (int i = fromInclusive; i < toExclusive; i++)
            {
                var param = i;
                tArr[j++] = new Task(() => body(param));
                tArr[j-1].Start();
            }
            Task.WaitAll(tArr);
        }

        /// <summary>
        /// Executes a for each operation in which iterations may run in parallel
        /// </summary>
        /// <typeparam name="TSource"> The type of the data in the source</typeparam>
        /// <param name="source"> An enumerable data source</param>
        /// <param name="body"> The delegate that is invoked once per iteration</param>
        public static void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {
            if (source==null || body == null)
            {
                return;
            }
            Task[] tArr = new Task[source.Count()];
            int j = 0;
            foreach (var item in source)
            {
                var param = item;
                tArr[j]=new Task(() => body(param));
                tArr[j++].Start();
            }
            Task.WaitAll(tArr);
        }

        /// <summary>
        /// Executes a for each operation in which iterations may run in parallel
        /// </summary>
        /// <typeparam name="TSource"> The type of the data in the source</typeparam>
        /// <param name="source"> An enumerable data source</param>
        /// <param name="parallelOptions"> An instance that configures the behavior of this operation</param>
        /// <param name="body"> The delegate that is invoked once per iteration</param>
        public static void ParallelForEachWithOptions<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
        {
            if (source == null || body == null)
            {
                return;
            }
            Task[] tArr = new Task[parallelOptions.MaxDegreeOfParallelism];
            int j = 0;
            foreach (var item in source)
            {
                var param = item;
                tArr[j] = new Task(() => body(param));
                tArr[j++].Start();
                if(j== parallelOptions.MaxDegreeOfParallelism)
                {
                    j = 0;
                    Task.WaitAll(tArr);
                }
            }
        }
    }
}

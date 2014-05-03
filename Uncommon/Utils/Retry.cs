using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xciles.Common.Utils
{
    public static class Retry
    {
        public static async Task DoAsync(Func<Task> action, TimeSpan retryInterval, int retryCount = 3)
        {
            await DoAsync(action, retryInterval, CancellationToken.None, retryCount).ConfigureAwait(false);
        }

        public static async Task DoAsync(Func<Task> action, Func<Task> retryWhen, int retryCount = 3)
        {
            await DoAsync(action, retryWhen, CancellationToken.None, retryCount).ConfigureAwait(false);
        }

        public static async Task DoAsync(Func<Task> action, TimeSpan retryInterval, CancellationToken ct, int retryCount = 3)
        {
            await DoAsync<object>(() =>
            {
                action();
                return null;
            }, () => TaskEx.Delay(retryInterval, ct), ct, retryCount).ConfigureAwait(false);
        }

        public static async Task DoAsync(Func<Task> action, Func<Task> retryWhen, CancellationToken ct, int retryCount = 3)
        {
            await DoAsync<object>(() =>
            {
                action();
                return null;
            }, retryWhen, ct, retryCount).ConfigureAwait(false);
        }

        public static async Task<T> DoAsync<T>(Func<Task<T>> action, TimeSpan retryInterval, int retryCount = 3)
        {
            return await DoAsync(action, () => TaskEx.Delay(retryInterval), CancellationToken.None, retryCount).ConfigureAwait(false);
        }

        public static async Task<T> DoAsync<T>(Func<Task<T>> action, TimeSpan retryInterval, CancellationToken ct, int retryCount = 3)
        {
            return await DoAsync(action, () => TaskEx.Delay(retryInterval, ct), ct, retryCount).ConfigureAwait(false);
        }

        public static async Task<T> DoAsync<T>(Func<Task<T>> action, Func<Task> retryWhen, int retryCount = 3)
        {
            return await DoAsync(action, retryWhen, CancellationToken.None, retryCount).ConfigureAwait(false);
        }

        public static async Task<T> DoAsync<T>(Func<Task<T>> action, Func<Task> retryWhen, CancellationToken ct, int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

                if (ct.IsCancellationRequested)
                {
                    return default(T);
                }

                await retryWhen().ConfigureAwait(false);
            }

            throw new AggregateException(exceptions);
        }
    }
}

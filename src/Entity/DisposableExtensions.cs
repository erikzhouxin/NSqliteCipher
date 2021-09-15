using System;
using System.Threading.Tasks;

namespace System.Data.SQLiteEFCore
{
    internal static class DisposableExtensions
    {
        public static ValueTask DisposeAsyncIfAvailable([CanBeNull] this IDisposable disposable)
        {
            if (disposable != null)
            {
                if (disposable is IAsyncDisposable asyncDisposable)
                {
                    return asyncDisposable.DisposeAsync();
                }

                disposable.Dispose();
            }

            return default;
        }
    }
}

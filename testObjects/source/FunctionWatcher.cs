using testObjects.source.capture;

namespace testObjects.source
{
    public class FunctionWatcher
    {
        public static async Task<CoverageResult<T>> Execute<T>(Func<T, CancellationToken, CoverageResult<T>> func, T i)
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(1000);
            var token = tokenSource.Token;

            var res = Task.Run(() => func(i,token),token);
            
            try
            {
                return await res;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }
}
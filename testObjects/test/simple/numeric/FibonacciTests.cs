using testObjects.source.simple.numeric;
using Xunit;

namespace testObjects.test.simple.numeric
{
    public class FibonacciTests
    {
        private readonly Fibonacci _fibonacci;

        public FibonacciTests()
        {
            _fibonacci = new Fibonacci();
        }
        
        [Theory]
        [MemberData(nameof(FibonacciNumbers))]
        public void FibonacciOfNShouldEqual(int n, int answer)
        {
            var fib = new Fibonacci();
            var result = fib.GetRecursive(n,CancellationToken.None);
            
            Assert.Equal(result.GetReturnValue(),answer);
        }
        
        [Theory]
        [MemberData(nameof(FibonacciNumbers))]
        public void FibonacciIterativeOfNShouldEqual(int n, int answer)
        {
            var result = _fibonacci.GetIterative(n);
            
            Assert.Equal(result.GetReturnValue(),answer);
        }
        
        public static IEnumerable<object[]> FibonacciNumbers()
        {
            yield return new object[] {1, 1};
            yield return new object[] {2, 1};
            yield return new object[] {3, 2};
            yield return new object[] {4, 3};
            yield return new object[] {5, 5};
            yield return new object[] {6, 8};
            yield return new object[] {7, 13};
            yield return new object[] {8, 21};
            yield return new object[] {9, 34};
            yield return new object[] {10, 55};
            
        }
    }
}
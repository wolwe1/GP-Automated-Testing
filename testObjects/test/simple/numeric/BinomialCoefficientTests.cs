using testObjects.source.simple.numeric;
using Xunit;

namespace testObjects.test.simple.numeric
{
    public class BinomialCoefficientTests
    {
        private readonly BinomialCoefficient _coefficient;
        
        public BinomialCoefficientTests()
        {
            _coefficient = new BinomialCoefficient();
        }

        [Theory]
        [MemberData(nameof(BinomialCoefficients))]
        public void BinomialCoefficientsShouldEqual(int n, int k, int answer)
        {
            var result = _coefficient.Get(n, k);
            
            Assert.Equal(result.GetReturnValue(),answer);
        }

        public static IEnumerable<object[]> BinomialCoefficients()
        {
            yield return new object[] {1, 1, 1};
            yield return new object[] {2, 1, 2};
            yield return new object[] {1, 2, 0};
            yield return new object[] {3, 2, 3};
            yield return new object[] {4, 2, 6};
            yield return new object[] {5, 2, 10};
            yield return new object[] {65, 2, 2080};
        }
    }
}
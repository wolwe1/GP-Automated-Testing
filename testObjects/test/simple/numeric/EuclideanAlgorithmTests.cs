using testObjects.source.simple.numeric;
using Xunit;

namespace testObjects.test.simple.numeric
{
    public class EuclideanAlgorithmTests
    {
        private readonly EuclideanAlgorithm _euclideanAlgorithm;

        public EuclideanAlgorithmTests()
        {
            _euclideanAlgorithm = new EuclideanAlgorithm();
        }

        [Theory]
        [MemberData(nameof(GcdAnswers))]
        public void GcdOfTwoNumbersShouldEqual(int first, int second, int answer)
        {
            var result = _euclideanAlgorithm.Get(first, second);
                
            Assert.Equal(result.GetReturnValue(),answer);
        }
        
        
        [Theory]
        [MemberData(nameof(GcdAnswers))]
        public void RecursiveGcdOfTwoNumbersShouldEqual(int first, int second, int answer)
        {
            var result = _euclideanAlgorithm.GetRecursive(first, second);
                
            Assert.Equal(result.GetReturnValue(),answer);
        }

        public static IEnumerable<object[]> GcdAnswers()
        {
            yield return new object[] {1, 1, 1 };
            yield return new object[] {1, 1, 1};
            yield return new object[] {1, 2, 1};
            yield return new object[] {1, 10, 1};
                
            yield return new object[] {2, 1, 1};
            yield return new object[] {2, 2, 2};
            yield return new object[] {2, 4, 2};
            yield return new object[] {2, 428, 2};
            yield return new object[] {2, 7, 1};
            yield return new object[] {2, 73, 1};
            yield return new object[] {2, 739, 1};
                
            yield return new object[] {3, 3, 3};
            yield return new object[] {3, 6, 3};
            yield return new object[] {3, 739, 1};

            yield return new object[] {9, 9, 9};
            yield return new object[] {9, 6, 3};
            yield return new object[] {9, 5, 1};

        }
    }
}
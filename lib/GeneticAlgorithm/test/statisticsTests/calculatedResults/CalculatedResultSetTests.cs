using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using Xunit;

namespace lib.GeneticAlgorithm.test.statisticsTests.calculatedResults
{
    public class CalculatedResultSetTests
    {
        [Theory]
        [MemberData(nameof(TestMapSet))]
        public static void MapModifiesListCorrectly(CalculationResultExtensions.MapFunc func)
        {
            var set = GetResultSet();

            var list = GetResultList().Map(func);
            var manualSet = new CalculationResultSet(list);

            var modifiedBySet = set.Map(func);

            Assert.True(manualSet.Matches(modifiedBySet));
        }

        [Fact]
        public static void SetsOfDifferingSizesDoNotMatch()
        {
            var set = GetResultSet();

            var list = GetResultList().GetRange(0, 8);
            var manualSet = new CalculationResultSet(list);

            Assert.False(manualSet.Matches(set));
        }

        [Fact]
        public static void SetsWithDifferentResultDoNotMatch()
        {
            var originalSet = GetResultSet();
            var modifiedSet = originalSet.Map(x => x - 1);

            Assert.False(originalSet.Matches(modifiedSet));
        }

        [Fact]
        public static void MaxReturnsLargestResult()
        {
            var originalSet = GetResultSet();
            var modifiedSet = originalSet.Map(x => x == 5 ? 50 : x);

            Assert.Equal(9, originalSet.Max());
            Assert.Equal(50, modifiedSet.Max());
        }

        [Fact]
        public static void GetThrowsOnInvalidAccess()
        {
            var set = GetResultSet();

            Assert.Throws<IndexOutOfRangeException>(() => set.Get(-1));
            Assert.Throws<IndexOutOfRangeException>(() => set.Get(10));
        }

        public static IEnumerable<object[]> TestMapSet()
        {
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x / 2)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x + 3)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x - 1)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x * 20)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) Math.Cos};
        }

        private static List<CalculationResult> GetResultList()
        {
            var resultList = new List<CalculationResult>();
            for (var i = 0; i < 10; i++)
            {
                var num = i;
                var member = new RandomNumberMember(i);

                var newResult = new CalculationResult(num, member.GetId());
                resultList.Add(newResult);
            }

            return resultList;
        }

        private static CalculationResultSet GetResultSet()
        {
            var resultList = GetResultList();

            return new CalculationResultSet(resultList);
        }
    }
}
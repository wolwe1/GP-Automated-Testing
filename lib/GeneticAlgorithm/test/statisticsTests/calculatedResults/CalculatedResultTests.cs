using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using Xunit;

namespace lib.GeneticAlgorithm.test.statisticsTests.calculatedResults
{
    public class CalculatedResultTests
    {
        [Fact]
        public static void ValuesRemainUnchanged()
        {
            var num = 0;
            var member = new RandomNumberMember(0);

            var result = new CalculationResult(num, member.GetId());

            num = 1;
            member = new RandomNumberMember(1);

            Assert.Equal(0, result.GetResult());
            Assert.Equal("0", result.GetMemberId());
        }

        [Fact]
        public static void OriginalListRemainsIntactOnChange()
        {
            var origin = GetResultList();

            var newList = origin.Map(x => x + 1);
            var acc = origin.Accumulator((x, n) => x + n);

            for (var i = 0; i < 10; i++)
            {
                var calc = origin.ElementAt(i).GetResult();

                Assert.Equal(i, calc);
            }
        }

        [Fact]
        public static void AccumulatorThrowsOnEmpty()
        {
            var origin = new List<CalculationResult>();

            Assert.Throws<Exception>(() => origin.Accumulator((x, n) => x + n));
        }

        [Theory]
        [MemberData(nameof(AccumulatorSet))]
        public static void AccumulatorAppliesCorrectly(CalculationResultExtensions.AccFunc func, double expected)
        {
            var origin = GetResultList();

            var acc = origin.Accumulator(func);

            Assert.Equal(expected, acc);
        }

        [Theory]
        [MemberData(nameof(TestMapSet))]
        public static void MapModifiesListCorrectly(CalculationResultExtensions.MapFunc func)
        {
            var origin = GetResultList();

            var newList = origin.Map(func);

            for (var i = 0; i < 10; i++)
            {
                var calc = origin.ElementAt(i).GetResult();
                var mappedCalc = newList.ElementAt(i).GetResult();

                var manualCalc = func(calc);

                Assert.Equal(manualCalc, mappedCalc);
            }
        }

        public static IEnumerable<object[]> TestMapSet()
        {
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x / 2)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x + 3)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x - 1)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) (x => x * 20)};
            yield return new object[] {(CalculationResultExtensions.MapFunc) Math.Cos};
        }

        public static IEnumerable<object[]> AccumulatorSet()
        {
            yield return new object[] {(CalculationResultExtensions.AccFunc) ((x, n) => x + n), 45};
            yield return new object[] {(CalculationResultExtensions.AccFunc) ((x, n) => x - n), -45};
            //yield return new object[] {(CalculationResultExtensions.AccFunc) ((x,n) => x / n)};
            yield return new object[] {(CalculationResultExtensions.AccFunc) ((x, n) => x * n), 0};
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
    }
}
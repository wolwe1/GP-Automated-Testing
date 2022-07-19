using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.history;
using Xunit;

namespace lib.GeneticAlgorithm.test.statisticsTests.historyTests
{
    public class RunHistoryTests
    {
        [Fact]
        public static void GetGenerationReturnsCorrectRun()
        {
            var history = GetHistory();

            var gen1 = history.GetGeneration(0);
            var gen10 = history.GetGeneration(9);

            Assert.Equal(45, gen1.GetTotalFitness());
            Assert.Equal(945, gen10.GetTotalFitness());
        }

        [Fact]
        public static void GetGenerationThrowsOnInvalidAccess()
        {
            var history = GetHistory();

            Assert.Throws<IndexOutOfRangeException>(() => history.GetGeneration(-1));
            Assert.Throws<IndexOutOfRangeException>(() => history.GetGeneration(10));
        }

        private static RunRecord<double> GetHistory()
        {
            var history = new RunRecord<double>(0);

            for (var i = 0; i < 10; i++) history.AddGeneration(GetEval(i));

            return history;
        }

        private static GenerationRecord<double> GetEval(int generation)
        {
            var eval = new GenerationRecord<double>();

            for (var i = 0; i < 10; i++)
            {
                var fitnessValue = generation * 10 + i;
                var member = new RandomNumberMember(fitnessValue);
                eval.Add(member, new Fitness(null,fitnessValue));
            }

            return eval;
        }
    }
}
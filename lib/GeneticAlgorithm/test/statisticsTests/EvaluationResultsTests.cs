using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.mockImplementations;
using lib.GeneticAlgorithm.source.statistics;
using Xunit;

namespace lib.GeneticAlgorithm.test.statisticsTests
{
    public class EvaluationResultsTests
    {
        [Fact]
        public static void SizeIsCorrect()
        {
            var eval = GetEval();

            Assert.Equal(10, eval.Size());
        }

        [Fact]
        public static void FitnessValuesCorrectInOrder()
        {
            var eval = GetEval();

            var fitnessValues = eval.GetFitnessValues();

            var evalResults = fitnessValues.ToList();

            for (var i = 0; i < 10; i++)
            {
                var result = evalResults.ElementAt(i);
                Assert.Equal(i, result.GetResult());
            }
        }

        [Fact]
        public static void TotalFitnessIsCorrect()
        {
            var eval = GetEval();
            var totalFitness = eval.GetTotalFitness();

            Assert.Equal(45, totalFitness);
        }

        public static GenerationRecord<double> GetEval()
        {
            var eval = new GenerationRecord<double>();

            for (var i = 0; i < 10; i++)
            {
                var member = new RandomNumberMember(i);
                eval.Add(member, new Fitness(null,i));
            }

            return eval;
        }
    }
}
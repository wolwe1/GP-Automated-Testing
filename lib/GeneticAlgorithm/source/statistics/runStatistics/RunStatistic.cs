using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using lib.GeneticAlgorithm.source.statistics.output;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics
{
    public abstract class RunStatistic : IRunStatistic
    {
        private readonly string _heading;
        protected string Scale;

        protected RunStatistic(string heading)
        {
            _heading = heading;
            Scale = "";
        }

        public abstract StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> generationResults);

        protected static List<CalculationResultSet> CreateGenerationResultSets<T>(
            List<GenerationRecord<T>> generationResults)
        {
            var convertedGenerationResults = new List<CalculationResultSet>();

            foreach (var result in generationResults)
            {
                var set = result.GetFitnessValues();
                convertedGenerationResults.Add(set);
            }

            return convertedGenerationResults;
        }

        protected StatisticOutput GetOutput(CalculationResultSet generationStats, double runStatistic)
        {
            var output = new StatisticOutput()
                .SetHeading(_heading)
                .SetGenerationValues(generationStats)
                .SetRunValue(runStatistic)
                .SetScale(Scale);

            return output;
        }
    }
}
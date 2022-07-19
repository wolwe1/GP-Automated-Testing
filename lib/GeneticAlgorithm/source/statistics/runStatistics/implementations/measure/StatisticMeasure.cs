using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure
{
    public abstract class StatisticMeasure : IStatisticMeasure
    {
        public abstract CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets);

        public abstract double GetRunStatistic(CalculationResultSet generationStats);
        public abstract string GetHeading();

        /// <summary>
        ///     Creates a <see cref="CalculationResultSet" /> from a list of calculated values, by attaching a generation number to
        ///     the value
        /// </summary>
        /// <param name="generationCalculations">The calculated statistical values for the generations</param>
        /// <returns>A <see cref="CalculationResultSet" /> representing the generations and their statistic</returns>
        protected CalculationResultSet CreateResultSetForGenerations(IEnumerable<double> generationCalculations)
        {
            var generationResults = new List<CalculationResult>();

            for (var i = 0; i < generationCalculations.Count(); i++)
            {
                var calculatedResult = generationCalculations.ElementAt(i);
                generationResults.Add(new CalculationResult(calculatedResult, $"Generation {i}"));
            }

            return new CalculationResultSet(generationResults);
        }
    }
}
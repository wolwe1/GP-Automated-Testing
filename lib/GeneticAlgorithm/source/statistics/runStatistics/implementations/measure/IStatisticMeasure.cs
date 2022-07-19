using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure
{
    public interface IStatisticMeasure
    {
        CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets);
        double GetRunStatistic(CalculationResultSet generationStats);
        string GetHeading();
    }
}
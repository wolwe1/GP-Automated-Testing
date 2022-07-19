using lib.GeneticAlgorithm.source.statistics.output;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics
{
    public interface IRunStatistic
    {
        StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> evaluationResults);
    }
}
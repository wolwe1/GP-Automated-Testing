using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.GeneticAlgorithm.source.statistics.output
{
    public interface IOutputPrinter
    {
        void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord);
    }
}
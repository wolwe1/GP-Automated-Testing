using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.runStatistics;

namespace lib.GeneticAlgorithm.source.statistics.history
{
    public interface IRunRecord
    {
        public List<StatisticOutput> Summarise(List<IRunStatistic> runStatistics);

        public int GetRunNumber();

        public List<IGenerationRecord> GetGenerationRecords();
    }
}
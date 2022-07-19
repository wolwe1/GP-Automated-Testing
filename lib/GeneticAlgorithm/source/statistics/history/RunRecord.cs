using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.runStatistics;

namespace lib.GeneticAlgorithm.source.statistics.history
{
    public class RunRecord<T> : IRunRecord
    {
        private readonly List<GenerationRecord<T>> _generationResults;
        private readonly int _runCount;

        public string AdditionalRunInfo { get; set; }

        public RunRecord(int runCount)
        {
            _runCount = runCount;
            _generationResults = new List<GenerationRecord<T>>();
            AdditionalRunInfo = "";
        }

        public void AddGeneration(GenerationRecord<T> generationRecord)
        {
            _generationResults.Add(generationRecord);
        }

        public GenerationRecord<T> GetGeneration(int index)
        {
            if (index >= _generationResults.Count || index < 0)
                throw new IndexOutOfRangeException(
                    $"Cannot get history of generation {index} with max of {_generationResults.Count} in run {_runCount}");

            return _generationResults.ElementAt(index);
        }

        public List<StatisticOutput> Summarise(List<IRunStatistic> runStatistics)
        {
            var outputs = new List<StatisticOutput>();
            foreach (var statistic in runStatistics)
            {
                var output = statistic.GetStatistic(_generationResults);
                outputs.Add(output);
            }

            return outputs;
        }

        public int GetRunNumber()
        {
            return _runCount;
        }

        public List<IGenerationRecord> GetGenerationRecords()
        {
            return _generationResults.Cast<IGenerationRecord>().ToList();
        }
    }
}
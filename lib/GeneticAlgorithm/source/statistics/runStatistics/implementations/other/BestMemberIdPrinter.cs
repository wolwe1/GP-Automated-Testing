using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.other
{
    public class BestMemberIdPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestMemberIdPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }

        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> generationRecords)
        {
            var bestMemberInRun = _bestMemberMeasure.GetBestPerformer(generationRecords);
          
            return new StatisticOutput()
                .SetHeading("Best member ID")
                .SetRunValue(bestMemberInRun.GetMemberId());
        }
    }
}
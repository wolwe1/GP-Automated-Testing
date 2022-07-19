using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure
{
    public class BestPerformerMeasure : StatisticMeasure
    {
        public override CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets)
        {
            var generationBests = generationResultSets.Select(GetBestPerformer);

            return new CalculationResultSet(generationBests);
        }

        public override double GetRunStatistic(CalculationResultSet generationStats)
        {
            return generationStats.Max();
        }

        public override string GetHeading()
        {
            return "Best";
        }

        public static CalculationResult GetBestPerformer(CalculationResultSet set)
        {
            var resultList = set.ToList();
            
            return resultList
                .OrderByDescending(r => r.GetResult())
                .FirstOrDefault();
        }

        public MemberRecord<T> GetBestPerformer<T>(IEnumerable<GenerationRecord<T>> generationRecords)
        {
            var bestMemberPerGeneration = generationRecords
                .Select(g => g.GetMemberWithMaxFitness());

            var bestMemberInRun = bestMemberPerGeneration
                .OrderByDescending(m => m.GetFitnessValue())
                .FirstOrDefault();

            return bestMemberInRun;
        }
    }
}
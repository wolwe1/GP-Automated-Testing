using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.runtime
{
    public class RunTimeStatistic : RunStatistic
    {
        private readonly IStatisticMeasure _measure;
        private IRuntimeMeasurement _runtimeMeasurement;

        public RunTimeStatistic(IStatisticMeasure measure) : base($"{measure.GetHeading()} Execution time")
        {
            _measure = measure;
            _runtimeMeasurement = new SecondsMeasurement();
        }

        public override StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> generationResults)
        {
            AdjustTimeScale(generationResults);
            var generationRunTimes = GetCalculationResultSet(generationResults);

            var averageRunTime = _measure.GetRunStatistic(generationRunTimes);

            return GetOutput(generationRunTimes, averageRunTime);
        }

        private void AdjustTimeScale<T>(List<GenerationRecord<T>> generationResults)
        {
            var sample = generationResults.ElementAt(0);
            var timeTaken = sample.RunTime.Milliseconds;

            _runtimeMeasurement = timeTaken switch
            {
                > 600000 => new HoursMeasurement(),
                > 60000 => new MinutesMeasurement(),
                > 2000 => new SecondsMeasurement(),
                _ => new MilliSecondMeasurement()
            };

            Scale = _runtimeMeasurement.GetScale();
        }


        private CalculationResultSet GetCalculationResultSet<T>(List<GenerationRecord<T>> generationResults)
        {
            var generationRunTimes = new List<CalculationResult>();

            for (var i = 0; i < generationResults.Count(); i++)
            {
                var generationResult = generationResults.ElementAt(i);

                var runtime = _runtimeMeasurement.GetRunTime(generationResult.RunTime);
                var calculationResult = new CalculationResult(runtime, $"Generation {i}");

                generationRunTimes.Add(calculationResult);
            }

            return new CalculationResultSet(generationRunTimes);
        }
    }
}
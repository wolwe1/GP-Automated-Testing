namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.runtime
{
    public class MilliSecondMeasurement : IRuntimeMeasurement
    {
        public double GetRunTime(TimeSpan time)
        {
            return time.Milliseconds;
        }

        public string GetScale()
        {
            return "ms";
        }
    }
}
namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.runtime
{
    public class SecondsMeasurement : IRuntimeMeasurement
    {
        public double GetRunTime(TimeSpan time)
        {
            return time.Seconds;
        }

        public string GetScale()
        {
            return "seconds";
        }
    }
}
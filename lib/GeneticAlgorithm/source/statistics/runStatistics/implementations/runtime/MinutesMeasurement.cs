namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.runtime
{
    public class MinutesMeasurement : IRuntimeMeasurement
    {
        public double GetRunTime(TimeSpan time)
        {
            return time.Minutes;
        }

        public string GetScale()
        {
            return "minutes";
        }
    }
}
namespace lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.runtime
{
    public interface IRuntimeMeasurement
    {
        double GetRunTime(TimeSpan time);
        string GetScale();
    }
}
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.terminationCriteria
{
    public interface ITerminationCriteria
    {
        bool Met<T>(int generationCount, GenerationRecord<T> generationRecord);
        string GetReason();
    }
}
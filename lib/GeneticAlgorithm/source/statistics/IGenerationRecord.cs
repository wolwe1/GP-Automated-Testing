using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics
{
    public interface IGenerationRecord
    {
        CalculationResultSet GetFitnessValues();

        double GetTotalFitness();

        int Size();
        List<IMemberRecord> GetMembers();
    }
}
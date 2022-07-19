using lib.GeneticAlgorithm.source.fitnessFunctions;

namespace lib.GeneticAlgorithm.source.statistics
{
    public interface IMemberRecord
    {
        double GetFitnessValue();

        string GetMemberId();

        Fitness GetFitness();
    }
}
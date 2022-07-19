using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.fitnessFunctions
{
    public interface IFitnessFunction
    {
        public Fitness Evaluate<T>(IPopulationMember<T> member) where T : IComparable;
        CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results);

        MemberRecord<T> GetBest<T>(GenerationRecord<T> candidates);
        MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers);
    }
}
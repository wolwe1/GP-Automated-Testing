using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.operators
{
    public interface IPopulationMutator<T>
    {
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
        List<IPopulationMember<T>> ApplyOperators(List<MemberRecord<T>> parents);
    }
}
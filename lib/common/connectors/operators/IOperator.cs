using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators
{
    public interface IOperator<T>
    {
        IEnumerable<string> CreateModifiedChildren(List<string> parents, IPopulationGenerator<T> populationGenerator);
        IEnumerable<IPopulationMember<T>> CreateModifiedChildren(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator);
    }
}
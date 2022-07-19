using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel
{
    public interface IControlModel<T>
    {
        bool TerminationCriteriaMet(int generationCount, GenerationRecord<T> generationRecord);
        List<string> SelectParentsReturningIds(GenerationRecord<T> results);
        List<MemberRecord<T>> SelectParents(GenerationRecord<T> results);
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
        int GetInitialPopulationSize();
        GenerationRecord<T> Evaluate(List<IPopulationMember<T>> population);
        List<IPopulationMember<T>> ApplyOperators(List<MemberRecord<T>> parents);
        void SetSeed(int seed);
    }
}
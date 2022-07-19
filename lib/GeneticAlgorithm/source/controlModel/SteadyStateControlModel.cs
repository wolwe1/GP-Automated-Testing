using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.operators;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel
{
    public class SteadyStateControlModel<T> : ControlModel<T> where T : IComparable
    {
        public SteadyStateControlModel(IPopulationMutator<T> mutator,IFitnessFunction fitnessFunc) : base(mutator,fitnessFunc)
        {
        }

        public override List<string> SelectParentsReturningIds(GenerationRecord<T> results)
        {
            var populationSizeToMaintain = results.Size();
            return SelectionMethod.SelectReturningIds(results, populationSizeToMaintain);
        }

        public override List<MemberRecord<T>> SelectParents(GenerationRecord<T> results)
        {
            var populationSizeToMaintain = results.Size();
            return SelectionMethod.Select(results, populationSizeToMaintain);
        }
        
    }
}
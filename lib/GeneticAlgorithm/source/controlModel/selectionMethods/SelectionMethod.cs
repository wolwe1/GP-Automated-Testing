using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.selectionMethods
{
    public abstract class SelectionMethod : ISelectionMethod
    {
        protected readonly IFitnessFunction FitnessFunction;

        protected SelectionMethod(IFitnessFunction function)
        {
            FitnessFunction = function;
        }

        public abstract List<string> SelectReturningIds<T>(GenerationRecord<T> results, int maxParentsToProduce);

        public abstract List<MemberRecord<T>> Select<T>(GenerationRecord<T> results, int maxParentsToProduce);

        public abstract void SetSeed(int seed);

    }
}
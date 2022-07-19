using lib.GeneticAlgorithm.source.controlModel.selectionMethods;
using lib.GeneticAlgorithm.source.controlModel.terminationCriteria;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.operators;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel
{
    public abstract class ControlModel<T> : IControlModel<T> where T : IComparable
    {
        private readonly List<ITerminationCriteria> _terminationCriteria;
        private readonly IPopulationMutator<T> _populationMutator;
        private readonly IFitnessFunction _fitnessFunction;
        private int _popSize;
        
        protected ISelectionMethod SelectionMethod;

        protected ControlModel(IPopulationMutator<T> populationMutator, IFitnessFunction fitnessFunction)
        {
            _populationMutator = populationMutator;
            _fitnessFunction = fitnessFunction;
            _terminationCriteria = new List<ITerminationCriteria>();
            _popSize = 0;
        }

        public bool TerminationCriteriaMet(int generationCount, GenerationRecord<T> generationRecord)
        {
            foreach (var criterion in _terminationCriteria)
                if (criterion.Met(generationCount, generationRecord))
                {
                    Console.WriteLine($"Run stopped due to: {criterion.GetReason()}");
                    return true;
                }

            return false;
        }

        public abstract List<string> SelectParentsReturningIds(GenerationRecord<T> results);

        public abstract List<MemberRecord<T>> SelectParents(GenerationRecord<T> results);

        public List<IPopulationMember<T>> ApplyOperators(List<string> parents)
        {
            return _populationMutator.ApplyOperators(parents);
        }

        public GenerationRecord<T> Evaluate(List<IPopulationMember<T>> population)
        {
            var results = new GenerationRecord<T>();

            foreach (var member in population)
            {
                var fitness = _fitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }

        public List<IPopulationMember<T>> ApplyOperators(List<MemberRecord<T>> parents)
        {
            return _populationMutator.ApplyOperators(parents);
        }

        public void SetSeed(int seed)
        {
            SelectionMethod.SetSeed(seed);
        }

        public ControlModel<T> UseSelection(ISelectionMethod newMethod)
        {
            SelectionMethod = newMethod;

            return this;
        }

        public ControlModel<T> UseTerminationCriteria(ITerminationCriteria newCriteria)
        {
            _terminationCriteria.Add(newCriteria);

            return this;
        }
        
        public ControlModel<T> SetPopulationSize(int popSize)
        {
            _popSize = popSize;

            return this;
        }
        
        public int GetInitialPopulationSize()
        {
            return _popSize;
        }
    }
}
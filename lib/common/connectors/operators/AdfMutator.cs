using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.operators;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators
{
    public class AdfMutator<T> : IPopulationMutator<T>
    {
        private readonly List<IOperator<T>> _operators;
        private readonly IPopulationGenerator<T> _populationGenerator;
        
        public AdfMutator(IPopulationGenerator<T> populationGenerator)
        {
            _populationGenerator = populationGenerator;
            _operators = new List<IOperator<T>>();
        }

        public List<IPopulationMember<T>> ApplyOperators(List<string> parents)
        {
            var newOffspring = new List<string>();

            foreach (var op in _operators)
            {
                newOffspring.AddRange(op.CreateModifiedChildren(parents,_populationGenerator));
            }

            return newOffspring.Select(off => _populationGenerator.GenerateFromId(off)).ToList();
        }

        public List<IPopulationMember<T>> ApplyOperators(List<MemberRecord<T>> parents)
        {
            var newOffspring = new List<IPopulationMember<T>>();

            foreach (var op in _operators)
            {
                newOffspring.AddRange(op.CreateModifiedChildren(parents,_populationGenerator));
            }

            return newOffspring;
        }

        public AdfMutator<T> UseOperator(IOperator<T> op)
        {
            _operators.Add(op);
            return this;
        }
    }
}
using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.common.connectors.ga;
using lib.common.visitors;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators.implementation
{
    public class MutationOperator<T,TU> : Operator<T> where T : IComparable where TU : IComparable
    {
        private readonly int _maxModificationDepth;

        public MutationOperator(int applicationPercentage, int maxModificationDepth) : base(applicationPercentage)
        {
            _maxModificationDepth = maxModificationDepth;
        }

        protected override IEnumerable<IPopulationMember<T>> Operate(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator)
        {
            var adf = GetAdfFromParents(parents);

            var mainToMutate = RandomNumberFactory.Next(adf.GetNumberOfMainPrograms());

            var mutator = CreateMutator(adf,mainToMutate,populationGenerator);

            adf.VisitMain(mainToMutate, mutator);

            return new List<IPopulationMember<T>>(){ new StateAdfPopulationMember<T,TU>(adf)};
        }

        protected override IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            var adf = GetAdfFromParents(parents,generator);

            var mainToMutate = RandomNumberFactory.Next(adf.GetNumberOfMainPrograms());

            var mutator = CreateMutator(adf,mainToMutate,generator);

            adf.VisitMain(mainToMutate, mutator);

            return new List<string>(){adf.GetId()};
        }

        private NodeMutatorVisitor<T> CreateMutator(Adf<T> adf,int mainToMutate, IPopulationGenerator<T> generator)
        {
            var numberOfNodes = adf.GetMainNodeCount(mainToMutate);
            //Prevent root node replacement
            var nodeToReplace = RandomNumberFactory.Next(numberOfNodes - 1) + 1;
            
            return new NodeMutatorVisitor<T>(nodeToReplace, generator,_maxModificationDepth);
        }
    }
}
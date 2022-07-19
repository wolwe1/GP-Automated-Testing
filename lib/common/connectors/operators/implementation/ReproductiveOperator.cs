using lib.AutomaticallyDefinedFunctions.factories;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators.implementation
{
    public class ReproductiveOperator<T> : Operator<T> where T : IComparable
    {
        public ReproductiveOperator(int applicationPercentage) : base(applicationPercentage)
        {
        }

        protected override IEnumerable<IPopulationMember<T>> Operate(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator)
        {
            //No op
            var chosenParent = RandomNumberFactory.Next(parents.Count);
            
            return new List<IPopulationMember<T>>(){parents.ElementAt(chosenParent).Member.GetCopy()};
        }

        protected override List<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            //No op
            var chosenParent = RandomNumberFactory.Next(parents.Count);
            
            return new List<string>(){parents.ElementAt(chosenParent)};
        }
    }
}
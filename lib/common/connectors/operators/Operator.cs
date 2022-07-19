using System.Collections;
using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.common.connectors.ga;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics;

namespace lib.common.connectors.operators
{
    public abstract class Operator<T> : IOperator<T> where T : IComparable
    {
        protected readonly int ApplicationPercentage;
        
        public Operator(int applicationPercentage)
        {
            ApplicationPercentage = applicationPercentage;
        }

        public IEnumerable<string> CreateModifiedChildren(List<string> parents, IPopulationGenerator<T> populationGenerator)
        {
            var numberOfOffspringToProduce = GetNumberOfOffspringToProduce(parents);

            var offspring = new List<string>();

            while (offspring.Count < numberOfOffspringToProduce)
            {
                var mutatedOffspring = Operate(parents,populationGenerator);
                offspring.AddRange(mutatedOffspring);
            }

            return offspring;
        }

        public IEnumerable<IPopulationMember<T>> CreateModifiedChildren(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator)
        {
            var numberOfOffspringToProduce = GetNumberOfOffspringToProduce(parents);

            var offspring = new List<IPopulationMember<T>>();

            while (offspring.Count < numberOfOffspringToProduce)
            {
                var mutatedOffspring = Operate(parents,populationGenerator);
                offspring.AddRange(mutatedOffspring);
            }

            return offspring;
        }

        protected abstract IEnumerable<IPopulationMember<T>> Operate(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator);

        protected abstract IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> populationGenerator);

        protected TU PickParent<TU>(List<TU> parents)
        {
            var chosenParentIndex = RandomNumberFactory.Next(parents.Count);
            return parents.ElementAt(chosenParentIndex);
        }
        protected virtual int GetNumberOfOffspringToProduce(ICollection parents)
        {
            var applicationPercentage = ApplicationPercentage / 100d;

            var numberOfOffspring = applicationPercentage * parents.Count;

            return (int) numberOfOffspring;
        }


        protected Adf<T> GetAdfFromParents(List<string> parents,IPopulationGenerator<T> generator)
        {
            var parent = PickParent(parents);

            var pop = (AdfPopulationMember<T>)generator.GenerateFromId(parent);
            return pop.GetAdf();
        }
        
        protected Adf<T> GetAdfFromParents(List<MemberRecord<T>> parents)
        {
            var parent = PickParent(parents).Member;

            var adf = ((AdfPopulationMember<T>) parent).GetAdf();
            return adf;
        }
        
    }
}
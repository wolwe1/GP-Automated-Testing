using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.fitnessFunctions;

namespace lib.common.fitnessFunctions
{
    public abstract class AdfFitnessFunction : FitnessFunction
    {
        protected readonly Test<object> Test;

        protected AdfFitnessFunction(Test<object> test)
        {
            Test = test;
        }

        public abstract override Fitness Evaluate<TU>(IPopulationMember<TU> member);
    }
}
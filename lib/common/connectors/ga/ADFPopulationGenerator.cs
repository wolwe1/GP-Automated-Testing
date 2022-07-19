using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.connectors.ga
{
    public class AdfPopulationGenerator<T> : IPopulationGenerator<T> where T : IComparable
    {
        protected AdfGenerator<T> Generator;

        public AdfPopulationGenerator(int seed, AdfSettings settings)
        {
            Generator = new AdfGenerator<T>(seed,settings);
        }
        
        protected AdfPopulationGenerator() { }

        public virtual IPopulationMember<T> GenerateNewMember()
        {
            var newAdf = Generator.Generate();
            return new AdfPopulationMember<T>(newAdf);
        }

        public virtual IPopulationMember<T> GenerateFromId(string chromosome)
        {
            var newAdf = Generator.GenerateFromId(chromosome);

            return new AdfPopulationMember<T>(newAdf);
        }

        public void SetSeed(int seed)
        {
            Generator.SetSeed(seed);
        }

        public AdfGenerator<T> GetGenerator()
        {
            return Generator;
        }

        public FunctionCreator GetFunctionCreator()
        {
            return Generator.GetFunctionCreator();
        }
    }
}
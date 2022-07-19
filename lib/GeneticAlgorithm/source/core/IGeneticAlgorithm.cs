using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.GeneticAlgorithm.source.core
{
    public interface IGeneticAlgorithm<T>
    {
        public List<IPopulationMember<T>> CreateInitialPopulation();

        public IExecutionHistory<T> Run();

        public void SetSeed(int seed);
    }
}
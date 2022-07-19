using lib.GeneticAlgorithm.source.controlModel;
using lib.GeneticAlgorithm.source.core;
using lib.GeneticAlgorithm.source.core.population;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.common.connectors.ga
{
    public class AdfGeneticAlgorithm<T> : GeneticAlgorithm<T> where T : IComparable
    {
        public AdfGeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, IExecutionHistory<T> history) : base(populationGenerator, controlModel, history)
        {
            
        }
    }
}
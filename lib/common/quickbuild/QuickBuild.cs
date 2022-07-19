using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.common.connectors.ga.state;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.quickbuild
{
    public class QuickBuild
    {
        public static IPopulationGenerator<T> CreateStatePopulationGenerator<T,TU>() where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T, TU>(3,8,1,60);

            return new StateAdfPopulationGenerator<T, TU>(0, settings);
        }
    }
}
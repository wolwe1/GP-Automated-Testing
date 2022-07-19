using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.state;
using lib.common.connectors.ga.state;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.connectors.ga
{
    public class StateAdfPopulationMember<T,TU> : AdfPopulationMember<T> where T : IComparable where TU : IComparable
    {
        public StateAdfPopulationMember(Adf<T> adf) : base(adf) { }
        
        public override IOutputContainer<T> GetResult()
        {
            var stateAdf = (StateBasedAdf<T,TU>) Adf;
            return new StateAdfOutputContainer<T,TU>(stateAdf.GetHistory());
        }
        
        public override IPopulationMember<T> GetCopy()
        {
            return new StateAdfPopulationMember<T,TU>(Adf.GetCopy());
        }
    }
    
    
}
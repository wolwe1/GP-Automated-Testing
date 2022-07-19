using lib.AutomaticallyDefinedFunctions.structure.adf.helpers;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.connectors
{
    public class AdfOutputContainer<T>  : AdfOutput<T>, IOutputContainer<T>
    {
        public AdfOutputContainer(AdfOutput<T> adfOutput) : base(adfOutput.GetOutputs()) { }

        public string GetOutputString()
        {
            throw new NotImplementedException();
        }
    }
}
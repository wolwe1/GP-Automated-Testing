using System.Text;
using lib.AutomaticallyDefinedFunctions.structure.adf.helpers;
using lib.AutomaticallyDefinedFunctions.structure.state;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.connectors.ga.state
{
    public class StateAdfOutputContainer<T,TU>  : AdfOutput<T>, IOutputContainer<T>
    {
        private AdfHistory<T, TU> _history;

        public StateAdfOutputContainer(AdfOutput<T> adfOutput) : base(adfOutput.GetOutputs()) { }

        public StateAdfOutputContainer(AdfHistory<T, TU> history) : base(ArraySegment<Output<T>>.Empty)
        {
            _history = history;
        }

        public string GetOutputString()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine();
            for (var index = 0; index < _history.OutputsWithResponse.Count; index++)
            {
                var output = _history.OutputsWithResponse.ElementAt(index);
                var adfOutput = output.Key;
                var programResponse = output.Value;
                var failed = adfOutput.GetOutputs().Any(o => o.Failed);
                var successOutput = failed ? "Failed" : "Success";
                
                strBuilder.Append($"\nOutput {index} : {successOutput}");

                if (!failed)
                {
                    strBuilder.Append($" Value - {string.Join("-",adfOutput.GetOutputValues())}");
                    strBuilder.Append($" Prog resp - {programResponse}");
                }
            }

            return strBuilder.ToString();
        }
    }
}
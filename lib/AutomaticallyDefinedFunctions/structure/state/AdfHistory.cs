using lib.AutomaticallyDefinedFunctions.structure.adf.helpers;

namespace lib.AutomaticallyDefinedFunctions.structure.state
{
    public class AdfHistory<T,TU>
    {
        public Dictionary<AdfOutput<T>,TU> OutputsWithResponse { get; }

        public AdfHistory()
        {
            OutputsWithResponse = new Dictionary<AdfOutput<T>, TU>();
        }

        public void AddHistory(AdfOutput<T> output, TU programResponse)
        {
            OutputsWithResponse.Add(output,programResponse);
        }

        public void AddHistory(AdfOutput<T> output)
        {
            OutputsWithResponse.Add(output,default);
        }
    }
}
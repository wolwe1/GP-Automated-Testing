using lib.AutomaticallyDefinedFunctions.factories.valueNodes.state;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    /*
     * A specialisation of AdfSettings that adds state based nodes to the set of nodes available for construction
     */
    public class StateAdfSettings<TAdfResponse,TProgResponse> : AdfSettings
    {
        public StateAdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance) : base(
            maxFunctionDepth, maxMainDepth, argumentCount, terminalChance)
        {
            ValueNodeFactories.Add(new ProgramResponseStateFactory<TProgResponse>());
            ValueNodeFactories.Add(new ExecutionCountStateFactory());
            ValueNodeFactories.Add(new LastOutputStateFactory<TAdfResponse>());
            ValueNodeFactories.Add(new OutputFailureStateFactory());
            ValueNodeFactories.Add(new RandomStateFactory());
        }
    }
}
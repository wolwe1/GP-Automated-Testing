using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.state;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    public class StateAdfGenerator<TAdfOutput,TProgResponse> : AdfGenerator<TAdfOutput> where TAdfOutput : IComparable where TProgResponse : IComparable
    {
        public StateAdfGenerator(int seed, StateAdfSettings<TAdfOutput,TProgResponse> settings) : base(seed, settings) {}

        public override Adf<TAdfOutput> Generate()
        {
            var newAdf = new StateBasedAdf<TAdfOutput, TProgResponse>();
            return (StateBasedAdf<TAdfOutput, TProgResponse>) LoadAdf(newAdf);
        }
        
        // public new StateBasedAdf<TAdfOutput,TProgResponse> GenerateFromId(string originalId)
        // {
        //     return (StateBasedAdf<TAdfOutput, TProgResponse>) base.GenerateFromId(originalId);
        // }
        
        protected override Adf<TAdfOutput> CreateAdfFrom(IEnumerable<MainProgram<TAdfOutput>> mainPrograms, IEnumerable<FunctionDefinition<TAdfOutput>> definitions)
        {
            var adf = new StateBasedAdf<TAdfOutput, TProgResponse>();

            foreach (var definition in definitions)
            {
                adf.UseDefinition(definition);
            }

            foreach (var mainProgram in mainPrograms)
            {
                adf.UseMain(mainProgram);
            }

            return adf;
        }
    }
}
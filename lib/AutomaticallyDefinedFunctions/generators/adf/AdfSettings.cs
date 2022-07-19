using lib.AutomaticallyDefinedFunctions.factories.comparators;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories.operators;
using lib.AutomaticallyDefinedFunctions.factories.valueNodes;
using lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    /*
     * A class wrapping various generation settings for program trees
     */
    public class AdfSettings
    {
        public readonly int MaxFunctionDepth;
        public readonly int MaxMainDepth;
        public readonly int ArgumentCount;
        public readonly int TerminalChance;

        public List<IFunctionFactory> Factories { get; set; }
        public List<ComparatorFactory> Comparators { get; set; }
        public List<IValueNodeFactory> ValueNodeFactories { get; set; }
        
        //Optional
        public readonly int NumberOfFunctions;

        public AdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance)
        {
            MaxFunctionDepth = maxFunctionDepth;
            MaxMainDepth = maxMainDepth;
            ArgumentCount = argumentCount;
            TerminalChance = terminalChance;

            NumberOfFunctions = 1;
            
            SetFactories();
            SetComparators();
        }

        /*
         * The factories added here determine what programs may consist of.
         * Adding new ones, or removing them will change the nodes available when constructing program trees
         */
        private void SetFactories()
        {
            Factories = new List<IFunctionFactory>()
            {
                new AddFunctionFactory(),
                new SubtractFunctionFactory(),
                new MultiplicationFunctionFactory(),
                new DivisionFunctionFactory(),
                new IfFunctionFactory(),
                new LoopFunctionFactory(),
                new LengthOfFunctionFactory()
            };

            ValueNodeFactories = new List<IValueNodeFactory>()
            {
                new ValueNodeFactory()
            };
        }
        
        /*
         * Similarly to the factories, this method determines what comparators may be used in program construction
         */
        private void SetComparators()
        {
            Comparators = new List<ComparatorFactory>()
                {
                    new LessThanComparatorFactory(),
                    new GreaterThanComparatorFactory(),
                    new EqualComparatorFactory(),
                    new NotEqualComparatorFactory(),
                };
        }
    }
}
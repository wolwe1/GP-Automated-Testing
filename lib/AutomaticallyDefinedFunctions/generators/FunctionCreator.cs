using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.factories.comparators;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.factories.valueNodes;
using lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard;
using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.generators
{
    /*
     * A generator capable of creating programs using the provided settings
     */
    public class FunctionCreator : FactoryManager
    {
        private readonly bool _useNullTerminals;
        private readonly AdfSettings _settings;

        /*
         * When 'useNullTerminals' is true, terminal nodes will node be set. This is used when creating function definitions
         */
        public FunctionCreator(AdfSettings settings,bool useNullTerminals) : base(settings)
        {
            _useNullTerminals = useNullTerminals;
            _settings = settings;
        }
        
        /*
         * Generates a program tree, where the root is a function node
         */
        public FunctionNode<T> CreateFunction<T>(int maxDepth) where T : IComparable
        {
            var chosenFactory = FunctionPicker.PickFactoryAs<T,IFunctionFactory>(Factories);

            return chosenFactory.CreateFunction<T>(maxDepth, this);
        }
        
        /*
         * Selects a random comparator node from those available
         */
        public NodeComparator<T> ChooseComparator<T>(int maxDepth) where T : IComparable
        {
            var chosenComparatorFactory = FunctionPicker.PickFactoryAs<T,ComparatorFactory>(Comparators);
            
            return (NodeComparator<T>)chosenComparatorFactory.CreateFunction<T>(maxDepth, this);
        }

        /*
         * Returns a node, either terminal or functional. If terminal, a terminal node is chosen and returned, otherwise
         * a function node is selected and the elements of that function are filled in recursively 
         */
        public INode<T> Choose<T>(int maxDepth) where T : IComparable
        {
            if (maxDepth <= 0) return GetTerminal<T>();
            
            var terminalOrFunction = RandomNumberFactory.AboveThreshold(_settings.TerminalChance);

            return terminalOrFunction switch
            {
                false => GetTerminal<T>(),
                true => CreateFunction<T>(maxDepth)
            };
        }

        /*
         * Selects a random terminal node, matching the target T
         */
        public INode<T> GetTerminal<T>() where T : IComparable
        {
            if (_useNullTerminals) return ValueNodeFactory.GetNull<T>();

            var chosenFactory = FunctionPicker.PickFactoryAs<T,IValueNodeFactory>(ValueNodeFactories);
            
            return chosenFactory.Get<T>();
        }
        
        /*
         * Generates a program tree from the provided ID
         */
        public INode<T> GenerateFunctionFromId<T>(string id) where T : IComparable
        {
            id = AdfParser.GetIdWithoutDelimiters(id);

            if (id.StartsWith(NodeCategory.FunctionDefinition))
                return FunctionFactory.GenerateDefinitionFromId<T>(id,this);

            var generator = Factories.FirstOrDefault(f => f.CanMap(id));
            
            if (generator != null) return generator.GenerateFunctionFromId<T>(id, this);

            var terminalGenerator = ValueNodeFactories.FirstOrDefault(f => f.CanMap(id));

            if (terminalGenerator != null) return terminalGenerator.GenerateFunctionFromId<T>(id,this);
            //Use comparator
            generator = Comparators.FirstOrDefault(f => f.CanMap(id));
            
            if(generator == null)
                throw new Exception($"Unable to find generator for ID '{id}'");
            
            return generator.GenerateFunctionFromId<T>(id,this);
        }

        public INode<T> GenerateChildFromId<T>(ref string id) where T : IComparable
        {
            var child = GenerateFunctionFromId<T>(id);
            var childId = child.GetId();
            
            var remainingId = GetIdAfter(id,childId);
            
            id = remainingId;
            
            return child;
        }

        private static string GetIdAfter(string id, string afterSubstring)
        {
            var afterSubstringWithoutDelimiters = AdfParser.GetIdWithoutDelimiters(afterSubstring);
            return id[(id.IndexOf(afterSubstringWithoutDelimiters, StringComparison.Ordinal) + afterSubstringWithoutDelimiters.Length)..];
        }
        
    }
}
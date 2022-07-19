using lib.AutomaticallyDefinedFunctions.factories.comparators;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.factories.valueNodes;
using lib.AutomaticallyDefinedFunctions.generators.adf;

namespace lib.AutomaticallyDefinedFunctions.generators
{
    /*
     * A factory capable of generating program trees. Primarily wraps around AdfSettings to allow for more modularity
     */
    public abstract class FactoryManager
    {
        protected List<IFunctionFactory> Factories;
        protected List<ComparatorFactory> Comparators;
        protected List<IValueNodeFactory> ValueNodeFactories;

        private AdfSettings _settings;

        protected FactoryManager(AdfSettings settings)
        {
            Factories = settings.Factories;
            Comparators = settings.Comparators;
            ValueNodeFactories = settings.ValueNodeFactories;

            _settings = settings;
        }
        
        public FactoryManager UseFactory(IFunctionFactory factory)
        {
            Factories.Add(factory);
            return this;
        }
        
        public FactoryManager UseComparator(ComparatorFactory factory)
        {
            Comparators.Add(factory);
            return this;
        }
        
        public FactoryManager UseValueNodeFactory(IValueNodeFactory factory)
        {
            ValueNodeFactories.Add(factory);
            return this;
        }
        
        public FactoryManager ClearFactories()
        {
            Factories.Clear();
            Comparators.Clear();
            ValueNodeFactories.Clear();
            return this;
        }
        
        private void SetFactories()
        {
            Factories = _settings.Factories;
            Comparators = _settings.Comparators;
            ValueNodeFactories = _settings.ValueNodeFactories;
        }
        
        public void Reset()
        {
            ClearFactories();
            SetFactories();
        }
    }
}
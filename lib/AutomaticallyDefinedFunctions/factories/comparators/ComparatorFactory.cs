using lib.AutomaticallyDefinedFunctions.factories.functionFactories;

namespace lib.AutomaticallyDefinedFunctions.factories.comparators
{
    public abstract class ComparatorFactory : FunctionFactory
    {
        protected ComparatorFactory(string symbol) : base(symbol) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(bool);
        }

    }
}
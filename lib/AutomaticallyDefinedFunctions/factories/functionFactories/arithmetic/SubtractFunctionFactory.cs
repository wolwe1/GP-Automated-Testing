using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.subtractFunction;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class SubtractFunctionFactory : ArithmeticFunctionFactory
    {
        public SubtractFunctionFactory(): base(NodeCategory.Subtract){}

        private static SubtractFunc<T> CreateSubtractFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(double))
            {
                return (SubtractFunc<T>) (object) new SubtractFunc<double>(new NumericSubtractFunc());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateSubtractFunction<T>();
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }
    }
}
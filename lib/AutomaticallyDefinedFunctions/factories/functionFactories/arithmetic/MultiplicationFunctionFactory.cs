using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.multiplicationFunction;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class MultiplicationFunctionFactory : ArithmeticFunctionFactory
    {
        public MultiplicationFunctionFactory(): base(NodeCategory.Multiplication){}

        private static MultiplicationFunc<T> CreateMultiplicationFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(double))
            {
                return (MultiplicationFunc<T>) (object) new MultiplicationFunc<double>(new NumericMultiplicationFunc());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateMultiplicationFunction<T>();
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }
    }
}
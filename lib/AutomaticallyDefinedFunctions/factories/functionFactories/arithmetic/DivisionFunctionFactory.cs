using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.divisionFunction;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class DivisionFunctionFactory : ArithmeticFunctionFactory
    {
        public DivisionFunctionFactory(): base(NodeCategory.Division){}

        private static DivisionFunc<T> CreateDivisionFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(double))
            {
                return (DivisionFunc<T>) (object) new DivisionFunc<double>(new NumericDivisionFunc());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateDivisionFunction<T>();
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }
        
    }
}
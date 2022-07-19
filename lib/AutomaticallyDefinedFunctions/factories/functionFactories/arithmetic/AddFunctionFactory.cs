using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class AddFunctionFactory : ArithmeticFunctionFactory
    {
        public AddFunctionFactory(): base(NodeCategory.Add){}
        public static AddFunc<T> CreateAddFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (AddFunc<T>) (object) new AddFunc<string>(new StringAddOperator());
            }

            if (typeof(T) == typeof(double))
            {
                return (AddFunc<T>) (object) new AddFunc<double>(new NumericAddOperator());
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (AddFunc<T>) (object) new AddFunc<bool>(new BooleanAddOperator());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }

        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateAddFunction<T>();
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double) || typeof(T) == typeof(string) || typeof(T) == typeof(bool);
        }

    }
}
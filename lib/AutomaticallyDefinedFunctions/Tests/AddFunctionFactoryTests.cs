using lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using Xunit;

namespace lib.AutomaticallyDefinedFunctions.Tests
{
    public class AddFunctionFactoryTests
    {
        [Fact]
        public void StringCallGeneratesStringAdder()
        {
            var adder = AddFunctionFactory.CreateAddFunction<string>();
            var adder2 = AddFunctionFactory.CreateAddFunction<double>();
            var adder3 = AddFunctionFactory.CreateAddFunction<bool>();

            Assert.Equal(typeof(AddFunc<string>),adder.GetType());
            Assert.Equal(typeof(AddFunc<double>),adder2.GetType());
            Assert.Equal(typeof(AddFunc<bool>),adder3.GetType());
        }
    }
}
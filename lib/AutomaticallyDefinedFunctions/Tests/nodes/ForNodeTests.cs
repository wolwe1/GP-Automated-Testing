using lib.AutomaticallyDefinedFunctions.exceptions;
using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.functions.forLoop;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using Xunit;

namespace lib.AutomaticallyDefinedFunctions.Tests.nodes
{
    public class ForNodeTests
    {
        [Theory]
        [MemberData(nameof(InvalidForLoopCombinations))]
        public void IFFunctionOnlyValidWithAllProperties<T>(ForLoopNode<T,double> loop ) where T : IComparable
        {
            Assert.Throws<InvalidStructureException>(() => loop.GetValue());
        }
        
        [Theory]
        [MemberData(nameof(LoopFunctionCombinations))]
        public void LoopOutputsCorrectly<T>(ForLoopNode<T,double> loop, T expectedResult ) where T : IComparable
        {
            Assert.Equal(expectedResult,loop.GetValue());
        }
        
        public static IEnumerable<object[]> LoopFunctionCombinations()
        {
            var stringLoop = NodeBuilder.CreateSimpleForLoop(3d, "a");
            var numberLoop = NodeBuilder.CreateSimpleForLoop(3d, 1d);
            var booleanLoop = NodeBuilder.CreateSimpleForLoop(3d, true);
            
            yield return new object[] {stringLoop,"aaa"};
            
            yield return new object[] {numberLoop,3};
            
            yield return new object[] {booleanLoop,true};
        }
        
        public static IEnumerable<object[]> InvalidForLoopCombinations()
        {
            var counter = new ValueNode<double>(0);
            var increment = new ValueNode<double>(1);
            var bounds = new ValueNode<double>(10);
            var comparator = new LessThanComparator<double>(counter,bounds);

            var func = NodeBuilder.CreateForLoop<string,double>(null, null, null);
            var func2 = NodeBuilder.CreateForLoop<string,double>( increment,null, null);
            var func3 = NodeBuilder.CreateForLoop<string,double>( increment,comparator, null);

            yield return new object[] {func};
            
            yield return new object[] {func2};
            
            yield return new object[] {func3};
            
        }
    }
}
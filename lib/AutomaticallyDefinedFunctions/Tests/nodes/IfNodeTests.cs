using lib.AutomaticallyDefinedFunctions.factories;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using Xunit;

namespace lib.AutomaticallyDefinedFunctions.Tests.nodes
{
    public class IfNodeTests
    {

        [Theory]
        [MemberData(nameof(ValueFunctionCombinations))]
        public void IfFunctionsTriggerCorrectBranch<T, TU>(IfNode<T,TU> ifStatement, T expectedResult ) where T : IComparable where TU : IComparable
        {
            Assert.Equal(expectedResult, ifStatement.GetValue());
        }

        [Theory]
        [MemberData(nameof(EqualityCombinations))]
        public void ComparatorsChainUsingOr(string expectedValue,IfNode<string,double> ifStatement,NodeComparator<double> comparator)
        {
            Assert.True(expectedValue == ifStatement.SetComparisonOperator(comparator).GetValue());
        }

        public static IEnumerable<object[]> EqualityCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);

            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>(smaller,bigger);
            var lessThan = new LessThanComparator<double>(smaller,bigger);
            var greaterThan = new GreaterThanComparator<double>(smaller,bigger);
            
            var lessThanOrEquals = lessThan.SetAdditionalComparator(equals);
            var greaterThanOrEquals = greaterThan.SetAdditionalComparator(equals);
            
            var ifStatement = NodeBuilder
                .CreateIfStatement(trueStatement, falseStatement, equals);

            yield return new object[] {falseStatement.GetValue(),ifStatement,equals };
            
            yield return new object[] {trueStatement.GetValue(),ifStatement,lessThan };
            
            yield return new object[] {falseStatement.GetValue(),ifStatement,greaterThan };
            
            yield return new object[] {trueStatement.GetValue(),ifStatement,lessThanOrEquals };

            yield return new object[] {falseStatement.GetValue(),ifStatement,greaterThanOrEquals };
            
        }

        [Fact]
        public void ComparatorsReturnTrueIfChainedWithEqualsWhenBranchesAreEqual()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 

            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>(smaller,smaller);
            var lessThan = new LessThanComparator<double>(smaller,smaller);
            var greaterThan = new GreaterThanComparator<double>(smaller,smaller);
            var lessThanOrEquals = lessThan.SetAdditionalComparator(equals);
            var greaterThanOrEquals = lessThan.SetAdditionalComparator(equals);
            
            var ifStatement = NodeBuilder
                .CreateIfStatement(trueStatement, falseStatement, lessThanOrEquals);
            
            Assert.Equal(trueStatement.GetValue(),ifStatement.GetValue());
            ifStatement.SetComparisonOperator(greaterThanOrEquals);
            Assert.Equal(trueStatement.GetValue(),ifStatement.GetValue());

        }
        
        public static IEnumerable<object[]> ValueFunctionCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new EqualsComparator<double>(smaller,bigger))
                ,bigger.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new EqualsComparator<double>(bigger,smaller))
                ,smaller.GetValue()};
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new GreaterThanComparator<double>(smaller,bigger))
                ,bigger.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new GreaterThanComparator<double>(bigger,smaller))
                ,bigger.GetValue()};
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new LessThanComparator<double>(smaller,bigger))
                ,smaller.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new LessThanComparator<double>(bigger,smaller))
                ,smaller.GetValue()};
        }

        private static IfNode<double,double> CreateIfStatement(INode<double> firstAndTrue,INode<double> secondAndFalse,NodeComparator<double> op)
        {
            return NodeBuilder.CreateIfStatement(firstAndTrue, secondAndFalse, op);
        }
        
        [Theory]
        [MemberData(nameof(InvalidIfFunctionCombinations))]
        public void IFFunctionOnlyValidWithAllProperties<T, U>(IfNode<T,U> ifStatement, bool expectedResult ) where T : IComparable where U : IComparable
        {
            Assert.Equal(expectedResult, ifStatement.IsValid());
        }
        
        public static IEnumerable<object[]> InvalidIfFunctionCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);
            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>(smaller,bigger);

            var func1 = NodeBuilder.CreateIfStatement<string,double>( null, null, null);
            var func2 = NodeBuilder.CreateIfStatement<string,double>( trueStatement, null, null);
            var func3 = NodeBuilder.CreateIfStatement<string,double>( trueStatement, falseStatement, null);
            var func4 = NodeBuilder.CreateIfStatement<string,double>( trueStatement, falseStatement, equals);

            yield return new object[] {func1,false};
            
            yield return new object[] {func2,false};
            
            yield return new object[] {func3,false};
            
            yield return new object[] {func4,true};
            
        }

    }
}
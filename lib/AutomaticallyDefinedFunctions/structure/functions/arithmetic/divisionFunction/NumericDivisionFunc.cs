using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.divisionFunction
{
    public class NumericDivisionFunc : IArithmeticOperator<double>
    {
        public double GetResult(List<INode> children)
        {
            var firstVal = ((INode<double>)children[0]).GetValue();
            var secondVal = ((INode<double>)children[1]).GetValue();

            if (secondVal == 0)
                return 0;

            return firstVal / secondVal;
        }
    }
}
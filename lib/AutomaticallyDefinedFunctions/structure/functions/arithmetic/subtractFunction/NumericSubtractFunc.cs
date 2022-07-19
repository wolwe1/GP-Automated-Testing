using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.subtractFunction
{
    public class NumericSubtractFunc : IArithmeticOperator<double>
    {
        public double GetResult(List<INode> children)
        {
            return ((INode<double>)children[0]).GetValue() - ((INode<double>)children[1]).GetValue();
        }
    }
}
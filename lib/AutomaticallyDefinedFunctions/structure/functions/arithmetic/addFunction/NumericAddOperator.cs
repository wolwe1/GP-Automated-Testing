using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction
{
    public class NumericAddOperator : IArithmeticOperator<double> 
    {
        public double GetResult(List<INode> children)
        {
            return children.Sum( (child) => ((INode<double>)child).GetValue());
        }
    }
}
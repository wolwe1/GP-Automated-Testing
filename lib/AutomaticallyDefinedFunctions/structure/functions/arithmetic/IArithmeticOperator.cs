using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic
{
    public interface IArithmeticOperator<T> where T : IComparable
    {
        T GetResult(List<INode> children) ;
    }
}
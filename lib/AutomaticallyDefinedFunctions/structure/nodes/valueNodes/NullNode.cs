using lib.AutomaticallyDefinedFunctions.generators;

namespace lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes
{
    /// <summary>
    /// The null node is intended to be used as a placeholder node for structural purposes, not for execution
    /// </summary>
    /// <typeparam name="T">The type of the intended value node</typeparam>
    public class NullNode<T> : ValueNode<T> where T : IComparable
    {
        public override T GetValue()
        {
            throw new Exception("Null node was executed");
        }

        public override bool IsValid()
        {
            return false;
        }
        
        public override int GetNullNodeCount()
        {
            return 1;
        }

        public override bool IsNullNode()
        {
            return true;
        }
        
        public override string GetId()
        {
            return "V['Null']";
        }
        
        public override INode<T> GetCopy()
        {
            return new NullNode<T>();
        }
        
        public override INode<T> ReplaceNode(int nodeIndexToReplace, FunctionCreator creator, int maxDepth)
        {
            return this;
        }
    }
}
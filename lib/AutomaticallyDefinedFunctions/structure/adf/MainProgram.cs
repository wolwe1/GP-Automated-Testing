using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.visitors;

namespace lib.AutomaticallyDefinedFunctions.structure.adf
{
    /*
     * A main results producing tree
     */
    public class MainProgram<T> where T : IComparable
    {
        private readonly INode<T> _nodeTree;

        public MainProgram(INode<T> nodeTree)
        {
            _nodeTree = nodeTree;
        }

        /*
         * Run the program and retrieve the raw T output
         */
        public T GetValue()
        {
            return _nodeTree.GetValue();
        }

        public bool IsValid()
        {
            return _nodeTree != null && _nodeTree.IsValid();
        }
        
        public string GetId()
        {
            return $"Main{_nodeTree.GetId()}";
        }

        public MainProgram<T> GetCopy()
        {
            return new MainProgram<T>(_nodeTree.GetCopy());
        }

        public int GetNodeCount()
        {
            return _nodeTree.GetNodeCount();
        }

        public void Visit(INodeVisitor visitor)
        {
            _nodeTree.Visit(visitor);
        }
        
    }
}
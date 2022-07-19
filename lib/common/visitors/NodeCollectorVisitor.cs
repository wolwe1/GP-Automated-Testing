using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.visitors;

namespace lib.common.visitors
{
    public class NodeCollectorVisitor<T> : INodeVisitor where T : IComparable
    {
        private readonly List<INode<T>> _matchingNodes;

        public NodeCollectorVisitor()
        {
            _matchingNodes = new List<INode<T>>();
        }

        public void Accept(INode node)
        {
            if (node is INode<T> tNode)
                _matchingNodes.Add(tNode);
        }

        public List<INode<T>> GetNodes()
        {
            return _matchingNodes;
        }

        public bool WantsToVisit()
        {
            return true;
        }
    }
}
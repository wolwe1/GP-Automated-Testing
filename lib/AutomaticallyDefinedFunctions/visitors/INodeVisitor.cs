using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.visitors
{
    public interface INodeVisitor
    {
        public void Accept(INode node);
        bool WantsToVisit();
    }
}
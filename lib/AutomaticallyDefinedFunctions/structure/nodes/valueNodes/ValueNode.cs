using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.visitors;

namespace lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes
{
    public class ValueNode<T> : INode<T> where T : IComparable
    {
        protected T Value;
        public INode Parent { get; set; }

        public ValueNode(T value)
        {
            Value = value;
        }
        
        protected ValueNode()
        {
            Value = default(T);
        }

        public virtual T GetValue()
        {
            return Value;
        }

        public virtual bool IsValid()
        {
            return Value != null;
        }

        public virtual int GetNullNodeCount()
        {
            return 0;
        }

        public virtual INode<T> GetCopy()
        {
            return new ValueNode<T>(Value);
        }

        public INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            return GetCopy();
        }

        public virtual bool IsNullNode()
        {
            return false;
        }
        
        public virtual string GetId()
        {
            return $"{NodeCategory.ValueNode}['{Value}']";
        }

        public int GetNodeCount() => 1;

        public virtual INode ReplaceNode(int nodeIndexToReplace, FunctionCreator creator, int maxDepth)
        {
            return creator.CreateFunction<T>(maxDepth);
        }
        
        
        public virtual void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);
            
        }
        
    }
}
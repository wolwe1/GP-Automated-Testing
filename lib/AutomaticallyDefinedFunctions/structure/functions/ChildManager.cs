using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.visitors;

namespace lib.AutomaticallyDefinedFunctions.structure.functions
{
    public abstract class ChildManager : INode
    {
        protected readonly List<INode> Children;
        private readonly int _expectedChildrenAmount;

        public INode Parent { get; set; }
        private ChildManager()
        {
            Children = new List<INode>();
        }

        protected ChildManager(IEnumerable<INode> nodes): this()
        {
            foreach (var node in nodes)
                AddChild(node);
            
     
            _expectedChildrenAmount = Children.Count;
        }

        protected ChildManager(int expectedChildrenAmount) : this()
        {
            _expectedChildrenAmount = expectedChildrenAmount;
        }

        public int GetChildCount()
        {
            return Children.Count;
        }
        
        public INode GetChild(int index)
        {
            return index >= GetChildCount() ? null : Children[index];
        }

        public void AddChild(INode newNode)
        {
            if(newNode is not null)
                newNode.Parent = this;
            Children.Add(newNode);
        }

        public int GetNullNodeCount()
        {
            return Children.Sum(x => x.GetNullNodeCount());
        }

        public int GetNodeCount()
        {
            return Children.Sum(child => child.GetNodeCount());
        }

        public bool IsValid()
        {
            return Children.Count == _expectedChildrenAmount && Children.All(child => child != null && child.IsValid());
        }

        public INode<T> GetChildAs<T>(int index) where T : IComparable
        {
            //Enables property based getters without throwing
            if (index >= Children.Count)
                return null;
            
            return Children[index] as INode<T>;
        }
        
        protected INode<T> GetChildCopyAs<T>(int index) where T : IComparable
        {
            var childAs = GetChildAs<T>(index);

            return childAs?.GetCopy();
        }

        protected void RegisterChildren(IEnumerable<INode> nodes)
        {
            foreach (var node in nodes)
                AddChild(node);
        }
        
        public void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);

            for (var i = 0; i < Children.Count; i++)
            {
                if(visitor.WantsToVisit())
                    Children[i].Visit(visitor);
            }
            
        }

        public void SetChild(INode nodeToReplace, INode newNode)
        {
            for (var index = 0; index < Children.Count; index++)
            {
                var child = Children[index];
                if (child.Equals(nodeToReplace))
                {
                    Children[index] = newNode;
                    Children[index].Parent = this;
                    break;
                }
            }
        }
        
        public abstract bool IsNullNode();
        public abstract string GetId();
        
    }
}
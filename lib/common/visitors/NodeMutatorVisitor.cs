using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.visitors;
using lib.common.connectors.ga;
using lib.GeneticAlgorithm.source.core.population;

namespace lib.common.visitors
{
    public class NodeMutatorVisitor<T> : INodeVisitor where T : IComparable
    {
        private int _nodeCount;
        private readonly int _targetNodeCount;

        private readonly FunctionCreator _funcCreator;
        private readonly int _maxModificationDepth;
        private bool _replaced;
        private bool _hasReplacedTree;

        public NodeMutatorVisitor(int targetNodeCount, IPopulationGenerator<T> generator, int maxModificationDepth)
        {
            _nodeCount = 0;
            _targetNodeCount = targetNodeCount;
            _funcCreator = ( (AdfPopulationGenerator<T>) generator).GetFunctionCreator();
            _maxModificationDepth = maxModificationDepth;
            _replaced = false;
        }

        public void Accept(INode node)
        {
            if (_replaced) return;

            if (_nodeCount == _targetNodeCount)
            {
                ReplaceNodeParent(node);
                _replaced = true;
                _hasReplacedTree = true;
            }

            _nodeCount++;
        }

        public bool WantsToVisit()
        {
            return !_hasReplacedTree;
        }

        private void ReplaceNodeParent(INode node)
        {
            //Parent should always be a function node, since a value node cannot have children
            var parent = (ChildManager)node.Parent;
            var newChild = CreateMatchingTypeSubtree(node);
            parent.SetChild(node, newChild);

        }

        private INode CreateMatchingTypeSubtree(INode node)
        {
            return node switch
            {
                INode<string> strNode => CreateNewSubtree(strNode),
                INode<double> doubleNode => CreateNewSubtree(doubleNode),
                INode<bool> boolNode => CreateNewSubtree(boolNode),
                _ => throw new Exception($"Could not create mutated tree for type {node.GetType()}")
            };
        }
        
        private INode CreateNewSubtree<TX>(INode<TX> node) where TX : IComparable
        {
            //Edge case
            if (node is NodeComparator<TX>)
                return _funcCreator.ChooseComparator<TX>(_maxModificationDepth);

            return _funcCreator.Choose<TX>(_maxModificationDepth);
        }
    }
}
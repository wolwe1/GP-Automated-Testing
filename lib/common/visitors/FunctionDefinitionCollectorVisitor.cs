using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.visitors;

namespace lib.common.visitors
{
    public class FunctionDefinitionCollectorVisitor<T> : INodeVisitor where T : IComparable
    {
        private readonly string _targetFunctionName;
        private FunctionDefinition<T> _definition;
        private bool _foundTarget;

        public FunctionDefinitionCollectorVisitor(string targetFunctionName)
        {
            _targetFunctionName = targetFunctionName;
        }

        public void Accept(INode node)
        {
            if (node is not FunctionDefinition<T> definition) return;
            
            if (definition.GetName() != _targetFunctionName) return;
            
            
            _definition = definition;
            _foundTarget = true;
        }

        public bool WantsToVisit()
        {
            return !_foundTarget;
        }

        public FunctionDefinition<T> GetDefinition()
        {
            return _definition;
        }
    }
}
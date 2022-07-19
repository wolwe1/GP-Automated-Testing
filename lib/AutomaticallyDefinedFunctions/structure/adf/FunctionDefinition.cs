using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.adf
{
    /*
     * A function definition
     */
    public class FunctionDefinition<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _name;
        private readonly INode<T> _function;

        public FunctionDefinition(string name,INode<T> function): base(1)
        {
            _name = name;
            _function = function;
            RegisterChildren(new List<INode>(){_function});
        }

        /*
         * Create an empty function with a name
         */
        public static FunctionDefinition<T> Create(string name)
        {
            return new FunctionDefinition<T>(name,null);    
        }

        /*
         * Set the program tree of the function
         */
        public FunctionDefinition<T> UseFunction(INode<T> function)
        {
            return new FunctionDefinition<T>(_name, function);
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.FunctionDefinition);
        }
        
        public override T GetValue()
        {
            return _function.GetValue();
        }
        
        public override INode<T> GetCopy()
        {
            return new FunctionDefinition<T>(_name, _function.GetCopy());
        }

        /*
         * When functions are created, their terminal values are [null] nodes, marking them for replacement by actual arguments.
         * This function will replace all these nodes using the function creator
         */
        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            return new FunctionDefinition<T>(_name, _function.ReplaceNullNodes(maxDepth,creator));
        }

        public string GetName()
        {
            return _name;
        }
        
    }
}
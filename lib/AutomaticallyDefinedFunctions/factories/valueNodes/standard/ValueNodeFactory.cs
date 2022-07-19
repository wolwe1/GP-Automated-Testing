using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard
{
    public class ValueNodeFactory : FunctionFactory, IValueNodeFactory
    {
        public ValueNodeFactory(): base(NodeCategory.ValueNode){}

        private static INode<T> Get<T>(string id) where T : IComparable
        {
            if (id.StartsWith("Null"))
                return new NullNode<T>();
            
            if (typeof(T) == typeof(string))
            {
                return (ValueNode<T>) (object) StringValueNodeFactory.Get(id);
            }

            if (typeof(T) == typeof(double))
            {
                return (ValueNode<T>) (object) DoubleValueNodeFactory.Get(id);
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (ValueNode<T>) (object) BooleanValueNodeFactory.Get(id);
            }
            
            throw new InvalidOperationException($"Unable to generate value node of type {typeof(T)}");
        }

        public static INode<T> GetNull<T>() where T : IComparable
        {
            return new NullNode<T>();
        }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            throw new NotImplementedException();
        }
        
        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return Get<T>(AdfParser.GetValueFromQuotes(id));
        }
        
        public new INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable
        {
            return Get<T>(AdfParser.GetValueFromQuotes(id[1..]));
        }

        public static INode<T> GetT<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (ValueNode<T>) (object) StringValueNodeFactory.Get();
            }

            if (typeof(T) == typeof(double))
            {
                return (ValueNode<T>) (object) DoubleValueNodeFactory.Get();
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (ValueNode<T>) (object) BooleanValueNodeFactory.Get();
            }

            throw new Exception($"Value node factory could not dispatch for type {typeof(T)}");
        }

        public INode<T> Get<T>() where T : IComparable
        {
            return GetT<T>();
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string) || typeof(T) == typeof(bool) || typeof(T) == typeof(double);
        }
        
    }
}
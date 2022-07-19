using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.other;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.operators
{
    public class LengthOfFunctionFactory : FunctionFactory
    {
        public LengthOfFunctionFactory() : base(NodeCategory.LengthOf) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                return CreateFunction<T, T>(maxDepth, parent);
            }
            
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => CreateFunction<T, string>(maxDepth, parent),
                1 => CreateFunction<T, double>(maxDepth, parent),
                2 => CreateFunction<T, bool>(maxDepth, parent),
                _ => throw new Exception($"Could not dispatch type {typeof(T)}")
            };
            //Sus boxing but T will be double
            
        }

        private FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent) where TU : IComparable where T : IComparable
        {
            return (FunctionNode<T>) (object) new LengthOfNode<TU>(parent.Choose<TU>(maxDepth - 1));
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }
        
        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return (FunctionNode<T>) (object) new LengthOfNode<TU>(functionCreator.GenerateChildFromId<TU>(ref id)); 
        }
    }
}
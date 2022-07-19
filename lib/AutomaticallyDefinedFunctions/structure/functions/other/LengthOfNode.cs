using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.other
{
    public class LengthOfNode<T> : FunctionNode<double> where T : IComparable
    {
        private INode<T> Argument => GetChildAs<T>(0);
        public LengthOfNode() : base(1) { }

        public LengthOfNode(INode<T> argument) : this()
        {
            RegisterChildren(new List<INode>(){argument});
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.LengthOf);
        }

        public override INode<double> GetCopy()
        {
            return new LengthOfNode<T>(GetChildCopyAs<T>(0));
        }

        public override INode<double> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            return new LengthOfNode<T>((INode<T>) ReplaceNullNodesForComponent(Argument,maxDepth - 1,creator));
        }

        public override double GetValue()
        {
            var typeofT = typeof(T);
            
            if (typeofT == typeof(string) )
            {
                var str = (string) (object) Argument.GetValue();
                
                return str?.Length ?? 0;
            }
            
            if (typeofT == typeof(bool) )
            {
                return 1;
            }
            
            if (typeofT == typeof(double) )
            {
                var number = (double) (object) Argument.GetValue();
                return GetLengthOfNumber(number);
            }

            throw new Exception();
        }

        private static double GetLengthOfNumber(double number)
        {
            var digitCount = 0;

            while (number >= 1)
            {
                number /= 10;
                digitCount++;
            }

            return digitCount;
        }
    }
}
using lib.AutomaticallyDefinedFunctions.exceptions;
using lib.AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.forLoop
{
    public class ForLoopNode<T,TU>: FunctionNode<T> where T : IComparable where TU : IComparable
    {
        private readonly AddFunc<TU> _incrementalAdd;
        private readonly AddFunc<T> _resultsAdd;
        
        private INode<TU> Increment => GetChildAs<TU>(0);
        private INode<T> CodeBlock => GetChildAs<T>(2);
        private NodeComparator<TU> Comparator => (NodeComparator<TU>) GetChildAs<TU>(1);
        
        public ForLoopNode() : base(3)
        {
            _incrementalAdd = AddFunctionFactory.CreateAddFunction<TU>();
            _resultsAdd =  AddFunctionFactory.CreateAddFunction<T>();
        }

        private ForLoopNode(INode<TU> incremental, NodeComparator<TU> comparator, INode<T> block): this()
        {
            RegisterChildren( new List<INode> {incremental,comparator,block});
        }

        public override string GetId()
        {
            return CreateId<TU>(NodeCategory.Loop);
        }
        
        public override T GetValue()
        {
            //if (!IsValid()) throw new InvalidStructureException("For loop function is invalid");
            
            var comparator = (NodeComparator<TU>)Comparator.GetCopy();

            var counter = comparator.GetChildAs<TU>(0);

            return Loop(comparator,counter);
            
        }

        private T Loop(NodeComparator<TU> comparator, INode<TU> counter)
        {
            _incrementalAdd.Refresh(counter,Increment);
            _resultsAdd.Clear();
            var safetyBreak = 0;
            while (comparator.PassesCheck())
            {
                //Repeat block
                _resultsAdd.AddChild(new ValueNode<T>(CodeBlock.GetValue()));
                
                //Increment counter
                counter = new ValueNode<TU>(_incrementalAdd.GetValue());
                _incrementalAdd.Refresh(counter,Increment);

                //Set the updated counter in the comparator
                comparator.SetPredicate(0,counter);
                safetyBreak++;

                if (safetyBreak == 250)
                    throw new ProgramLoopException(comparator.GetChild(0).GetId(), comparator.GetId(), comparator.GetChild(1).GetId());
            }

            return _resultsAdd.GetValue();
        }

        public override INode<T> GetCopy()
        {
            return new ForLoopNode<T, TU>(Increment.GetCopy(),(NodeComparator<TU>) Comparator.GetCopy(),CodeBlock.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var newIncremental = ReplaceNullNodesForComponent(Increment,maxDepth - 1,creator);
            var newBlock = ReplaceNullNodesForComponent(CodeBlock,maxDepth - 1,creator);
            var newComparator = ReplaceNullNodesForComponent(Comparator,maxDepth - 1,creator);

            return new ForLoopNode<T, TU>((INode<TU>) newIncremental, (NodeComparator<TU>) newComparator,(INode<T>) newBlock);
        }
        
        public ForLoopNode<T,TU> SetIncrement(INode<TU> incremental)
        {
            return new ForLoopNode<T, TU>(incremental,(NodeComparator<TU>) Comparator?.GetCopy(),CodeBlock?.GetCopy());
        }

        public ForLoopNode<T,TU> SetCodeBlock(INode<T> block)
        {
            return new ForLoopNode<T, TU>(Increment?.GetCopy(),(NodeComparator<TU>) Comparator?.GetCopy(),block);
        }

        public ForLoopNode<T,TU> SetComparator(NodeComparator<TU> comparator)
        {
            return new ForLoopNode<T, TU>(Increment?.GetCopy(),comparator,CodeBlock?.GetCopy());

        }
    }
}
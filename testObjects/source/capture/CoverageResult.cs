namespace testObjects.source.capture
{
    public class CoverageResult<T> : CoverageResultWrapper
    {

        public CoverageResult(string className, string functionName,int numberNodes) : base(className,functionName,numberNodes)
        {
            ResultType = typeof(T);
        }
    
        private void AddCoverage(int nodeNumber,int edgeNumber,NodeType nodeType,bool isStart,bool isEnd)
        {
            Coverages.Add(new Coverage(nodeNumber,edgeNumber,nodeType,isStart,isEnd));
        }

        public void AddStartNode(NodeType nodeType)
        {
            AddCoverage(0,-1,nodeType,true,false);
        }
    
        public void AddNode(int nodeNumber,NodeType nodeType)
        {
            AddCoverage(nodeNumber,nodeNumber - 1,nodeType,false,false);
        }
    
        public void AddEndNode(int nodeNumber,NodeType nodeType)
        {
            AddCoverage(nodeNumber,nodeNumber - 1,nodeType,false,true);
        }
    

        public CoverageResult<T> SetResult(T result)
        {
            Result = result;
            return this;
        }

        public CoverageResult<T> Merge(CoverageResult<T> other)
        {
            Coverages.AddRange(other.Coverages);
            return this;
        }
    }

    public enum NodeType
    {
        Statement,
        Loop,
        If,
        Return,
        FunctionCall
    }
}



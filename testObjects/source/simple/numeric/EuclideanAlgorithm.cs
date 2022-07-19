using testObjects.source.capture;

namespace testObjects.source.simple.numeric
{
    public class EuclideanAlgorithm
    {

        public CoverageResult<double> Get(int a, int b)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<double>("EuclideanAlgorithm","Get",5);
            
            coverage.AddStartNode(NodeType.Loop);
            while (b != 0)
            {
                coverage.AddNode(1,NodeType.Statement);
                var temp = b;
                coverage.AddNode(2,NodeType.Statement);
                b = a % b;
                coverage.AddNode(3,NodeType.Statement);
                a = temp;
            }
            coverage.AddEndNode(4,NodeType.Statement);
            return coverage.SetResult(a);
        }

        public CoverageResult<double> GetRecursive(int a, int b)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<double>("EuclideanAlgorithm","Get",3);
            
            coverage.AddStartNode(NodeType.If);
            if (b == 0)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(a);
            }
            coverage.AddNode(2,NodeType.Return);
            var recurseResult = GetRecursive(b, a % b);

            return coverage.Merge(recurseResult).SetResult(recurseResult.GetReturnValue()); 
        }
    }
}
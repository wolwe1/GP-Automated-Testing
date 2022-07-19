using testObjects.source.capture;

namespace testObjects.source.simple.numeric
{
    public class IsPrime
    {
        public CoverageResult<bool> Get(int n)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("IsPrime","Get",9);

            coverage.AddStartNode(NodeType.If);
            if (n <= 3)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(n > 1);
            }
            
            coverage.AddNode(2,NodeType.If);
            if(n % 2 == 0 || n % 3 == 0)
            {
                coverage.AddNode(3,NodeType.Return);
                return coverage.SetResult(false);
            }

            coverage.AddNode(4,NodeType.Statement);
            var i = 5;
            coverage.AddNode(4,NodeType.Loop);
            while (Math.Pow(i,2) <= n)
            {
                coverage.AddNode(5,NodeType.If);
                if(n % i == 0 || n % (i + 2) == 0)
                {
                    coverage.AddNode(6,NodeType.Return);
                    return coverage.SetResult(false);
                }
                coverage.AddNode(7,NodeType.Statement);
                i += 6;
            }
            
            coverage.AddNode(8,NodeType.Return);
            return coverage.SetResult(true);
        }
    }
}
using testObjects.source.capture;

namespace testObjects.source.simple.numeric
{
    public class BinomialCoefficient
    {
        public CoverageResult<double> Get(int n, int k)
        {
            return GetSafe(n, k, 0);
        }
        public CoverageResult<double> GetSafe(int n, int k,int callCount)
        {
            //Prevent blowing the stack
            if(callCount >= 5)
                return CoverageResultWrapper.SetupCoverage<double>("BinomialCoefficient","Get",6).SetResult(-1);

            
            var coverage = CoverageResultWrapper.SetupCoverage<double>("BinomialCoefficient","Get",6);
            
            coverage.AddStartNode(NodeType.If);
            // Base Cases
            if (k > n)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(0);
            }
            
            coverage.AddNode(2,NodeType.If);
            if (k == 0 || k == n)
            {
                coverage.AddNode(3,NodeType.Return);
                return coverage.SetResult(1);
            }
 
            // Recur
            coverage.AddNode(4,NodeType.Return);
            var nMinus1KMinus1 = GetSafe(n - 1, k - 1,++callCount);
            coverage.AddNode(5,NodeType.Return);
            var nMinus1K = GetSafe(n - 1, k,++callCount);

            return coverage.Merge(nMinus1KMinus1)
                .Merge(nMinus1K)
                .SetResult(nMinus1KMinus1.GetReturnValue() + nMinus1K.GetReturnValue());
        }
    }
}
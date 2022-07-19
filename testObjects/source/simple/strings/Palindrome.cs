using testObjects.source.capture;

namespace testObjects.source.simple.strings
{
    public class Palindrome
    {
        public CoverageResult<string> Get(string str)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("Palindrome","Get",4);
            
            //Be a nice user
            if (str.Length >= 500)
                return coverage.SetResult("");
            
            coverage.AddStartNode(NodeType.Statement);
            var palindrome = "";

            coverage.AddNode(1,NodeType.Loop);
            for (var i = str.Length - 1; i >= 0; i--)
            {
                coverage.AddNode(2,NodeType.Statement);
                palindrome += str[i];
            }

            coverage.AddEndNode(3,NodeType.Return);
            return coverage.SetResult(palindrome);
        }

        public CoverageResult<string> GetRecursive(string str)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<string>("Palindrome","GetRecursive",5);
            
            //Be a nice user
            if (str.Length >= 500)
                return coverage.SetResult("");
            
            coverage.AddStartNode(NodeType.If);
            if (str.Length == 0)
            {
                coverage.AddEndNode(1,NodeType.Return);
                return coverage.SetResult(str);
            }

            coverage.AddNode(2,NodeType.Statement);
            var tail = str[^1];
            coverage.AddNode(3,NodeType.Statement);
            var head = str.Substring(0, str.Length - 1);
            
            coverage.AddEndNode(4,NodeType.Return);
            var recursiveResult = GetRecursive(head);
            var mergedCoverage = coverage.Merge(recursiveResult);
            
            return mergedCoverage.SetResult(tail + recursiveResult.GetReturnValue());
        }
    }
}
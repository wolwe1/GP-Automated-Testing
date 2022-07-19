using testObjects.source.capture;

namespace testObjects.source.simple.strings
{
    public class Substring
    {
        //Function that decides whether the first string is equal, a subtype or not of the other
        public CoverageResult<bool> Get(string str, string strTwo)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("Substring","Get",5);

            coverage.AddStartNode(NodeType.If);
            if (str.Equals(strTwo))
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(true);
            }

            coverage.AddNode(2,NodeType.If);
            if (strTwo.Contains(str))
            {
                coverage.AddNode(3,NodeType.Return);
                return coverage.SetResult(true);
            }

            coverage.AddNode(4,NodeType.Return);
            return coverage.SetResult(false);
        }
    }
}
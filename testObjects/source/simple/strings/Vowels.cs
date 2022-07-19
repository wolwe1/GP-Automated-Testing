using testObjects.source.capture;

namespace testObjects.source.simple.strings
{
    public class Vowels
    {
        public CoverageResult<bool> Get(string str,string strTwo)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("Vowels","Get",10);
            
            coverage.AddStartNode(NodeType.If);
            if (str.Length == 0 || strTwo.Length == 0)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(false);
            }
            
            coverage.AddNode(2,NodeType.Statement);
            var vowels = new char[] {'a', 'e', 'i', 'o', 'u'};

            coverage.AddNode(3,NodeType.Loop);
            for (var i = 0; i < str.Length; i++)
            {
                coverage.AddNode(4,NodeType.If);
                if (vowels.Contains(str[i]))
                {
                    coverage.AddNode(5,NodeType.If);
                    return coverage.SetResult(true);
                }
            }
            
            coverage.AddNode(6,NodeType.Loop);
            for (var i = 0; i < strTwo.Length; i++)
            {
                coverage.AddNode(7,NodeType.If);
                if (vowels.Contains(strTwo[i]))
                {
                    coverage.AddNode(8,NodeType.Return);
                    return coverage.SetResult(true);
                }
            }

            coverage.AddEndNode(9,NodeType.Return);
            return coverage.SetResult(false);
        }
    }
}
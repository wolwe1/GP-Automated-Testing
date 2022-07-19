using testObjects.source.capture;

namespace testObjects.source.simple.strings
{
    public class Anagram
    {
        public CoverageResult<bool> Get(string stringOne, string stringTwo)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("Anagram","Get",16);
            
            coverage.AddStartNode(NodeType.Statement);
            stringOne = stringOne.ToLower().Replace(" ","");
            coverage.AddNode(1,NodeType.Statement);
            stringTwo = stringTwo.ToLower().Replace(" ","");
            
            coverage.AddNode(2,NodeType.If);
            if (stringOne.Length != stringTwo.Length)
            {
                coverage.AddEndNode(3,NodeType.Return);
                return coverage.SetResult(false);
            }
            
            coverage.AddNode(4,NodeType.FunctionCall);
            var firstStringCharacterSet = GetCharacterDictionary(stringOne,coverage);
            
            coverage.AddNode(11,NodeType.Loop);
            foreach (var character in stringTwo)
            {
                coverage.AddNode(12,NodeType.If);
                if (!firstStringCharacterSet.ContainsKey(character))
                {
                    coverage.AddEndNode(13,NodeType.Return);
                    return coverage.SetResult(false);
                }
                
                coverage.AddNode(14,NodeType.If);
                if (--firstStringCharacterSet[character] < 0)
                {
                    coverage.AddEndNode(15,NodeType.Return);
                    return coverage.SetResult(false);
                }
            }
            
            coverage.AddEndNode(16,NodeType.Return);
            return coverage.SetResult(true);
        }

        private static Dictionary<char, int> GetCharacterDictionary(string stringOne, CoverageResult<bool> coverage)
        {
            coverage.AddNode(5,NodeType.Statement);
            var firstStringCharacterSet = new Dictionary<char, int>();

            coverage.AddNode(6,NodeType.Loop);
            foreach (var character in stringOne)
            {
                coverage.AddNode(7,NodeType.If);
                if (firstStringCharacterSet.ContainsKey(character))
                {
                    coverage.AddNode(8,NodeType.Statement);
                    firstStringCharacterSet[character] += 1;
                }
                else
                {
                    coverage.AddNode(9,NodeType.Statement);
                    firstStringCharacterSet[character] = 1;
                }
                    
            }
            coverage.AddEndNode(10,NodeType.Return);
            return firstStringCharacterSet;
        }

        public CoverageResult<bool> GetRecursive(string str, string strTwo)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<bool>("Anagram","GetRecursive",16);
            
            coverage.AddStartNode(NodeType.If);
            if (str.Length == 0 && strTwo.Length == 0)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(true);
            }

            coverage.AddNode(2,NodeType.Statement);
            str = str.Replace(" ", "").ToLower();
            coverage.AddNode(3,NodeType.Statement);
            strTwo = strTwo.Replace(" ", "").ToLower();
            
            coverage.AddNode(4,NodeType.If);
            if (str.Length != strTwo.Length)
            {
                coverage.AddNode(5,NodeType.Return);
                return coverage.SetResult(false);
            }
            
            coverage.AddNode(6,NodeType.Statement);
            var strHead = str[0];
            coverage.AddNode(7,NodeType.Statement);
            var indexInOther = strTwo.IndexOf(strHead);

            coverage.AddNode(8,NodeType.If);
            if (indexInOther == -1)
            {
                coverage.AddNode(9,NodeType.Return);
                return coverage.SetResult(false);
            }

            coverage.AddNode(10,NodeType.Statement);
            var strTwoWithoutChar = strTwo.Remove(indexInOther, 1);

            var recursiveResult = GetRecursive(str[1..], strTwoWithoutChar);

            return coverage.Merge(recursiveResult).SetResult(recursiveResult.GetReturnValue());
        }
    }
}
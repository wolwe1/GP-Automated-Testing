using System.Text.RegularExpressions;

namespace lib.AutomaticallyDefinedFunctions.parsing
{
    public static class AdfParser
    {
        public static (string[], string[]) ParseAdfId(string adfId)
        {
            var content = adfId["ADF(".Length..^1];

            var contents = content.Split("*");

            var mainContent = contents[0].Length > 0 ? contents[0].Split(";") : Array.Empty<string>();
            var definitionContent = contents[1].Length > 0 ? contents[1].Split(";") : Array.Empty<string>();
            
            return (mainContent,
                definitionContent);
        }

        public static string GetIdWithoutDelimiters(string id)
        {
            return id.Replace("[", "").Replace("]", "");
        }

        public static bool IsFunctionIdentifier(string id)
        {
            return id == NodeCategory.If  || 
                   id == NodeCategory.Loop || 
                   id == NodeCategory.Add;
        }
        
        public static bool IsFunctionIdentifier(char id)
        {
            return IsFunctionIdentifier(id.ToString());
        }
        
        public static string GetTypeInfo(string id,string symbol)
        {
            var match = Regex.Match(id, @$"^{symbol}<.*?,.*?>");
            
            return match.Groups[0].Value;
        }
        
        public static Type GetAuxType(string typeInfo)
        {
            if (typeInfo == "")
                return null;
            
            var auxType = typeInfo.Split(",")[1];
            auxType = auxType.Remove(auxType.Length - 1);
            
            return Type.GetType(auxType);
        }

        public static string GetValueFromQuotes(string id)
        {
            //Find entire value
            var match = Regex.Match(id, @"^\'.*?\'");
            //Retrieve first match
            var currentId = match.Groups[0].Value;
            //Strip quotes
            return currentId[1..^1];
        }
        
    }
}
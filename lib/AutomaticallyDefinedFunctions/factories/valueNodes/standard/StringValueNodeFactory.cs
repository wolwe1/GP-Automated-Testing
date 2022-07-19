using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard
{
    public static class StringValueNodeFactory
    {
        public static IEnumerable<ValueNode<string>> GetAll()
        {
            return Enumerable.Range('a', 'z' - 'a' + 1).Select(c => new ValueNode<string>( ((char)c).ToString()));
        }

        public static ValueNode<string> Get()
        {
            var choice = RandomNumberFactory.Next(26);
            return GetAll().ElementAt(choice);
        }
        
        public static ValueNode<string> Get(string id)
        {
            return new ValueNode<string>(id);
        }
        
        
    }
}
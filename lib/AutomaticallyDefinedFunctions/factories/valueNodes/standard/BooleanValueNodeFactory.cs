using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard
{
    public static class BooleanValueNodeFactory
    {
        public static IEnumerable<ValueNode<bool>> GetAll()
        {
            return new List<ValueNode<bool>>() {new(true), new(false)};
        }

        public static ValueNode<bool> Get()
        {
            var choice = RandomNumberFactory.Next(2);

            return choice == 0 ? new ValueNode<bool>(true) : new ValueNode<bool>(false);
        }

        public static ValueNode<bool> Get(string id)
        {
            var idAsBool = Boolean.Parse(id);

            return new ValueNode<bool>(idAsBool);
        }
    }
}
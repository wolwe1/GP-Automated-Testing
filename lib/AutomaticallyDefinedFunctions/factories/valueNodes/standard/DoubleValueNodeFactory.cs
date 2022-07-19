using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.standard
{
    public static class DoubleValueNodeFactory
    {
        public static IEnumerable<ValueNode<double>> GetAll()
        {
            return Enumerable.Range(0, 10).Select(d => new ValueNode<double>(d));
        }

        public static ValueNode<double> Get()
        {
            var choice = RandomNumberFactory.Next(10);
            return new ValueNode<double>(choice);
        }

        public static ValueNode<double> Get(string id)
        {
            var idAsDouble = Double.Parse(id);

            return new ValueNode<double>(idAsDouble);
        }
    }
}
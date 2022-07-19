using lib.GeneticAlgorithm.source.core.population;

namespace lib.GeneticAlgorithm.source.mockImplementations
{
    public class OutputContainer<T> : IOutputContainer<T>
    {
        private List<T> _outputs;

        public OutputContainer(T value)
        {
            _outputs = new List<T>(){value};
        }
        
        public OutputContainer(List<T> outputs)
        {
            _outputs = outputs;
        }

        public IEnumerable<T> GetOutputValues()
        {
            return _outputs;
        }

        public string GetOutputString()
        {
            return string.Join(",", _outputs);
        }
    }
}
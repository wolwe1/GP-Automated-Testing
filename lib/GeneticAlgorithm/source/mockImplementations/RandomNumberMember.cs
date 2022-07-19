using lib.GeneticAlgorithm.source.core.population;

namespace lib.GeneticAlgorithm.source.mockImplementations
{
    public class RandomNumberMember : IPopulationMember<double>
    {
        private readonly double _value;
        
        public RandomNumberMember(int number)
        {
            _value = number;
        }

        public string GetId()
        {
            return _value.ToString();
        }

        public IOutputContainer<double> GetResult()
        {
            return new OutputContainer<double>(_value);
        }

        public IPopulationMember<double> GetCopy()
        {
            return new RandomNumberMember((int) _value);
        }
    }
}
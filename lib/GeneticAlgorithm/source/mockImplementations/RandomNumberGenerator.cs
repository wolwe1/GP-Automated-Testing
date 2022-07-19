using lib.GeneticAlgorithm.source.core.population;

namespace lib.GeneticAlgorithm.source.mockImplementations
{
    public class RandomNumberGenerator : IPopulationGenerator<double>
    {
        private Random numgen;

        public RandomNumberGenerator()
        {
            numgen = new Random(0);
        }
        public IPopulationMember<double> GenerateNewMember()
        {
            return new RandomNumberMember(numgen.Next(11));
        }

        public IPopulationMember<double> GenerateFromId(string chromosome)
        {
            var number = int.Parse(chromosome);

            return new RandomNumberMember(number);
        }

        public void SetSeed(int seed)
        {
            numgen = new Random(seed);
        }
    }
}
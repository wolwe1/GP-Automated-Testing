using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.terminationCriteria
{
    public class GenerationCountCriteria : ITerminationCriteria
    {
        private readonly int _maxGenerations;

        public GenerationCountCriteria(int maxGenerations)
        {
            _maxGenerations = maxGenerations;
        }

        public bool Met<T>(int generationCount, GenerationRecord<T> generationRecord)
        {
            return generationCount >= _maxGenerations;
        }

        public string GetReason()
        {
            return "Reached desired generation count";
        }
    }
}
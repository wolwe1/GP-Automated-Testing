using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.terminationCriteria
{
    public class DesiredFitnessCriteria : ITerminationCriteria
    {
        private readonly double _desiredFitness;

        public DesiredFitnessCriteria(double desiredFitness)
        {
            _desiredFitness = desiredFitness;
        }

        public bool Met<T>(int generationCount, GenerationRecord<T> generationRecord)
        {
            var fitnessValues = generationRecord.GetFitnessValues();
            var bestFitness = fitnessValues.Max();

            return bestFitness >= _desiredFitness;
        }

        public string GetReason()
        {
            return $"The desired fitness of {_desiredFitness} was met";
        }
    }
}
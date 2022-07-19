using lib.GeneticAlgorithm.source.statistics;

namespace lib.GeneticAlgorithm.source.controlModel.terminationCriteria
{
    public class DesiredFitnessForFitnessFunctionCriteria : ITerminationCriteria
    {
        private readonly double _desiredFitness;
        private readonly Type _typeOfFitnessFunction;

        public DesiredFitnessForFitnessFunctionCriteria(Type typeOfFitnessFunction, double desiredFitness)
        {
            _desiredFitness = desiredFitness;
            _typeOfFitnessFunction = typeOfFitnessFunction;
        }

        public bool Met<T>(int generationCount, GenerationRecord<T> generationRecord)
        {
            if (generationRecord == null)
                return false;
                
            var fitnessList = generationRecord.GetMemberRecords().Select( m => m.GetFitness());

            var targetFitnessList = fitnessList.Select(f => f.GetFitnessOfFunction(_typeOfFitnessFunction));

            var targetFitnessValues = targetFitnessList.Select(f => f.GetFitnessNoForward());

            var bestFitness = targetFitnessValues.Max();

            return bestFitness >= _desiredFitness;
        }

        public string GetReason()
        {
            var desiredClassName = _typeOfFitnessFunction.FullName?.Split(".").Last();
            return $"The desired fitness of {_desiredFitness} for criteria of {desiredClassName} was met";
        }
    }
}
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.GeneticAlgorithm.source.controlModel.terminationCriteria
{
    public class NoAverageImprovementCriteria : ITerminationCriteria
    {
        private readonly AverageMeasure _averageMeasure;
        private readonly int _maxNumberOfGenerationsWithoutChange;
        private int _generationsWithoutChange;
        private double _runningAverage;

        public NoAverageImprovementCriteria(int maxNumberOfGenerationsWithoutChange)
        {
            _runningAverage = 0;
            _generationsWithoutChange = 0;
            _maxNumberOfGenerationsWithoutChange = maxNumberOfGenerationsWithoutChange;
            _averageMeasure = new AverageMeasure();
        }

        public bool Met<T>(int generationCount, GenerationRecord<T> generationRecord)
        {
            if (generationRecord == null)
                return false;

            var average = _averageMeasure.Get(generationRecord);

            if (average > _runningAverage)
            {
                _runningAverage = average;
                _generationsWithoutChange = 0;

                return false;
            }

            _generationsWithoutChange++;

            return _generationsWithoutChange >= _maxNumberOfGenerationsWithoutChange;
        }

        public string GetReason()
        {
            return $"Population went {_maxNumberOfGenerationsWithoutChange} generations without improvement";
        }
    }
}
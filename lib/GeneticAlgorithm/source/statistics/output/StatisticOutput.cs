using lib.GeneticAlgorithm.source.statistics.calculatedResults;

namespace lib.GeneticAlgorithm.source.statistics.output
{
    public class StatisticOutput
    {
        private CalculationResultSet _generationValues;
        private string _heading;
        private object _runValue;
        private string _scale;

        public StatisticOutput SetHeading(string heading)
        {
            _heading = heading;
            return this;
        }

        public StatisticOutput SetGenerationValues(CalculationResultSet generationValues)
        {
            _generationValues = generationValues;
            return this;
        }

        public StatisticOutput SetRunValue(object runStatistic)
        {
            _runValue = runStatistic;
            return this;
        }

        public StatisticOutput SetScale(string scale)
        {
            _scale = scale;
            return this;
        }

        public string GetHeading()
        {
            return _heading;
        }

        public object GetRunValue()
        {
            return _runValue;
        }

        public CalculationResultSet GetGenerationValues()
        {
            return _generationValues;
        }

        public string GetScale()
        {
            return _scale;
        }
    }
}
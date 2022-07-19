using System.Text;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.common.testHandler.Generalisation
{
    public class GeneralisationResult
    {
        public string BaseTestName { get; }
        private List<GeneralisationTest> _generalisationTestsResults;
        
        public GeneralisationResult(string baseTestName, List<GeneralisationTest> generalisationPerformance)
        {
            BaseTestName = baseTestName;
            _generalisationTestsResults = generalisationPerformance;
        }

        public string Summarise(List<StatisticMeasure> measures)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"ADF's trained on {BaseTestName}");
            strBuilder.AppendLine(GetOverallStatistics(measures));
            foreach (var generalisationTestsResult in _generalisationTestsResults)
            {
                var individualTestSummary = generalisationTestsResult.Summarise(measures);
                strBuilder.AppendLine(individualTestSummary);
            }
            return strBuilder.ToString();
        }

        private string GetOverallStatistics(List<StatisticMeasure> measures)
        {
            var strBuilder = new StringBuilder();
            var fitnessValueSet = CreateResultSet();
            
            foreach (var statisticMeasure in measures)
            {
                var overallResult = statisticMeasure.GetRunStatistic(fitnessValueSet);
                strBuilder.AppendLine($"Overall {statisticMeasure.GetHeading()} : {overallResult}");
            }

            return strBuilder.ToString();
        }
        
        private CalculationResultSet CreateResultSet()
        {
            var allFitnessValues = _generalisationTestsResults.SelectMany(test => test.GetFitnessValues()).ToList();

            var resultSet = new CalculationResultSet();
            foreach (var fitness in allFitnessValues)
            {
                resultSet.Add(fitness.GetFitness(),"");
            }

            return resultSet;
        }
    }
}
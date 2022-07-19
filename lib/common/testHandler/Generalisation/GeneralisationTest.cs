using System.Text;
using lib.GeneticAlgorithm.source.fitnessFunctions;
using lib.GeneticAlgorithm.source.statistics.calculatedResults;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.common.testHandler.Generalisation
{
    public class GeneralisationTest
    {
        public string TestName { get; }
        private List<Fitness> _fitnessValuesForTest;

        public GeneralisationTest(string testName, List<Fitness> performances)
        {
            TestName = testName;
            _fitnessValuesForTest = performances;
        }

        public string Summarise(List<StatisticMeasure> measures)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Performance on {TestName}");

            var fitnessResultSet = CreateResultSet();

            foreach (var statisticMeasure in measures)
            {
                var performanceOnTest = statisticMeasure.GetRunStatistic(fitnessResultSet);
                strBuilder.AppendLine($"    {statisticMeasure.GetHeading()} : {performanceOnTest}");
            }

            return strBuilder.ToString();
        }

        private CalculationResultSet CreateResultSet()
        {
            var resultSet = new CalculationResultSet();
            foreach (var fitness in _fitnessValuesForTest)
            {
                resultSet.Add(fitness.GetFitness(),"");
            }

            return resultSet;
        }

        public List<Fitness> GetFitnessValues()
        {
            return _fitnessValuesForTest;
        }
    }
}
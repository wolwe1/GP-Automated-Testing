using System.Text;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.GeneticAlgorithm.source.statistics.output.implementations
{
    public class DefaultOutputPrinter : IOutputPrinter
    {
        public virtual void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {
            var builder = new StringBuilder();
            builder.AppendLine(CreateRunOutput(runStatistics));
            builder.AppendLine(CreateGenerationOutput(runStatistics));

            Console.WriteLine(builder.ToString());
        }

        protected string CreateRunOutput(List<StatisticOutput> runStatistics)
        {
            var builder = new StringBuilder();

            for (var index = 0; index < runStatistics.Count; index++)
            {
                var statisticOutput = runStatistics[index];

                var runOutput = $" - {statisticOutput.GetHeading()} : {statisticOutput.GetRunValue()} {statisticOutput.GetScale()}";
                builder.Append(runOutput);
            }

            return builder.ToString();
        }

        protected string CreateGenerationOutput(List<StatisticOutput> runStatistics)
        {
            var builder = new StringBuilder();

            var numberOfGenerations = runStatistics.ElementAt(0).GetGenerationValues().Size();

            //For each generation
            for (var index = 0; index < numberOfGenerations; index++)
            {
                builder.AppendLine($"\nGeneration {index} : ");
                foreach (var runStatistic in runStatistics)
                {
                    var generationValuesForStatistic = runStatistic.GetGenerationValues();

                    if (generationValuesForStatistic != null)
                    {
                        var runOutput =
                            $" {runStatistic.GetHeading()} : {generationValuesForStatistic.Get(index)?.GetResult()} {runStatistic.GetScale()}";
                        builder.Append(runOutput);
                    }
                }
                
            }

            return builder.ToString();
        }
    }
}
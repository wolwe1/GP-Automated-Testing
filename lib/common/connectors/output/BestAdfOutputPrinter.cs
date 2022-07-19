using System.Text;
using lib.AutomaticallyDefinedFunctions.structure.adf.helpers;
using lib.GeneticAlgorithm.source.statistics;
using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.runStatistics;
using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.common.connectors.output
{
    public class BestAdfOutputPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestAdfOutputPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }
        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> evaluationResults)
        {
            var bestMemberInRun = _bestMemberMeasure.GetBestPerformer(evaluationResults);

            var memberOutput = bestMemberInRun.Member.GetResult();

            var strOutput = memberOutput.GetOutputString();

            return new StatisticOutput()
                .SetHeading("Best member Output")
                .SetRunValue(strOutput);
        }

        private string CreateOutputString<T>(List<Output<T>> outputs)
        {
            var strBuilder = new StringBuilder();

            for (var index = 0; index < outputs.Count; index++)
            {
                var output = outputs[index];
                strBuilder.AppendLine($"Output {index} : Failed - {output.Failed}");

                if (!output.Failed)
                    strBuilder.Append($" Value - {output.Value}");
            }

            return strBuilder.ToString();
        }
    }
}
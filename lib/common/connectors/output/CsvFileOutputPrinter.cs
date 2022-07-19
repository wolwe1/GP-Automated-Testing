using lib.GeneticAlgorithm.source.statistics.history;
using lib.GeneticAlgorithm.source.statistics.output;
using lib.GeneticAlgorithm.source.statistics.output.implementations;

namespace lib.common.connectors.output
{
    public class CsvFileOutputPrinter : FileOutputPrinter
    {
        public override void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {

            WriteToCsv(runStatistics,runRecord);
            
            base.Print(runStatistics,runRecord);
        }

        private void WriteToCsv<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {
            foreach (var runStatistic in runStatistics)
            {
                var runNumber = runRecord.GetRunNumber();
                var statisticType = runStatistic.GetHeading();
                var values = runStatistic.GetGenerationValues();
                if (values != null)
                {
                    
                    var calculatedValues = values.ToList().Select(c => c.GetResult()).ToList();
                    CreateCsvFile(statisticType,calculatedValues,runRecord);
                }
            }
        }

        //Creates timeseries data for the generations
        private void CreateCsvFile<T>(string statisticType, List<double> values, RunRecord<T> runRecord)
        {
            var numberOfGenerations = values.Count;
            var generationRange = Enumerable.Range(1, numberOfGenerations);

            var fileName = $"{runRecord.AdditionalRunInfo}_Run{runRecord.GetRunNumber()}_{statisticType}";
            fileName = Path.Combine("run-statistics", fileName);
            
            var header = string.Join(",", generationRange.Select(g => $"Generation {g.ToString()}"));
            
            //Replace commas with full stops
            var convertedValues = values.Select(v => v.ToString().Replace(",", "."));
            var csvValues = string.Join(",", convertedValues);

            var data = header + "\n" + csvValues;


            TryAppendOutputToFile(data, fileName);

        }
    }
}
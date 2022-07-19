using lib.GeneticAlgorithm.source.statistics.runStatistics.implementations.measure;

namespace lib.common.testHandler.Generalisation
{
    public class GeneralisationResultsManager
    {
        private readonly List<StatisticMeasure> _measures;
        private readonly List<GeneralisationResult> _generalisationResults;

        public GeneralisationResultsManager()
        {
            _generalisationResults = new List<GeneralisationResult>();
            _measures = new List<StatisticMeasure>()
            {
                new AverageMeasure(),
                new BestPerformerMeasure(),
                new StandardDeviationMeasure()
            };

        }

        public GeneralisationResult GetGeneralisationResultsFromADFsTrainedOn(string baseTestName)
        {
            return _generalisationResults.FirstOrDefault(r => r.BaseTestName == baseTestName);
        }

        public void Summarise()
        {
            foreach (var generalisationResult in _generalisationResults)
            {
                var summary = generalisationResult.Summarise(_measures);
                Console.WriteLine(summary);
                WriteToFile(summary,generalisationResult.BaseTestName);
            }
            
        }

        private void WriteToFile(string data, string fileName)
        {
            try
            {
                var projectDir = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
                projectDir = Path.Combine(projectDir, "output");
                
                var destPath = Path.Combine(projectDir, "generalisation");
                destPath = Path.Combine(destPath, fileName);
                File.WriteAllText(destPath, data);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write output to file");
            }
        }

        public void AddResults(GeneralisationResult result)
        {
            _generalisationResults.Add(result);
        }
    }
}
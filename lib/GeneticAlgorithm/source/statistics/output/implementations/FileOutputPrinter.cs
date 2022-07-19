using System.Text;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.GeneticAlgorithm.source.statistics.output.implementations
{
    public class FileOutputPrinter : DefaultOutputPrinter
    {
        public override void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {
            var builder = new StringBuilder();
            builder.AppendLine(CreateRunOutput(runStatistics));
            builder.AppendLine(CreateGenerationOutput(runStatistics));

            var fileName = CreateFileName(runRecord);
            

            TryWriteOutputToFile(builder.ToString(), fileName);
            Console.WriteLine(builder.ToString());
        }

        protected string CreateFileName<T>(RunRecord<T> runRecord)
        {
            var fileName = runRecord.AdditionalRunInfo == "" ? DateTime.Now.ToLongTimeString() : runRecord.AdditionalRunInfo;
            return $"{fileName}- Run {runRecord.GetRunNumber()}.txt";
        }
        protected bool TryWriteOutputToFile(string data,string fileName)
        {
            try
            {
                var projectDir = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
                projectDir = Path.Combine(projectDir, "output");
                
                var destPath = Path.Combine(projectDir, fileName);
                File.WriteAllText(destPath, data);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write output to file");
                return false;
            }
            
        }
        
        protected bool TryAppendOutputToFile(string data,string fileName)
        {
            try
            {
                var projectDir = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
                projectDir = Path.Combine(projectDir, "output");
                
                var destPath = Path.Combine(projectDir, fileName);
                File.AppendAllText(destPath, data);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write output to file");
                return false;
            }
            
        }
    }
}
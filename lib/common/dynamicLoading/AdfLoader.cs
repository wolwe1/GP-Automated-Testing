using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.structure.state;
using lib.common.settings;
using lib.common.testHandler;

namespace lib.common.dynamicLoading
{
    public class AdfLoader
    {
        public static void SaveToFile(List<TestHistory> testHistories)
        {
            foreach (var testHistory in testHistories)
            {
                SaveToFile(testHistory);
            }
        }

        public static void SaveToFile(TestHistory testHistory)
        {
            var testName = testHistory.GetTestName();
            
            var inputType = testHistory.GetInputType();
            var responseType = testHistory.GetResponseType();
            var numberOfInputs = testHistory.GetNumberOfInputs();

            var data = "";
            for (var i = 0; i < testHistory.GetNumberOfRuns(); i++)
            {
                var bestAdfs = testHistory.GetBestAdfs(i);
                
                data += i + "$" + numberOfInputs + "$" + inputType.FullName + "$" + responseType.FullName + "$";
                data += string.Join("$", bestAdfs);

                //Delimit a run
                data += "~";
            
            }
            TryWriteOutputToFile(data, testName);
        }

        private static bool TryWriteOutputToFile(string data,string fileName)
        {
            try
            {
                var filePath = GetFilePath(fileName);
                File.AppendAllText(filePath, data);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write output to file");
                return false;
            }
            
        }

        private static string GetFilePath(string fileName)
        {
            var projectDir = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
            projectDir = Path.Combine(projectDir, "output");
            projectDir = Path.Combine(projectDir, "adf");
            
            return Path.Combine(projectDir, fileName);
        }

        public static Dictionary<string, List<IStateBasedAdf>> ReadFromDirectory(string directory)
        {
            var adfsFromFiles = new Dictionary<string, List<IStateBasedAdf>>();
            
            foreach (var file in Directory.EnumerateFiles(GetFilePath(""), "*"))
            {
                var adfs = ReadFromPath(file);
                var fileNameWithoutPath = file[ (file.IndexOf(directory, StringComparison.Ordinal) + 5 )..];
                adfsFromFiles.Add(fileNameWithoutPath,adfs);
            }

            return adfsFromFiles;
        }
        
        public static List<IStateBasedAdf> ReadFromPath(string filePath)
        {
            var data = File.ReadAllText(filePath);

            return ParseFileData(data);
        }
        public static List<IStateBasedAdf> ReadFromFile(string fileName)
        {
            var filePath = GetFilePath(fileName);

            var data = File.ReadAllText(filePath);

            return ParseFileData(data);
        }

        private static List<IStateBasedAdf> ParseFileData(string data)
        {
            var runs = data[..^1].Split("~");

            var adfsForRun = new List<IStateBasedAdf>();
            foreach (var runData in runs)
            {
                adfsForRun.AddRange(ParseRunData(runData));
            }

            return adfsForRun;
        }
        
        private static List<IStateBasedAdf> ParseRunData(string data)
        {
            var items = data.Split("$");

            var runNumber = int.Parse(items[0]);
            var numberOfInputs = int.Parse(items[1]);
            var inputType = Type.GetType(items[2]);
            var responseType = Type.GetType(items[3]);

            var adfIds = items.Skip(4);
            
            return GenerateAdfs(runNumber,numberOfInputs,inputType,responseType,adfIds);
        }

        private static List<IStateBasedAdf> GenerateAdfs(int runNumber, int numberOfInputs, Type inputType, Type responseType,
            IEnumerable<string> adfIds)
        {
            if (inputType == typeof(string))
            {
                return GenerateAdfs<string>(runNumber,numberOfInputs,responseType,adfIds);
            }
            
            if (inputType == typeof(double) || inputType == typeof(int))
            {
                return GenerateAdfs<double>(runNumber,numberOfInputs,responseType,adfIds);
            }
            
            if (inputType == typeof(bool))
            {
                return GenerateAdfs<bool>(runNumber,numberOfInputs,responseType,adfIds);
            }

            throw new Exception("Could not find input type of test");
        }
        
        private static List<IStateBasedAdf> GenerateAdfs<T>(int runNumber, int numberOfInputs, Type responseType,
            IEnumerable<string> adfIds) where T : IComparable
        {
            if (responseType == typeof(string))
            {
                return GenerateAdfs<T,string>(runNumber,numberOfInputs,adfIds);
            }
            
            if (responseType == typeof(double))
            {
                return GenerateAdfs<T,double>(runNumber,numberOfInputs,adfIds);
            }
            
            if (responseType == typeof(bool))
            {
                return GenerateAdfs<T,bool>(runNumber,numberOfInputs,adfIds);
            }
            
            throw new Exception("Could not find response type of test");
        }
        
        private static List<IStateBasedAdf> GenerateAdfs<T, TU>(int runNumber, int numberOfInputs, IEnumerable<string> adfIds) where TU : IComparable where T : IComparable
        {
            var settings = new StateAdfSettings<T, TU>(
                GlobalSettings.MaxFunctionDepth,
                GlobalSettings.MaxMainDepth,
                numberOfInputs,
                GlobalSettings.TerminalChance
            );

            var generator = new StateAdfGenerator<T, TU>(runNumber, settings);

            return adfIds.Select(id => (IStateBasedAdf) generator.GenerateFromId(id)).ToList();
        }
        
    }
}
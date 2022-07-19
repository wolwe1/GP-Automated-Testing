using lib.common.dynamicLoading;

namespace lib.common.testHandler.setup
{
    /*
     * This test strategy loads test information from a file
     */
    public class TestFileDescriptorStrategy : ITestStrategy
    {
        private readonly string _filePath;

        public TestFileDescriptorStrategy(string filePath)
        {
            _filePath = filePath;
        }

        public List<Test<object>> LoadTests()
        {

            if (!File.Exists(_filePath)) throw new Exception($"Test sample file: {_filePath} does not exist");
            
            var lines = File.ReadAllLines(_filePath);

            return GetTestsFromHelper(lines);

        }
        
        private List<Test<object>> GetTestsFromHelper(string[] lines)
        {
            var tests = new List<Test<object>>();
            
            for (var i = 0; i < lines.Length; i+= 3)
            {
                var pathToDll = lines[i];
                var className = lines[i+1];
                var functionName = lines[i+2];

                var test = AssemblyLoader.CreateTestFromMethod(pathToDll,className, functionName);

                tests.Add(test);
            }

            return tests;
        }
    }
}
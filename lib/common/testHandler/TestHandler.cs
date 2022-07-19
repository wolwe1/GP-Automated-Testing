using lib.common.dynamicLoading;
using lib.common.settings;
using lib.common.testHandler.integration;
using lib.common.testHandler.setup;
using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.common.testHandler
{
    /*
     * This helper class loads tests using the provided test strategy and runs the genetic algorithm provided by the builder against the
     * tests loaded by the strategy
     */
    public class TestHandler
    {
        private readonly ITestStrategy _testStrategy;
        
        private List<Test<object>> _tests;
        private int _currentTest = 0;
        private readonly GeneticAlgorithmBuilder _builder;

        public TestHandler(ITestStrategy testStrategy,GeneticAlgorithmBuilder builder)
        {
            _testStrategy = testStrategy;
            _tests = new List<Test<object>>();
            _builder = builder;
        }

        public TestHandler LoadTests()
        {
            _tests = _testStrategy.LoadTests();
            return this;
        }

        public List<TestHistory> RunAllTests()
        {
            var testHistories = new List<TestHistory>();
            for (var i = 0; i < _tests.Count; i++)
            {
                var testHistory = RunGaAgainstNextTest();
                //testHistories.AddRange(testHistory); By default, the history is not stored in memory as this can cause performance degradation
            }

            return testHistories;
        }

        private Test<object> GetNextTest()
        {
            return _currentTest >= _tests.Count ? null : _tests.ElementAt(_currentTest++);
        }

        private List<TestHistory> RunGaAgainstNextTest()
        {
            var test = GetNextTest();
            
            if(test == null)
                return null;

            Console.WriteLine($"Running GA against Test: {test?.GetName()}");
            var inputType = test?.GetArguments()?.ElementAt(0);

            RunGaAgainstTest(inputType, test);

            //By default, not keeping history in memory
            return null;
        }

        private TestHistory RunGaAgainstTest(Type argumentsType, Test<object> test)
        {
            if (argumentsType == typeof(string))
                return RunGaAgainstTest<string>(test);
            
            if (argumentsType == typeof(double) || argumentsType == typeof(int))
                return RunGaAgainstTest<double>(test);
            
            if (argumentsType == typeof(bool))
                return RunGaAgainstTest<bool>(test);
            
            throw new Exception($"Could not dispatch GA for test of type {argumentsType?.FullName}");
        }
        
        private TestHistory RunGaAgainstTest<T>(Test<object> test) where T : IComparable
        {
            var geneticAlgorithm = _builder.Build<T>(test,0);
            
            IExecutionHistory<T> runHistory = null;
            for (var i = 0; i < GlobalSettings.NumberOfRuns; i++)
            {
                geneticAlgorithm.SetSeed(i);
                runHistory = geneticAlgorithm.Run();
            }
            AdfLoader.SaveToFile(new TestHistory(runHistory,test));
            
            runHistory.Summarise();

            return null; 
        }
    }

}
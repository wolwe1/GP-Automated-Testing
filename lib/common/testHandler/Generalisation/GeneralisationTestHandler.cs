using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.state;
using lib.common.connectors.ga;
using lib.common.coverage.calculators;
using lib.common.fitnessFunctions;
using lib.common.testHandler.setup;
using lib.GeneticAlgorithm.source.fitnessFunctions;

namespace lib.common.testHandler.Generalisation
{
    public class GeneralisationTestHandler
    {
        private readonly List<Test<object>> _tests;
        private readonly Dictionary<string,List<Test<object>>> _testSets;

        public GeneralisationTestHandler(ITestStrategy testStrategy)
        {
            _tests = testStrategy.LoadTests();
            _testSets = GenerateTestSets();
        }

        /*
         * This function dynamically creates generalization sets from the available methods, by matching interface types
         * and sub types
         */
        private Dictionary<string, List<Test<object>>> GenerateTestSets()
        {
            var testSets = new Dictionary<string, List<Test<object>>>();
            
            foreach (var baseTest in _tests)
            {
                var numberOfInputs = baseTest.GetArguments().Count;
                var inputType = baseTest.GetArguments()[0];
                var responseType = baseTest.GetUnderlyingReturnType();
                var subsetTests = GetSubsetTests(baseTest.GetName(),numberOfInputs,inputType,responseType);
                testSets.Add(baseTest.GetName(),subsetTests);
            }
            return testSets;
        }

        private List<Test<object>> GetSubsetTests(string baseTestName, int maxInputs, Type baseInputType,
            Type baseResponseType)
        {
            var matchingTests = new List<Test<object>>();
            foreach (var test in _tests)
            {
                if (test.GetName() != baseTestName)
                {
                    var numberOfInputs = test.GetArguments().Count;
                    var inputType = test.GetArguments()[0];
                    var responseType = test.GetUnderlyingReturnType();

                    //These just allow us to map double based ADFs to integer tests
                    if (inputType == typeof(int))
                        inputType = typeof(double);
                    
                    if (baseInputType == typeof(int))
                        baseInputType = typeof(double);
                    
                    if (numberOfInputs <= maxInputs && inputType == baseInputType && responseType == baseResponseType)
                    {
                        matchingTests.Add(test);
                    }
                }
            }
            return matchingTests;
        }

        public GeneralisationResultsManager Run(Dictionary<string,List<IStateBasedAdf>> adfsWithOriginTests)
        {
            var generalisationResults = new GeneralisationResultsManager();
            
            foreach (var adfsWithOriginTest in adfsWithOriginTests)
            {
                var baseTestName = adfsWithOriginTest.Key;
                var testSet = _testSets.FirstOrDefault(i => i.Key == baseTestName);

                //Test base does not generalise
                if (testSet.Value.Count != 0)
                {
                    Console.WriteLine($"Testing generalisation of ADF's trained on {baseTestName}");
                    var generalisationPerformance =RunAdfsAgainstGeneralisationSet(adfsWithOriginTest.Value, testSet);

                    generalisationResults.AddResults(new GeneralisationResult(baseTestName,generalisationPerformance));
                }
            }

            return generalisationResults;
        }

        private List<GeneralisationTest> RunAdfsAgainstGeneralisationSet(List<IStateBasedAdf> adfs, KeyValuePair<string, List<Test<object>>> testSet)
        {
            var tests = testSet.Value;

            var testResults = new List<GeneralisationTest>();
            foreach (var test in tests)
            {
                var testName = test.GetName();
                Console.WriteLine($"Testing against {testName}");
                var testResult = RunAdfsAgainstTest(adfs,test);
                
                testResults.Add(testResult);
            }
            return testResults;
        }

        private GeneralisationTest RunAdfsAgainstTest(List<IStateBasedAdf> adfs, Test<object> test)
        {
            var testReturnType = test.GetUnderlyingReturnType();
            
            if(testReturnType == typeof(string))
                return RunAdfsAgainstTest<string>(adfs,test);
            
            if(testReturnType == typeof(double) || testReturnType == typeof(int))
                return RunAdfsAgainstTest<double>(adfs,test);
            
            if(testReturnType == typeof(bool))
                return RunAdfsAgainstTest<bool>(adfs,test);
            
            throw new Exception("Could not identify base test return type");
        }

        private GeneralisationTest RunAdfsAgainstTest<TU>(List<IStateBasedAdf> adfs, Test<object> test) where TU : IComparable
        {
            var testInputType = test.GetArguments()[0];
            
            if(testInputType == typeof(string))
                return RunAdfsAgainstTest<string,TU>(adfs,test);
            
            if(testInputType == typeof(double) || testInputType == typeof(int))
                return RunAdfsAgainstTest<double,TU>(adfs,test);
            
            if(testInputType == typeof(bool))
                return RunAdfsAgainstTest<bool,TU>(adfs,test);

            throw new Exception("Could not identify base test input type");
        }

        public GeneralisationTest RunAdfsAgainstTest<T,TU>(List<IStateBasedAdf> adfs, Test<object> test) where TU : IComparable where T : IComparable
        {
            var fitnessFunction = new CodeCoverageFitnessFunction<TU>(test,new StatementCoverageCalculator(), 15);

            var performances = new List<Fitness>();
            
            foreach (var adf in adfs)
            {
                var wrapper = new AdfPopulationMember<T>((Adf<T>) adf);
                var performance = fitnessFunction.Evaluate(wrapper);
                performances.Add(performance);
            }

            return new GeneralisationTest(test.GetName(),performances);
        }
    }
}
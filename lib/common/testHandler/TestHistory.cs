using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.common.testHandler
{
    public class TestHistory
    {
        private ITypelessExecutionHistory _executionHistory;
        private readonly Test<object> _test;

        public TestHistory(ITypelessExecutionHistory executionHistory,Test<object> test)
        {
            _executionHistory = executionHistory;
            _test = test;
        }

        public string GetTestName()
        {
            return _test.GetName();
        }

        public List<string> GetBestAdfs()
        {
            return _executionHistory.GetBestPerformerIds(10);
        }

        public List<string> GetBestAdfs(int i)
        {
            return _executionHistory.GetBestPerformerIdsForRun(i,10);
        }
        
        public Type GetInputType()
        {
            return _test.GetArguments().FirstOrDefault();
        }

        public Type GetResponseType()
        {
            return _test.GetUnderlyingReturnType();
        }

        public int GetNumberOfInputs()
        {
            return _test.GetArguments().Count;
        }

        public int GetNumberOfRuns()
        {
            return _executionHistory.GetNumberOfRuns();
        }

  
    }
}
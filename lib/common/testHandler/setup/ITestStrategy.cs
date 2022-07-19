namespace lib.common.testHandler.setup
{
    /*
     * A strategy for loading test objects
     */
    public interface ITestStrategy
    {
        List<Test<object>> LoadTests();
    }
}
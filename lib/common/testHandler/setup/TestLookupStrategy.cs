using lib.common.dynamicLoading;

namespace lib.common.testHandler.setup
{
    /*
     * The test lookup strategy loads tests using an interactive command line interface
     */
    public class TestLookupStrategy : ITestStrategy
    {
        public List<Test<object>> LoadTests()
        {
            var tests = new List<Test<object>>();
            while (UserExperience.Ask("Add test"))
            {
                tests.Add(AddTest());
            }

            return tests;
        }
        
        
        private Test<object> AddTest()
        {
            var pathToDll = UserExperience.Get("Full path to .dll");
            var className = UserExperience.Get("Class name");
            var functionName = UserExperience.Get("Function name");

            return AssemblyLoader.CreateTestFromMethod(pathToDll,className, functionName);
        }
        
        public Type? GetClassByName(string name)
        {
            var qualifiedName = LookupConversion(name);

            if (qualifiedName != null) return Type.GetType(qualifiedName);
            
            var asm = typeof(TestHandler).Assembly;

            var assemblyTypes = asm.ExportedTypes;

            return assemblyTypes.FirstOrDefault(x => x.Name == name);

        }
        
        private string? LookupConversion(string typeName)
        {
            return typeName.ToLower() switch
            {
                "int" => "System.Int32",
                "double" => "System.Double",
                "string" => "System.String",
                "boolean" => "System.Boolean",
                _ => null
            };
        }

    }
}
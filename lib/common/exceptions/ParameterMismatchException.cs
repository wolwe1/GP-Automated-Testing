using lib.common.dynamicLoading;

namespace lib.common.exceptions
{
    public class ParameterMismatchException : Exception
    {
        private ParameterMismatchException(string className, string functionName, List<object> parameters, List<Type> argumentTypes)
        : base(CreateMessage(className,functionName,parameters,argumentTypes)){
            
        }

        private static string CreateMessage(string className, string functionName, IEnumerable<object> parameters, IEnumerable<Type> argumentTypes)
        {
            return $"Provided parameters for method {className}.{functionName} do not match.\n" +
                   $"Provided[{string.Join(",", parameters)}]\n" +
                   $"Expected[{string.Join(",", argumentTypes)}]\n";
        }

        public static ParameterMismatchException Create(DynamicMethod method, List<object> parameters)
        {
            var className = method.GetType().Name;
            var functionName = method.GetName();
            var expectedTypes = method.GetArguments();

            return new ParameterMismatchException(className, functionName, parameters,expectedTypes);
        }
    }
}
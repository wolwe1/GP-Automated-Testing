using System.Reflection;
using lib.common.exceptions;

namespace lib.common.dynamicLoading
{
    public class DynamicMethod
    {
        private readonly DynamicClass _class;
        private readonly MethodInfo _methodInfo;
        private readonly List<Type> _argumentTypes;
        private readonly Type _returnType;

        public DynamicMethod(DynamicClass classForMethod, string functionName)
        {
            _class = classForMethod;
            
            var method = GetMethodInformation(functionName);
            var argumentTypes = method.GetParameters().Select(x => x.ParameterType).ToList();
            var returnType = method.ReturnType;

            _methodInfo = method;
            _argumentTypes = argumentTypes;
            _returnType = returnType;
            
        }
        
        public object InvokeMethod(object[] args)
        {
            return _methodInfo.Invoke(_class.GetObject(), args);
        }

        private MethodInfo GetMethodInformation(string functionName)
        {
            try
            {
                var method = _class.GetTypeOf().GetMethod(functionName);
                if (method == null)
                    throw new Exception("Not found");
                
                return method;
            }
            catch (Exception e)
            {
                throw new MethodSetupException(
                    $"Unable to get method {functionName} from {_class.GetTypeOf()} : {e.Message}");
            }
            
        }

        public void Print()
        {
            Console.WriteLine($"Class: {_class}, MethodInfo: {_methodInfo}, Arguments: {_argumentTypes}, Return type: {_returnType}");
        }

        public string GetName()
        {
            return _methodInfo.Name;
        }

        public List<Type> GetArguments()
        {
            return _argumentTypes;
        }

        public string GetFullName()
        {
            return $"{_class.GetName()} - {GetName()}";
        }

        public Type GetReturnType()
        {
            return _returnType;
        }
    }
}
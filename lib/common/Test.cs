using lib.common.dynamicLoading;
using lib.common.exceptions;
using testObjects.source.capture;

namespace lib.common
{
    public class Test<T>
    {
        private readonly DynamicMethod _method;

        public Test(DynamicMethod method)
        {
            _method = method;
        }

        public CoverageResultWrapper Run(List<object> parameters)
        {
            if (!ValidParameters(parameters))
            {
                try
                {
                    parameters = CastParametersIfApplicable(parameters);
                }
                catch (Exception)
                {
                    throw ParameterMismatchException.Create(_method,parameters);
                }
                
            }

            var parms = parameters.Cast<object>().ToArray();
            //select only the correct amount of parameters, allows bigger adfs to work on smaller tests
            parms = parms.Take(_method.GetArguments().Count).ToArray();
            
            return (CoverageResultWrapper) _method.InvokeMethod(parms);
        }

        private bool ValidParameters(List<object> parameters)
        {
            for (var i = 0; i < _method.GetArguments().Count; i++)
            {
                var expectedArgument = _method.GetArguments()[i];
                var givenParameter = parameters[i];

                if (givenParameter == null || givenParameter.GetType() != expectedArgument) 
                    return false;
            }

            return true;
        }

        private List<object> CastParametersIfApplicable(List<object> parameters)
        {
            var convertedParams = new List<object>();
     
            for (var i = 0; i < _method.GetArguments().Count; i++)
            {
                var expectedArgument = _method.GetArguments()[i];
                var givenParameter = parameters[i];

                if (givenParameter == null)
                    throw new Exception("Input value is null");
                
                var convertedParam = Convert.ChangeType(givenParameter, expectedArgument);
                convertedParams.Add(convertedParam);
            }

            return convertedParams;
        }

        public List<Type> GetArguments()
        {
            return _method.GetArguments();
        }

        public string GetName()
        {
            return _method.GetFullName();
        }

        public Type GetReturnType()
        {
            return _method.GetReturnType();
        }

        public Type GetUnderlyingReturnType()
        {
            var type = GetReturnType();

            return type.GetGenericArguments()[0];
        }
    }
}
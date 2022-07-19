using System.Collections;
using System.Reflection;
using lib.common.exceptions;

namespace lib.common.dynamicLoading
{
  /*
   * This class is implementation specific and is used to load C# classes not contained within the project
   */
    public static class AssemblyLoader
    {
      private static readonly Hashtable AssemblyReferences = new();
      private static readonly Hashtable ClassReferences = new();

      public static Test<object> CreateTestFromMethod(string assemblyLocation,string className, string functionName)
      {
        var methodToTest = GetMethodFromAssembly(assemblyLocation, className, functionName);

        return new Test<object>(methodToTest);
      }
      
      private static DynamicMethod GetMethodFromAssembly(string dllPath, string className, string functionName)
      {
        var classForMethod = GetClassReference(dllPath, className);

        return new DynamicMethod(classForMethod, functionName);
      }
      
      private static DynamicClass GetClassReference(string assemblyName, string className)
      {
        //Already hold reference
        if (ClassReferences.ContainsKey(className))
          return ((DynamicClass) ClassReferences[className]);
        
        var assembly = GetAssembly(assemblyName);
        
        var targetType = assembly.GetTypes().FirstOrDefault(t => TypeMatchesClassName(t, className));
        
        var classInfo = targetType != null ? new DynamicClass(targetType) : throw new ClassNotFoundException(className,assembly.GetTypes());
        ClassReferences.Add(className, classInfo);

        return classInfo;
      }

      private static Assembly GetAssembly(string assemblyName)
      {
        
        if (AssemblyReferences.ContainsKey(assemblyName))
          return (Assembly) AssemblyReferences[assemblyName];
            
        try
        {
          var newAssembly = Assembly.LoadFrom(assemblyName);
          AssemblyReferences.Add(assemblyName, newAssembly);

          return newAssembly;
        }
        catch (Exception e)
        {
          throw new AssemblyNotFoundException(assemblyName);
        }

      }
      
      private static bool TypeMatchesClassName(Type type,string className)
      {
        return type.FullName != null && type.IsClass && type.FullName.EndsWith("." + className);
      }
      
    }

}
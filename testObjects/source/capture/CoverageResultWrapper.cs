namespace testObjects.source.capture;

public class CoverageResultWrapper
{
    public readonly List<Coverage> Coverages;
    public readonly string ClassName;
    public readonly string FunctionName;
    public readonly int NumberOfNodes;
    public Type ResultType;
    protected dynamic Result;
    protected CoverageResultWrapper(string className, string functionName,int numberNodes)
    {
        Coverages = new List<Coverage>();
        ClassName = className;
        FunctionName = functionName;
        NumberOfNodes = numberNodes;
    }
        
    public static CoverageResult<TU> SetupCoverage<TU>(string className, string functionName, int numberNodes)
    {
        return new CoverageResult<TU>(className,functionName,numberNodes);
    }

    public dynamic GetReturnValue()
    {
        return Result;
    }
        
    public CoverageResultWrapper Merge(CoverageResultWrapper other)
    {
        Coverages.AddRange(other.Coverages);
        return this;
    }
        
}
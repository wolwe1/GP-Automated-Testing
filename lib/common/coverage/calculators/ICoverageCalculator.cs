
using testObjects.source.capture;

namespace lib.common.coverage.calculators
{
    public interface ICoverageCalculator
    {
        double Calculate(CoverageResultWrapper coverageInfo);
        double Calculate(List<CoverageResultWrapper> coverageValues);
    }
}
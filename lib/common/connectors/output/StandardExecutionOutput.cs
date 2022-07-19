using lib.GeneticAlgorithm.source.statistics.history;

namespace lib.common.connectors.output
{
    public class StandardExecutionOutput<T> : BasicExecutionHistory<T>
    {
        public StandardExecutionOutput() : base()
        {
            UseStatistic(new BestAdfOutputPrinter());
            
            //Noisy but interesting example of another type of printer
            //UseStatistic(new BestMemberIdPrinter());
        }

    }
}
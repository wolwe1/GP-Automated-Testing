namespace lib.GeneticAlgorithm.source.statistics.calculatedResults
{
    /// <summary>
    ///     Wrapper for Calculation results, allows for easy conversion from Result to Calculation result
    /// </summary>
    public class CalculationResultSet
    {
        private readonly List<CalculationResult> _calculatedResults;

        public CalculationResultSet(IEnumerable<CalculationResult> results)
        {
            _calculatedResults = results.ToList();
        }

        public CalculationResultSet()
        {
            _calculatedResults = new List<CalculationResult>();
        }

        /// <summary>
        ///     Create a calculated result set from a list of <see cref="MemberRecord{T}" />
        /// </summary>
        public static CalculationResultSet Create<T>(IEnumerable<MemberRecord<T>> results)
        {
            var calculationResults = results.Select(x => new CalculationResult(x.GetFitnessValue(), x.GetMemberId()));
            return new CalculationResultSet(calculationResults);
        }


        /// <summary>
        ///     Create a list of the internal <see cref="CalculationResult" />s
        /// </summary>
        public List<CalculationResult> ToList()
        {
            return _calculatedResults.ToList();
        }

        /// <summary>
        ///     Map current result set to a new one after applying the function
        /// </summary>
        /// <param name="function">Function to map the <see cref="CalculationResult" /> values</param>
        /// <returns>A new <see cref="CalculationResultSet" /> with mapped values</returns>
        public CalculationResultSet Map(CalculationResultExtensions.MapFunc function)
        {
            return new CalculationResultSet(_calculatedResults.Map(function));
        }

        /// <summary>
        ///     Get accumulated value of underlying <see cref="CalculationResult" /> set
        /// </summary>
        /// <param name="function">Function to accumulate the <see cref="CalculationResult" /> values</param>
        /// <returns>The accumulated values</returns>
        public double Accumulate(CalculationResultExtensions.AccFunc function)
        {
            return _calculatedResults.Accumulator(function);
        }

        public double Total()
        {
            return Accumulate((x, n) => x + n);
        }

        public double Max()
        {
            return _calculatedResults.Max(x => x.GetResult());
        }

        public int Size()
        {
            return _calculatedResults.Count();
        }

        public CalculationResult Get(int index)
        {
            if (index < 0 || index >= Size())
                throw new IndexOutOfRangeException(
                    $"Unable to get calculated result {index} from set of size {Size()}");

            return _calculatedResults.ElementAt(index);
        }

        public bool Matches(CalculationResultSet other)
        {
            var resultSetSize = Size();
            if (resultSetSize != other.Size()) return false;

            for (var i = 0; i < resultSetSize; i++)
            {
                var thisResult = _calculatedResults.ElementAt(i);
                var otherResult = other.Get(i);

                if (otherResult.GetResult() != thisResult.GetResult()) return false;
            }

            return true;
        }

        public void Add(double value, string id)
        {
            _calculatedResults.Add(new CalculationResult(value, id));
        }
    }
}
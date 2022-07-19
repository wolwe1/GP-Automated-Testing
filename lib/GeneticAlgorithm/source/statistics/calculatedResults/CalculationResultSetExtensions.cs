namespace lib.GeneticAlgorithm.source.statistics.calculatedResults
{
    public static class CalculationResultSetExtensions
    {
        /// <summary>
        ///     Map each <see cref="CalculationResult" /> in the list to a new <see cref="CalculationResult" /> with a result equal
        ///     to the return value of the function passed in
        /// </summary>
        /// <param name="function">The function to apply to each <see cref="CalculationResult" /> value</param>
        /// <returns>A new List of <see cref="CalculationResult" /></returns>
        public static IEnumerable<CalculationResultSet> Map(this IEnumerable<CalculationResultSet> list,
            CalculationResultExtensions.MapFunc function)
        {
            return list.Select(set => set.Map(function)).ToList();
        }

        /// <summary>
        ///     Applies an accumulator function to the result set of the <see cref="CalculationResult" /> list
        /// </summary>
        /// <param name="function">The accumulator function to apply to <see cref="CalculationResult" /> results</param>
        /// <returns>A new List of <see cref="CalculationResult" /></returns>
        /// <exception cref="Exception">If the list has no members</exception>
        public static List<double> Accumulator(this IEnumerable<CalculationResultSet> list,
            CalculationResultExtensions.AccFunc function)
        {
            return list.Select(set => set.Accumulate(function)).ToList();
        }
    }
}
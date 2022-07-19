namespace lib.GeneticAlgorithm.source.statistics.calculatedResults
{
    public static class CalculationResultExtensions
    {
        public delegate double AccFunc(double val1, double val2);

        public delegate double MapFunc(double val1);

        /// <summary>
        ///     Map each <see cref="CalculationResult" /> in the list to a new <see cref="CalculationResult" /> with a result equal
        ///     to the return value of the function passed in
        /// </summary>
        /// <param name="function">The function to apply to each <see cref="CalculationResult" /> value</param>
        /// <returns>A new List of <see cref="CalculationResult" /></returns>
        public static IEnumerable<CalculationResult> Map(this IEnumerable<CalculationResult> list, MapFunc function)
        {
            var mappedList = new List<CalculationResult>();

            foreach (var result in list)
            {
                var mappedResult = function(result.GetResult());
                var newResult = new CalculationResult(mappedResult, result.GetMemberId());

                mappedList.Add(newResult);
            }

            return mappedList;
        }

        /// <summary>
        ///     Applies an accumulator function to the result set of the <see cref="CalculationResult" /> list
        /// </summary>
        /// <param name="function">The accumulator function to apply to <see cref="CalculationResult" /> results</param>
        /// <returns>A new List of <see cref="CalculationResult" /></returns>
        /// <exception cref="Exception">If the list has no members</exception>
        public static double Accumulator(this IEnumerable<CalculationResult> list, AccFunc function)
        {
            var calculationResults = list.ToList();
            if (!calculationResults.Any()) return 0;

            var accumulator = calculationResults.ElementAt(0).GetResult();

            for (var i = 1; i < calculationResults.Count(); i++)
            {
                var listItem = calculationResults.ElementAt(i);
                accumulator = function(accumulator, listItem.GetResult());
            }

            return accumulator;
        }
    }
}
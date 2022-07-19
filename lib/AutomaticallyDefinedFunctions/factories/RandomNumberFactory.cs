namespace lib.AutomaticallyDefinedFunctions.factories
{
    public static class RandomNumberFactory
    {
        private static Random _random = new();

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static bool TrueOrFalse()
        {
            return Next(2) == 0;
        }

        public static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public static bool AboveThreshold(int threshold)
        {
            var num = Next(100);

            return num > threshold;
        }

        public static T SelectFromList<T>(IEnumerable<T> list)
        {
            var enumerable1 = list.ToList();
            var enumerable = enumerable1.ToList();
            var collectionSize = enumerable.Count;

            return collectionSize switch
            {
                0 => throw new Exception("Cannot select random element from empty list"),
                1 => enumerable.ElementAt(0),
                _ => enumerable1.ElementAt(Next(collectionSize))
            };
        }
        
    }
}
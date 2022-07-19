using lib.AutomaticallyDefinedFunctions.factories;

namespace lib.AutomaticallyDefinedFunctions.generators
{
    public static class FunctionPicker
    {
        public static TFactory PickFactoryAs<T,TFactory>(IEnumerable<IDispatcher> factories)
        {
            var factoriesThatCanDispatch = factories
                .Where(f => f.CanDispatch<T>()).ToList();
            
            var choice = RandomNumberFactory.Next(factoriesThatCanDispatch.Count);

            return (TFactory)factoriesThatCanDispatch.ElementAt(choice);
        }
        
    }
}
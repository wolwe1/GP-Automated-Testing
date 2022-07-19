namespace lib.AutomaticallyDefinedFunctions.factories
{
    public interface IDispatcher
    {
        bool CanDispatch<T>();
        
        bool CanMap(string id);
    }
}
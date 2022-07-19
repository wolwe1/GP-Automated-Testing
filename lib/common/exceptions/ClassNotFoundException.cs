namespace lib.common.exceptions
{
    public class ClassNotFoundException : Exception
    {
        
        public ClassNotFoundException(string className, Type[] types) : base(CreateMessage(className,types))
        {
        }

        private static string CreateMessage(string classname,Type[] availableTypes)
        {
            var types = string.Join(",", availableTypes.Select(t => t.Name));
            
            return $"Could not load class {classname} in assembly, " +
                   $"available types are: {types}\n";
        }
    }
}
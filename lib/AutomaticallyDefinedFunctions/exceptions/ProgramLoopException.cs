namespace lib.AutomaticallyDefinedFunctions.exceptions
{
    public class ProgramLoopException : Exception
    {
        public ProgramLoopException(string counter, string comparator, string bound) : base(CreateMessage(counter,comparator,bound))
        {
        }

        private static string CreateMessage(string counter, string comparator, string bound)
        {
            return $"Loop executed 1000 times ({counter} {comparator} {bound})";
        }

    }
}
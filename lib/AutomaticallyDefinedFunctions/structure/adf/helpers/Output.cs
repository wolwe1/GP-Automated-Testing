namespace lib.AutomaticallyDefinedFunctions.structure.adf.helpers
{
    /*
     * A wrapper around a T value
     */
    public class Output<T>
    {
        public T Value { get; }
        public bool Failed { get; private set; }
        
        public Output(T value, bool failed)
        {
            Value = value;
            Failed = failed;
        }

        public Output(T value)
        {
            Value = value;
            Failed = false;
        }

        public Output()
        {
            Failed = true;
        }

        /*
         * Mark the output as failed, this is done when an error is caught by the runner
         */
        public void Fail()
        {
            Failed = true;
        }
    }
}
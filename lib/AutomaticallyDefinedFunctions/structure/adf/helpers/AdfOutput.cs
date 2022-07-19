namespace lib.AutomaticallyDefinedFunctions.structure.adf.helpers
{
    /*
     * An output container that holds a list of outputs produced by the execution of a program
     */
    public class AdfOutput<T>
    {
        private List<Output<T>> _outputs;

        public AdfOutput(List<Output<T>> outputs)
        {
            _outputs = outputs;
        }

        public AdfOutput(IEnumerable<Output<T>> outputs)
        {
            _outputs = outputs.ToList();
        }

        /*
         * Retrieve the output produced by main branch *index*
         */
        public Output<T> GetOutput(int index)
        {
            return _outputs[index];
        }

        /*
         * Get all outputs produced by the program run
         */
        public List<Output<T>> GetOutputs()
        {
            return _outputs;
        }

        /*
         * Unwrap all the outputs to their T value
         */
        public IEnumerable<T> GetOutputValues()
        {
            return _outputs.Select(o => o.Value).ToList();
        }

        /*
         * Mark the outputs as failed
         */
        public void FailOutputs()
        {
            foreach (var output in _outputs)
            {
                output.Fail();
            }
        }

        /*
         * Check whether an output failed to be created by one of the program trees
         */
        public bool Failed()
        {
            return _outputs.Any(o => o.Failed);
        }
    }
}
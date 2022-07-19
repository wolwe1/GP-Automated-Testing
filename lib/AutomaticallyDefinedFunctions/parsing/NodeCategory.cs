namespace lib.AutomaticallyDefinedFunctions.parsing
{
    public class NodeCategory
    {
        private string Value { get; }

        private NodeCategory(string value)
        {
            Value = value;
        }

        public static string If => new NodeCategory("IF").Value;
        public static string Loop => new NodeCategory("LOOP").Value;
        public static string Add => new NodeCategory("ADD").Value;
        
        public static string ValueNode => new NodeCategory("V").Value;
        public static string Subtract => new NodeCategory("SUB").Value;
        public static string Multiplication => new NodeCategory("MULT").Value;
        public static string Division => new NodeCategory("DIV").Value;
        public static string State => new NodeCategory("STATE").Value;
        public static string NotNull => new NodeCategory("!NULL").Value;
        public static string Equal => new NodeCategory("=").Value;
        public static string LessThan => new NodeCategory("<").Value;
        public static string GreaterThan => new NodeCategory(">").Value;
        public static string NotEqual => new NodeCategory("!=").Value;
        public static string LengthOf => new NodeCategory("Len").Value;
        public static string FunctionDefinition => new NodeCategory("Func").Value;
        public static string ExecutionCount => new NodeCategory("ExecCount").Value;
        public static string LastOutput => new NodeCategory("LastOut").Value;
        public static string ProgramResponse => new NodeCategory("ProgResp").Value;
        public static string Random => new NodeCategory("Rand").Value;
        public static string OutputFailed => new NodeCategory("Fail").Value;
    }
}
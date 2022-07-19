namespace testObjects.source.capture
{
    public class Coverage
    {
        public readonly int NodeNumber;
        public readonly int EdgeNumber;
        public readonly NodeType NodeType;
        public readonly bool IsStart;
        public readonly bool IsEnd;

        public Coverage(int nodeNumber, int edgeNumber, NodeType nodeType,bool isStart,bool isEnd)
        {
            NodeNumber = nodeNumber;
            EdgeNumber = edgeNumber;
            NodeType = nodeType;
            IsStart = isStart;
            IsEnd = isEnd;
        }

        public bool IsStartNode()
        {
            return IsStart;
        }
    }
}
namespace Framework.Fibonnaci
{
    public class FibHeapNode<T>
    {
        public int Degree { get; set; }
        public double Key { get; set; }
        public bool Mark { get; set; }
        public FibHeapNode<T> Parent { get; set; }
        public FibHeapNode<T> Right { get; set; }
        public FibHeapNode<T> Left { get; set; }
        public FibHeapNode<T> Child { get; set; }
        public T Data { get; set; }

        public FibHeapNode(T data, double key)
        {
            Right = this;
            Left = this;
            Data = data;
            Key = key;
        }

        /// <summary>
        /// affichage du noeud
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return "key = " + Key + ", degree = " + Degree + ", mark = " + Mark;
        }
    }
}
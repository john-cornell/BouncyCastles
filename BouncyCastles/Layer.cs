using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public class Layer
    {
        public Layer(int layerLevel)
        {
            LayerLevel = layerLevel;
        }
        public int LayerLevel { get; private set; }

        public List<Node> Nodes = new List<Node>();

        public void Process(List<Node> inputNodes)
        {
            if (LayerLevel == 0)
                throw new InvalidOperationException("Layer Level 0 nodes cannot process previous nodes");

            Nodes.ForEach(n => n.Process(inputNodes));
        }

        public Node AddNode()
        {
            Node node = new Node(LayerLevel);
            Nodes.Add(node);

            return node;
        }
    }
}

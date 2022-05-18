using BouncyCastles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public enum LayerType { Input, Hidden, Output }

    public class Layer 
    {
        public LayerType LayerType { get; set; }
        public List<Node> Nodes { get; private set; }

        public Layer(MLPGenerationContext context)
        {
            Generate(context);
        }

        private void Generate(MLPGenerationContext context)
        {
            int nodeCount = context.NodeSizes[context.CurrentBuildNodeIndex];

            Nodes = new List<Node>(nodeCount);

            for (int i = 0; i< nodeCount; i++)
            {
                Nodes.Add(new Node(context));
            }
        }
    }
}

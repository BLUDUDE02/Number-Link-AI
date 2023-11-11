using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLink_AI
{
    internal class TreeLink
    {
        public Node FromNode, ToNode;
        public TreeLink(Node from_node, Node to_node)
        {
            FromNode = from_node;
            ToNode = to_node;
        }
    }
}

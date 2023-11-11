using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberLink_AI
{
    /// <summary>
    /// Node Helper Class
    /// </summary>
    public class Node
    {
        public Node Predecessor = null;
        public Vector2 Coords;
        public List<Node> Neighbors = new List<Node>();
        public Color Value;
        public int pathNum = -1;

        public Node(Vector2 coords, Color value)
        {
            this.Coords = coords;
            this.Value = value;
        }
    }
}

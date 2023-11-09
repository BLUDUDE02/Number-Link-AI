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
        public Vector2 Coords;
        public List<Node> Neighbors = new List<Node>();
        public int Value;

        public Node(Vector2 coords, int value)
        {
            this.Coords = coords;
            this.Value = value;
        }
    }
}

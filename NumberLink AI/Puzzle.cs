using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberLink_AI
{
    /// <summary>
    /// Helper class to handle puzzles with node pairs
    /// </summary>
    public class Puzzle
    {
        public Node[,] Nodes;
        public List<LinkedNodes> Pairs = new List<LinkedNodes>();
        public int height;
        public int width;

        public Puzzle (Bitmap bmp)
        {
            height = bmp.Height;
            width = bmp.Width;
            Nodes = new Node[height, width];
            List<Color> colors = new List<Color>();

            //Build All Nodes, add unique and non-transparent colors to a list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Node node = new Node(new Vector2(x, y), bmp.GetPixel(x, y));
                    Nodes[x, y] = node;
                    if(node.Value.Name != "0" && !colors.Contains(node.Value))
                    {
                        colors.Add(node.Value);
                    }
                }
            }

            // Initialize the nodes' neighbors.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x > 0)
                        Nodes[x, y].Neighbors.Add(Nodes[x - 1, y]);
                    if (x < width - 1)
                        Nodes[x, y].Neighbors.Add(Nodes[x + 1, y]);
                    if (y > 0)
                        Nodes[x, y].Neighbors.Add(Nodes[x, y - 1]);
                    if (y < height - 1)
                        Nodes[x, y].Neighbors.Add(Nodes[x, y + 1]);
                }
            }

            // Initialize each color pairing.
            for(int i = 0; i < colors.Count; i++)
            {
                LinkedNodes link = new LinkedNodes();
                link.ID = i;
                for(int x = 0; x < width; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        if (Nodes[x,y].Value == colors[i])
                        {
                            link.Nodes.Add(Nodes[x,y]);
                        }
                    }
                }
                Pairs.Add(link);
            }

        }
    }
}

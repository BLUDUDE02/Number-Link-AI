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
    public class Feeler
    {
        public List<Feeler> Feelers = new List<Feeler>();
        public int canMove = 1;
        public List<Node> Path = new List<Node>();
        public Color color;
        public int id;
        private Window Window;
        private int wid;
        private int hgt;
        private Puzzle Puzzle;
        private Node Start = null;
        private Node End = null;
        private Node currentNode = null;
        private Node lastNode = null;
        private Random Random = new Random();

        public Feeler(Window window, Puzzle puzzle, int id)
        {
            this.Puzzle = puzzle;
            this.Window = window;
            this.hgt = puzzle.height;
            this.wid = puzzle.width;
            this.Start = puzzle.Pairs[id].Nodes[0];
            this.End = puzzle.Pairs[id].Nodes[1];
            this.color = Start.Value;
            this.id = id;
        }

        /// <summary>
        /// Create the path
        /// </summary>
        public void FindEnd()
        {
            currentNode = Start;
            lastNode = currentNode;
            Path.Add(currentNode);
            canMove = 1;
            while(canMove == 1)
            {
                canMove = MakeAMove();
            }

            if(canMove == 0)
            {
                System.Diagnostics.Debug.WriteLine("Feeler (" + id.ToString() + ") Got Stuck!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Feeler (" + id.ToString() + ") Successfully Connected!");
            }
        }

        private int MakeAMove()
        {
            Node nextNode = null;
            //Get Current Neighbors not in path
            List<Node> possibleNodes = currentNode.Neighbors.
                OrderBy(n => GetDistance(n, End)).
                Where(n => !ContainsNode(n)).ToList();
            if(possibleNodes.Count > 0)
            {
                //We may have multiple equidistent nodes
                List<Node> closestNodes = new List<Node>();
                int currentdistance = GetDistance(possibleNodes[0], End);
                foreach (Node node in possibleNodes)
                {
                    if (GetDistance(node, End) <= currentdistance)
                    {
                        closestNodes.Add(node);
                    }
                }

                if (closestNodes.Count >= 1)
                {
                    nextNode = closestNodes[Random.Next(closestNodes.Count - 1)];
                }
                else
                {
                    nextNode = closestNodes[0];
                }

                lastNode = currentNode;
                Path.Add(nextNode);
                currentNode = nextNode;

                if(currentNode != End) 
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }    
        }

        private bool ContainsNode(Node n)
        {
            if (n != Start && n!= End)
            {
                foreach (LinkedNodes link in Puzzle.Pairs)
                {
                    if (link.Nodes.Contains(n))
                    {
                        return true;
                    }
                }
                foreach (Feeler feeler in Feelers)
                {
                    if (feeler.Path.Contains(n))
                    {
                        return true;
                    }
                }
            }
            if (this.Path.Contains(n))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get Distance Between Two Nodes (Manhattan)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int GetDistance(Node a, Node b)
        {
            int dist = (int)Math.Ceiling(Math.Abs(a.Coords.X - b.Coords.X) + Math.Abs(a.Coords.Y - b.Coords.Y));
            return dist;
        }
    }
}

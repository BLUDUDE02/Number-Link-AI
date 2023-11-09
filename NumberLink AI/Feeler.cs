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
        public int Target;
        public List<Feeler> Feelers = new List<Feeler>();
        public int canMove = 1;
        private Window Window;
        private int wid;
        private int hgt;
        private Node[,] Puzzle;
        private Node Start = null;
        private Node currentNode;
        private Node lastNode;
        private Node End = null;
        private List<Node> Path = new List<Node>();
        private Random Random = new Random();

        public Feeler(Window window, Node[,] puzzle, int target)
        {
            this.Puzzle = puzzle;
            this.Target = target;
            this.Window = window;
            this.hgt = puzzle.GetUpperBound(0);
            this.wid = puzzle.GetUpperBound(1);

            for (int y = 0; y < hgt; y++)
            {
                for (int x = 0; x < wid; x++)
                {
                    if (puzzle[x,y].Value == this.Target)
                    {
                        if(Start == null)
                        {
                            Start = puzzle[x,y];
                        }
                        else
                        {
                            End = puzzle[x, y];
                        }
                    }
                }
            }
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
                System.Diagnostics.Debug.WriteLine("Feeler (" + Target + ") Got Stuck!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Feeler (" + Target + ") Got Won!");
            }
        }

        private int MakeAMove()
        {
            Node nextNode = null;
            //Get Current Neighbors not in path
            List<Node> possibleNodes = currentNode.Neighbors.
                OrderBy(n => GetDistance(n, End)).
                Where(n => !ContainsNode(n)).ToList();
            //We may have multiple equidistent nodes
            List<Node> closestNodes = new List<Node>();
            int currentdistance = GetDistance(possibleNodes[0], End);
            if(possibleNodes.Count > 0)
            {
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
            foreach(Feeler feeler in Feelers)
            {
                if(feeler.Path.Contains(n))
                {
                    return true;
                }
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

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
        public List<Node> VisitedNodes = new List<Node>();
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
            
            int success = SolveWithSpanningTree(Start);

            if(success == 0)
            {
                System.Diagnostics.Debug.WriteLine("Feeler (" + id.ToString() + ") Could Not Find A Path!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Feeler (" + id.ToString() + ") Successfully Connected!");
            }
        }

        /// <summary>
        /// Greedy algorithm for navigating one space in a maze.
        /// </summary>
        /// <returns></returns>
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

        private int SolveWithSpanningTree(Node root)
        {
            CleanPuzzle();
            VisitedNodes = new List<Node>();
            Random rand = new Random();

            // Set the root node's predecessor so we know it's in the tree.
            root.Predecessor = root;
            VisitedNodes.Add(root);

            // Make a list of candidate links.
            List<TreeLink> links = new List<TreeLink>();

            // Add the root's valid neighbors to the links list.
            foreach (Node neighbor in root.Neighbors.Where(n => !ContainsNode(n)).ToList())
            {
                if (neighbor != null)
                    links.Add(new TreeLink(root, neighbor));
            }

            // Add the other nodes to the tree.
            while (links.Count > 0)
            {
                // Pick the first link.
                int link_num = 0;
                TreeLink link = links[link_num];
                links.RemoveAt(link_num);

                // Add this link to the tree.
                Node to_node = link.ToNode;
                link.ToNode.Predecessor = link.FromNode;
                VisitedNodes.Add(to_node);

                // Remove any links from the list that point
                // to nodes that are already in the tree.
                // (That will be the newly added node.)
                for (int i = links.Count - 1; i >= 0; i--)
                {
                    if (links[i].ToNode.Predecessor != null)
                        links.RemoveAt(i);
                }

                // Add to_node's links to the links list.
                foreach (Node neighbor in to_node.Neighbors.Where(n => !ContainsNode(n)).ToList())
                {
                    if ((neighbor != null) && (neighbor.Predecessor == null))
                        links.Add(new TreeLink(to_node, neighbor));
                }
            }
            if(VisitedNodes.Contains(End))
            {
                //End Found! Build the path using the predecessors
                Node newNode = End;
                while(newNode != Start)
                {
                    Path.Add(newNode);
                    newNode = newNode.Predecessor;
                }
                Path.Add(newNode.Predecessor);
                Path.Reverse();
                return 2;
            }
            else
            {
                //No valid path! return 0.
                return 0;
            }

        }

        /// <summary>
        /// Wipe every predecessor from the puzzle
        /// </summary>
        private void CleanPuzzle()
        {
            for(int x = 0; x < Puzzle.width; x++)
            {
                for(int y = 0; y < Puzzle.height; y++)
                {
                    Puzzle.Nodes[x, y].Predecessor = null;
                }
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

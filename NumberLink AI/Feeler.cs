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
        public int canMove = 1;
        public List<Node> Path = new List<Node>();
        public List<Node> VisitedNodes = new List<Node>();
        public Color color;
        public int id;
        private Puzzle Puzzle;
        private Node Start = null;
        private Node End = null;
        public List<(int, int)> Blockers = new List<(int, int)>();
        public List<Feeler> feelers = new List<Feeler>();

        public Feeler(Puzzle puzzle, int id)
        {
            this.Puzzle = puzzle;
            this.Start = puzzle.Pairs[id].Nodes[0];
            this.End = puzzle.Pairs[id].Nodes[1];
            this.color = Start.Value;
            this.id = id;
        }

        /// <summary>
        /// Create the path
        /// </summary>
        public int FindEnd()
        {
            return SolveWithSpanningTree(Start);
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
            foreach (Node neighbor in root.Neighbors.Where(n => !ContainsNode(n,0)).ToList())
            {
                if (neighbor != null)
                    links.Add(new TreeLink(root, neighbor));
            }

            // Add the other nodes to the tree.
            while (links.Count > 0)
            {
                // Pick the best link.
                int link_num = 0;
                for(int i = 0; i < links.Count(); i++)
                {
                    //stick to paths
                    int count = links[i].ToNode.Neighbors.Where(n => ContainsNode(n,0)).ToList().Count();
                    if (count == 2)
                    {
                        link_num = i;
                        break;
                    }
                    //Avoid terminal nodes that havent run yet
                    //count = links[i].ToNode.Neighbors.Where(n => ContainsNode(n, 1)).ToList().Count();
                    //if (count == 0)
                    //{
                    //    link_num = i;
                    //    break;
                    //}
                }
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
                foreach (Node neighbor in to_node.Neighbors.Where(n => !ContainsNode(n,0)).ToList())
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
                return 1;
            }
            else
            {
                //No valid path! return 0.
                //Add to the Blockers List.
                foreach(Node neighbor in VisitedNodes.Last().Neighbors)
                {
                    foreach (Feeler feeler in feelers)
                    {
                        if (feeler.Path.Contains(neighbor) && feeler != this)
                        {
                            if(!Blockers.Select(o => o.Item1).ToList().Contains(feeler.id))
                            {
                                Blockers.Add((feeler.id, this.id));
                            }
                        }
                    }  
                }

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

        /// <summary>
        /// Check for unavailable closest nodes. select 0 for all, 1 for just endpoints, and 2 for just edges, and 3 for endpoints yet to run
        /// </summary>
        /// <param name="n"></param>
        /// <param name="choice"></param>
        /// <returns></returns>
        private bool ContainsNode(Node n, int choice)
        {
            if (n != Start && n!= End)
            {
                if(choice == 0 || choice == 1)
                {
                    foreach (LinkedNodes link in Puzzle.Pairs)
                    {
                        if (link.Nodes.Contains(n))
                        {
                            return true;
                        }
                    }
                }
                if(choice == 0 || choice == 2)
                {
                    foreach (Feeler feeler in feelers)
                    {
                        if (feeler.Path.Contains(n))
                        {
                            return true;
                        }
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

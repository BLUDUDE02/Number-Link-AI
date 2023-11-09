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
    public partial class Window : Form
    {
        #region Variables
        public int puzzle = 1;
        private int population = 0;
        private int generations = 0;
        private int mutationRate = 0;
        private int fitness = 0;
        private Node[,] nodes;
        List<Node> globalSolution = new List<Node>();
        List<Feeler> feelers = new List<Feeler>();

        private Timer timer1;

        private static Color[] colors = {Color.Red, Color.Orange,
            Color.Yellow, Color.Green, Color.Blue, Color.Cyan, Color.Magenta};

        #endregion

        #region Puzzle Declarations
        static int[,] puzzle1 = { { 2, 0, 0, 0, 3 },
                                  { 0, 0, 0, 0, 0 },
                                  { 0, 1, 4, 0, 0 },
                                  { 0, 0, 2, 0, 0 },
                                  { 4, 1, 3, 0, 0 } };

        static int[,] puzzle2 = { { 0, 0, 0, 0, 0 },
                                  { 2, 1, 0, 1, 2 },
                                  { 3, 0, 0, 0, 0 },
                                  { 0, 5, 0, 4, 0 },
                                  { 3, 4, 0, 5, 0 } };

        static int[,] puzzle3 = { { 0, 0, 0, 0, 0, 0 },
                                  { 5, 1, 3, 2, 0, 0 },
                                  { 1, 0, 0, 4, 0, 0 },
                                  { 4, 0, 0, 0, 0, 5 },
                                  { 0, 3, 0, 0, 2, 6 },
                                  { 0, 0, 0, 6, 0, 0 } };

        static int[,] puzzle4 = { { 0, 0, 0, 0, 0, 0, 1 },
                                  { 0, 3, 2, 6, 0, 4, 3 },
                                  { 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 5, 0, 0, 0, 0 },
                                  { 0, 0, 0, 2, 0, 0, 0 },
                                  { 0, 0, 0, 5, 1, 4, 0 },
                                  { 6, 0, 0, 0, 0, 0, 0 }};

        /// <summary>
        /// Convert int list to nodes (for clarity).
        /// </summary>
        /// <param name="puzzle"></param>
        /// <returns></returns>
        private static Node[,] ConvertToNodes(int[,] puzzle)
        {
            int hgt = puzzle.GetUpperBound(0)+1;
            int wid = puzzle.GetUpperBound(1)+1;
            Node[,] nodes = new Node[hgt, wid];
            //Create Nodes
            for (int y = 0; y < hgt; y++)
            {
                for (int x = 0; x < wid; x++)
                {
                    nodes[x, y] = new Node(new Vector2(x, y), puzzle[x, y]);
                }
            }

            // Initialize the nodes' neighbors.
            for (int r = 0; r < hgt; r++)
            {
                for (int c = 0; c < wid; c++)
                {
                    if (r > 0)
                        nodes[r, c].Neighbors.Add(nodes[r - 1, c]);
                    if (r < hgt - 1)
                        nodes[r, c].Neighbors.Add(nodes[r + 1, c]);
                    if (c > 0)
                        nodes[r, c].Neighbors.Add(nodes[r, c - 1]);
                    if (c < wid - 1)
                        nodes[r, c].Neighbors.Add(nodes[r, c + 1]);
                }
            }

            return nodes;
        }

        new readonly Dictionary<int, Node[,]> puzzles = new Dictionary<int, Node[,]>
        {
            {1, ConvertToNodes(puzzle1)},
            {2, ConvertToNodes(puzzle2)},
            {3, ConvertToNodes(puzzle3)},
            {4, ConvertToNodes(puzzle4)}
        };
        #endregion


        public Window()
        {
            InitializeComponent();
            InitializePuzzles();
        }

        #region UI

        #region Puzzle Selection
        /// <summary>
        /// Handles Selecting Puzzle 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preview1_Click(object sender, EventArgs e)
        {
            puzzle = 1;
            feelers = new List<Feeler>();
            InitializePuzzles();
        }
        /// <summary>
        /// Handles Selecting Puzzle 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preview2_Click(object sender, EventArgs e)
        {
            puzzle = 2;
            feelers = new List<Feeler>();
            InitializePuzzles();
        }
        /// <summary>
        /// Handles Selecting Puzzle 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preview3_Click(object sender, EventArgs e)
        {
            puzzle = 3; 
            feelers = new List<Feeler>();
            InitializePuzzles();
        }
        /// <summary>
        /// Handle Selecting Puzzle 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preview4_Click(object sender, EventArgs e)
        {
            puzzle = 4;
            feelers = new List<Feeler>();
            InitializePuzzles();
        }
        #endregion

        #region Synchronous Functions
        //Initialize Timer.
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 200; // in miliseconds
            timer1.Start();
        }

        //Timer Update Function.
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            DrawPuzzle(MainFrame, puzzles[puzzle]);
        }

        #endregion


        /// <summary>
        /// Handle Game Start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                ErrorProvider.Clear();
                population = int.Parse(PopIn.Text);
                generations = int.Parse(GenIn.Text);
                mutationRate = int.Parse(MutIn.Text);
                fitness = int.Parse(FitIn.Text);
                TestCase();
            }
        }

        private void InitializePuzzles()
        {
            DrawPuzzle(preview1, puzzles[1]);
            DrawPuzzle(preview2, puzzles[2]);
            DrawPuzzle(preview3, puzzles[3]);
            DrawPuzzle(preview4, puzzles[4]);
        }

        /// <summary>
        /// Draw a numberlink grid in a given picturebox, and a solution, if given.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="grid"></param>
        /// <param name="solution"></param>
        private void DrawPuzzle(PictureBox image, Node[,] grid)
        {
            int hgt = grid.GetUpperBound(0) + 1;
            int wid = grid.GetUpperBound(1) + 1;

            int CellWid = image.Width / (wid + 2);
            int CellHgt = image.Height / (hgt + 2);

            if (CellWid > CellHgt) CellWid = CellHgt;
            else CellHgt = CellWid;
            int Xmin = (image.Width - wid * CellWid) / 2;
            int Ymin = (image.Height - hgt * CellHgt) / 2;

            Bitmap bm = new Bitmap(
                image.Width,
                image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                Rectangle rect = new Rectangle(2, 2, image.Width - 4, image.Height - 4);
                SolidBrush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(Color.Brown, 6);

                //Draw grid nodes.
                gr.FillRectangle(brush, rect);
                for (int y = 0; y < hgt; y++)
                {
                    int ycoord = y * CellHgt + Ymin;
                    for (int x = 0; x < wid; x++)
                    {
                        int xcoord = x * CellWid + Xmin;
                        rect = new Rectangle(xcoord - 1, ycoord - 1, CellWid - 2, CellHgt - 2);
                        if (grid[x, y].Value != 0)
                        {
                            brush = new SolidBrush(colors[grid[x, y].Value - 1]);
                            gr.FillEllipse(brush, rect);
                        }
                    }
                }

                //Draw solution
                if(image.Name == "MainFrame" && feelers != null && feelers.Count > 0)
                {
                    foreach(Feeler feeler in feelers)
                    {
                        for (int j = 1; j < feeler.Path.Count; j++)
                        {
                            Pen FeelerPen = new Pen(colors[feeler.Target-1], 8);
                            Point A = new Point((int)(feeler.Path[j].Coords.X * CellWid + Xmin + CellWid / 2), 
                                (int)(feeler.Path[j].Coords.Y * CellHgt + Ymin + CellHgt / 2));
                            Point B = new Point((int)(feeler.Path[j-1].Coords.X * CellWid + Xmin + CellWid / 2),
                                (int)(feeler.Path[j-1].Coords.Y * CellHgt + Ymin + CellHgt / 2));
                            gr.DrawLine(FeelerPen,A, B);
                        }
                    }
                }

                //Highlight selected box
                rect = new Rectangle(0, 0, image.Width, image.Height);
                switch (image.Name)
                {
                    case "preview1":
                        if (puzzle == 1)
                        {
                            gr.DrawRectangle(pen, rect);
                        }
                        break;
                    case "preview2":
                        if (puzzle == 2)
                        {
                            gr.DrawRectangle(pen, rect);
                        }
                        break;
                    case "preview3":
                        if (puzzle == 3)
                        {

                            gr.DrawRectangle(pen, rect);
                        }
                        break;
                    case "preview4":
                        if (puzzle == 4)
                        {
                            gr.DrawRectangle(pen, rect);
                        }
                        break;
                    default:
                        break;
                }

            }
            image.Image = bm;
        }

        #region Error Handling
        /// <summary>
        /// Validate all possible UI errors
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputs()
        {
            //this boolean lets us still fail, but highlight all errors.
            bool test = true;

            ErrorProvider.Clear();

            //If selected puzzle is invalid
            //(should be impossible, but hey ho).
            if (puzzle < 0 || puzzle > 4)
            {
                MessageBox.Show("FATAL ERROR :(");
                Application.Exit();
            }

            //If any text boxes are empty.
            if (PopIn.Text == String.Empty)
            {
                ErrorProvider.SetError(PopIn, "Please enter a value here.");
                test = false;
            }

            if (GenIn.Text == String.Empty)
            {
                ErrorProvider.SetError(GenIn, "Please enter a value here.");
                test = false;
            }

            if (MutIn.Text == String.Empty)
            {
                ErrorProvider.SetError(MutIn, "Please enter a value here.");
                test = false;
            }

            if (FitIn.Text == String.Empty)
            {
                ErrorProvider.SetError(FitIn, "Please enter a value here.");
                test = false;
            }

            //If the Population Box is < 2
            if (PopIn.Text != String.Empty && int.Parse(PopIn.Text) < 2)
            {
                ErrorProvider.SetError(PopIn, "Please enter value greater than 1.");
                test = false;
            }

            //If the Generation Box is < 1
            if (GenIn.Text != String.Empty && int.Parse(GenIn.Text) < 1)
            {
                ErrorProvider.SetError(GenIn, "Please enter value greater than 0.");
                test = false;
            }

            //If the Mutation Box is < 1
            if (MutIn.Text != String.Empty && int.Parse(MutIn.Text) < 1)
            {
                ErrorProvider.SetError(MutIn, "Please enter value greater than 0.");
                test = false;
            }

            //If the Mutation Box is > 100
            if (MutIn.Text != String.Empty && int.Parse(MutIn.Text) > 100)
            {
                ErrorProvider.SetError(MutIn, "Please enter value less than 100.");
                test = false;
            }

            //If the Fitness Box is < 2
            if (FitIn.Text != String.Empty && int.Parse(FitIn.Text) < 2)
            {
                ErrorProvider.SetError(FitIn, "Please enter value greater than 1.");
                test = false;
            }

            //If the Fitness Box is > Population Size!
            if ((FitIn.Text != String.Empty && PopIn.Text != String.Empty) &&
                int.Parse(FitIn.Text) > int.Parse(PopIn.Text) + 1)
            {
                ErrorProvider.SetError(FitIn, "Please enter value less than " +
                    (int.Parse(PopIn.Text) + 1) + ".");
                test = false;
            }

            return test;
        }
        #endregion

        #endregion

        #region Game
        void TestCase()
        {
            feelers = new List<Feeler>();
            Node[,] nodes = puzzles[puzzle];
            int numfeelers = 0;
            switch(puzzle)
            {
                case 1:
                    numfeelers = 4; 
                    break;
                case 2:
                    numfeelers = 5;
                    break;
                case 3:
                    numfeelers = 6;
                    break;
                case 4:
                    numfeelers = 6;
                    break;
                default: 
                    break;
            }

            for(int i = 1; i < numfeelers+1; i++)
            {
                feelers.Add(new Feeler(this, nodes, i));
            }

            foreach(Feeler feeler in feelers)
            {
                feeler.Feelers = feelers;
                feeler.FindEnd();
            }
            InitTimer();
        }
        #endregion
    }
}
using System.Diagnostics.Eventing.Reader;

namespace NumberLink_AI
{
    public partial class Window : Form
    {
        public int puzzle = 1;
        private int population = 0;
        private int generations = 0;
        private int mutationRate = 0;
        private int fitness = 0;

        private static Color[] colors = {Color.Red, Color.Orange,
            Color.Yellow, Color.Green, Color.Blue, Color.Cyan, Color.Magenta};

        #region Puzzle Declarations
        static int[,] puzzle1 = { { 5, 0, 0, 0, 3 },
                           { 0, 0, 0, 0, 0 },
                           { 0, 1, 4, 0, 0 },
                           { 0, 0, 5, 0, 0 },
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

        static int[,] puzzle4 = { { 0, 0, 0, 0, 2, 7, 2, 0 },
                           { 0, 0, 0, 0, 1, 0, 6, 0 },
                           { 0, 0, 4, 0, 0, 0, 0, 0 },
                           { 0, 0, 0, 0, 0, 7, 6, 0 },
                           { 0, 0, 4, 0, 0, 0, 0, 0 },
                           { 0, 0, 0, 0, 0, 0, 0, 0 },
                           { 0, 5, 3, 1, 0, 0, 0, 0 },
                           { 0, 0, 0, 0, 0, 0, 0, 0 }};

        new readonly Dictionary<int, int[,]> puzzles = new Dictionary<int, int[,]>
        {
            {1, puzzle1},
            {2, puzzle2},
            {3, puzzle3},
            {4, puzzle4}
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
            InitializePuzzles();
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

                MessageBox.Show("Selected Puzzle: " + puzzle +
                    "\nPopulation Size: " + population +
                    "\nGeneration Size: " + generations +
                    "\nMutation Rate: " + mutationRate +
                    "\nFitness Size: " + fitness);
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
        private void DrawPuzzle(PictureBox image, int[,] grid, int[,]? solution = null)
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
                        if (grid[x, y] != 0)
                        {
                            brush = new SolidBrush(colors[grid[x, y] - 1]);
                            gr.FillEllipse(brush, rect);
                        }
                    }
                }

                //Draw solution (NOT IMPLEMENTED)

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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private int population = 0;
        private int generations = 0;
        private int mutationRate = 0;
        private int fitness = 0;

        private List<Feeler> feelers = new List<Feeler>();
        private Puzzle puzzle = null;
        private Timer timer1;
        #endregion

        public Window()
        {
            InitializeComponent();
            InitTimer();
        }

        #region UI

        #region Synchronous Functions
        //Initialize Timer.
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 10; // in miliseconds
            timer1.Start();
        }

        //Timer Update Function.
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (this.WindowState != FormWindowState.Minimized && puzzle != null)
            {
                DrawPuzzle(MainFrame, puzzle);
            }
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
                TestCase(puzzle);
            }
        }

        /// <summary>
        /// Handle Puzzle Image Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileDialogButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathText.Text = openFileDialog.FileName;
                Bitmap bitmap = new Bitmap(FilePathText.Text);
                puzzle = new Puzzle(bitmap);
                UpdateUI();
            }
        }

        /// <summary>
        /// Draw a numberlink grid in a given picturebox, and a solution, if given.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="grid"></param>
        /// <param name="solution"></param>
        private void DrawPuzzle(PictureBox image, Puzzle puzzle)
        {
            int hgt = puzzle.height;
            int wid = puzzle.width;

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

                gr.FillRectangle(brush, rect);
                //Draw solution
                if (image.Name == "MainFrame" && feelers != null && feelers.Count > 0)
                {
                    foreach (Feeler feeler in feelers)
                    {
                        for (int j = 1; j < feeler.Path.Count; j++)
                        {
                            Pen FeelerPen = new Pen(feeler.color);
                            Point B = new Point((int)(feeler.Path[j].Coords.X * CellWid + Xmin + CellWid / 2),
                                (int)(feeler.Path[j].Coords.Y * CellHgt + Ymin + CellHgt / 2));
                            Point A = new Point((int)(feeler.Path[j - 1].Coords.X * CellWid + Xmin + CellWid / 2),
                                (int)(feeler.Path[j - 1].Coords.Y * CellHgt + Ymin + CellHgt / 2));
                            if (j == feeler.Path.Count - 1)
                            {
                                FeelerPen.CustomEndCap = new AdjustableArrowCap(5, 5);
                            }
                            gr.DrawLine(FeelerPen, A, B);
                        }
                    }
                }

                //Draw grid nodes.
                foreach (LinkedNodes link in puzzle.Pairs)
                {
                    foreach (Node node in link.Nodes)
                    {
                        int ycoord = (int)node.Coords.Y * CellHgt + Ymin;
                        int xcoord = (int)node.Coords.X * CellWid + Xmin;
                        brush = new SolidBrush(node.Value);
                        Color color2 = ControlPaint.Light(node.Value, 1);
                        rect = new Rectangle(xcoord + 4, ycoord + 4, CellWid - 8, CellHgt - 8);
                        gr.FillEllipse(brush, rect);
                        if (node == link.Nodes[0])
                        {
                            gr.DrawEllipse(new Pen(color2, 4), rect);
                        }
                        Font font = new Font("Arial", (float)CellHgt / 3);
                        System.Drawing.SizeF stringSize = gr.MeasureString(link.ID.ToString(), font);
                        int posX = Convert.ToInt32((rect.Width - stringSize.Width) / 2 + rect.X);
                        int posY = Convert.ToInt32((rect.Height - stringSize.Height) / 2 + rect.Y);
                        gr.DrawString(link.ID.ToString(), font, new SolidBrush(node.Value.GetBrightness() > 0.3f ? Color.Black : Color.White),
                            new System.Drawing.Point(posX, posY));
                    }
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

            //If any text boxes are empty.
            if (FilePathText.Text == String.Empty)
            {
                ErrorProvider.SetError(FilePathText, "Please select a file");
                test = false;
            }

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

            //If the Fitness Box is > Population Size + 1
            if ((FitIn.Text != String.Empty && PopIn.Text != String.Empty) &&
                int.Parse(FitIn.Text) > int.Parse(PopIn.Text) + 1)
            {
                ErrorProvider.SetError(FitIn, "Please enter value less than " +
                    (int.Parse(PopIn.Text) + 1) + ".");
                test = false;
            }

            //If selected puzzle image is invalid.
            if (FilePathText.Text != String.Empty)
            {
                if (new Bitmap(FilePathText.Text).Height > 32)
                {
                    ErrorProvider.SetError(FilePathText, "Please select an image smaller than 32x32.");
                    test = false;
                }
            }

            return test;
        }
        #endregion

        #endregion

        #region Game Functions
        void TestCase(Puzzle puzzle)
        {
            feelers = new List<Feeler>();
            int numfeelers = puzzle.Pairs.Count();

            for (int i = 0; i < numfeelers; i++)
            {
                feelers.Add(new Feeler(this, puzzle, i));
            }

            foreach (Feeler feeler in feelers)
            {
                feeler.Feelers = feelers;
                feeler.FindEnd();
            }
        }
        #endregion
    }
}
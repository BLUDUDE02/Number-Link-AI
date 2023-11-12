using System.Drawing;
using System.Windows.Forms;

namespace NumberLink_AI
{
    partial class Window
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MainFrame = new PictureBox();
            GeneticAlgorithmGroup = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            PopLbl = new Label();
            GenLbl = new Label();
            MutLbl = new Label();
            FitLbl = new Label();
            FitIn = new MaskedTextBox();
            PopIn = new MaskedTextBox();
            GenIn = new MaskedTextBox();
            MutIn = new MaskedTextBox();
            ResultsBox = new GroupBox();
            checkBox1 = new CheckBox();
            StartButton = new Button();
            ErrorProvider = new ErrorProvider(components);
            openFileDialog = new OpenFileDialog();
            FileDialogButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            FilePathText = new TextBox();
            ((System.ComponentModel.ISupportInitialize)MainFrame).BeginInit();
            GeneticAlgorithmGroup.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ResultsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // MainFrame
            // 
            MainFrame.BackColor = Color.Black;
            MainFrame.Dock = DockStyle.Fill;
            MainFrame.Location = new Point(28, 32);
            MainFrame.Margin = new Padding(28, 32, 28, 32);
            MainFrame.Name = "MainFrame";
            MainFrame.Padding = new Padding(9, 11, 9, 11);
            MainFrame.Size = new Size(1134, 960);
            MainFrame.TabIndex = 4;
            MainFrame.TabStop = false;
            // 
            // GeneticAlgorithmGroup
            // 
            GeneticAlgorithmGroup.AutoSize = true;
            GeneticAlgorithmGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GeneticAlgorithmGroup.BackColor = Color.Linen;
            GeneticAlgorithmGroup.Controls.Add(tableLayoutPanel3);
            GeneticAlgorithmGroup.Dock = DockStyle.Fill;
            GeneticAlgorithmGroup.ForeColor = Color.SaddleBrown;
            GeneticAlgorithmGroup.Location = new Point(6, 111);
            GeneticAlgorithmGroup.Margin = new Padding(6);
            GeneticAlgorithmGroup.MaximumSize = new Size(27857, 32000);
            GeneticAlgorithmGroup.Name = "GeneticAlgorithmGroup";
            GeneticAlgorithmGroup.Padding = new Padding(6);
            GeneticAlgorithmGroup.Size = new Size(553, 248);
            GeneticAlgorithmGroup.TabIndex = 5;
            GeneticAlgorithmGroup.TabStop = false;
            GeneticAlgorithmGroup.Text = "Parameters";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AllowDrop = true;
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(PopLbl, 0, 0);
            tableLayoutPanel3.Controls.Add(GenLbl, 0, 1);
            tableLayoutPanel3.Controls.Add(MutLbl, 0, 2);
            tableLayoutPanel3.Controls.Add(FitLbl, 0, 3);
            tableLayoutPanel3.Controls.Add(FitIn, 1, 3);
            tableLayoutPanel3.Controls.Add(PopIn, 1, 0);
            tableLayoutPanel3.Controls.Add(GenIn, 1, 1);
            tableLayoutPanel3.Controls.Add(MutIn, 1, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel3.Location = new Point(6, 38);
            tableLayoutPanel3.Margin = new Padding(6);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(541, 204);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // PopLbl
            // 
            PopLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            PopLbl.AutoSize = true;
            PopLbl.Location = new Point(20, 0);
            PopLbl.Margin = new Padding(6, 0, 6, 0);
            PopLbl.Name = "PopLbl";
            PopLbl.Size = new Size(128, 51);
            PopLbl.TabIndex = 0;
            PopLbl.Text = "Population";
            PopLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // GenLbl
            // 
            GenLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GenLbl.AutoSize = true;
            GenLbl.Location = new Point(6, 51);
            GenLbl.Margin = new Padding(6, 0, 6, 0);
            GenLbl.Name = "GenLbl";
            GenLbl.Size = new Size(142, 51);
            GenLbl.TabIndex = 1;
            GenLbl.Text = "Generations";
            GenLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MutLbl
            // 
            MutLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            MutLbl.AutoSize = true;
            MutLbl.Location = new Point(9, 102);
            MutLbl.Margin = new Padding(6, 0, 6, 0);
            MutLbl.Name = "MutLbl";
            MutLbl.Size = new Size(139, 51);
            MutLbl.TabIndex = 4;
            MutLbl.Text = "Mutation %";
            MutLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FitLbl
            // 
            FitLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            FitLbl.AutoSize = true;
            FitLbl.Location = new Point(61, 153);
            FitLbl.Margin = new Padding(6, 0, 6, 0);
            FitLbl.Name = "FitLbl";
            FitLbl.Size = new Size(87, 51);
            FitLbl.TabIndex = 6;
            FitLbl.Text = "Fitness";
            FitLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FitIn
            // 
            FitIn.BackColor = Color.Snow;
            FitIn.Dock = DockStyle.Fill;
            FitIn.ForeColor = Color.SaddleBrown;
            FitIn.Location = new Point(160, 159);
            FitIn.Margin = new Padding(6, 6, 59, 6);
            FitIn.Mask = "000";
            FitIn.Name = "FitIn";
            FitIn.RightToLeft = RightToLeft.Yes;
            FitIn.Size = new Size(322, 39);
            FitIn.TabIndex = 7;
            FitIn.Text = "10";
            // 
            // PopIn
            // 
            PopIn.BackColor = Color.Snow;
            PopIn.Dock = DockStyle.Fill;
            PopIn.ForeColor = Color.SaddleBrown;
            PopIn.Location = new Point(160, 6);
            PopIn.Margin = new Padding(6, 6, 59, 6);
            PopIn.Mask = "000";
            PopIn.Name = "PopIn";
            PopIn.RightToLeft = RightToLeft.Yes;
            PopIn.Size = new Size(322, 39);
            PopIn.TabIndex = 2;
            PopIn.Text = "10";
            // 
            // GenIn
            // 
            GenIn.BackColor = Color.Snow;
            GenIn.Dock = DockStyle.Fill;
            GenIn.ForeColor = Color.SaddleBrown;
            GenIn.Location = new Point(160, 57);
            GenIn.Margin = new Padding(6, 6, 59, 6);
            GenIn.Mask = "000";
            GenIn.Name = "GenIn";
            GenIn.RightToLeft = RightToLeft.Yes;
            GenIn.Size = new Size(322, 39);
            GenIn.TabIndex = 3;
            GenIn.Text = "10";
            // 
            // MutIn
            // 
            MutIn.BackColor = Color.Snow;
            MutIn.Dock = DockStyle.Fill;
            MutIn.ForeColor = Color.SaddleBrown;
            MutIn.Location = new Point(160, 108);
            MutIn.Margin = new Padding(6, 6, 59, 6);
            MutIn.Mask = "000";
            MutIn.Name = "MutIn";
            MutIn.RightToLeft = RightToLeft.Yes;
            MutIn.Size = new Size(322, 39);
            MutIn.TabIndex = 5;
            MutIn.Text = "10";
            // 
            // ResultsBox
            // 
            ResultsBox.AutoSize = true;
            ResultsBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ResultsBox.BackColor = Color.Linen;
            ResultsBox.Controls.Add(checkBox1);
            ResultsBox.Dock = DockStyle.Fill;
            ResultsBox.ForeColor = Color.SaddleBrown;
            ResultsBox.Location = new Point(6, 371);
            ResultsBox.Margin = new Padding(6);
            ResultsBox.MaximumSize = new Size(27857, 32000);
            ResultsBox.Name = "ResultsBox";
            ResultsBox.Padding = new Padding(6);
            ResultsBox.Size = new Size(553, 115);
            ResultsBox.TabIndex = 8;
            ResultsBox.TabStop = false;
            ResultsBox.Text = "Options";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(9, 41);
            checkBox1.Margin = new Padding(0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(247, 36);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Wisdom of Crowds";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            StartButton.AutoSize = true;
            StartButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            StartButton.BackColor = Color.Snow;
            StartButton.Dock = DockStyle.Fill;
            StartButton.ForeColor = Color.SaddleBrown;
            StartButton.Location = new Point(6, 498);
            StartButton.Margin = new Padding(6);
            StartButton.MaximumSize = new Size(27857, 107);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(553, 107);
            StartButton.TabIndex = 9;
            StartButton.Text = "Start!";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartButton_Click;
            // 
            // ErrorProvider
            // 
            ErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ErrorProvider.ContainerControl = this;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Portable Network Graphics|*.png";
            // 
            // FileDialogButton
            // 
            FileDialogButton.AutoSize = true;
            FileDialogButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FileDialogButton.BackColor = Color.Snow;
            FileDialogButton.Dock = DockStyle.Fill;
            FileDialogButton.ForeColor = Color.SaddleBrown;
            FileDialogButton.Location = new Point(6, 57);
            FileDialogButton.Margin = new Padding(6);
            FileDialogButton.MaximumSize = new Size(27857, 107);
            FileDialogButton.Name = "FileDialogButton";
            FileDialogButton.Size = new Size(553, 42);
            FileDialogButton.TabIndex = 11;
            FileDialogButton.Text = "Browse";
            FileDialogButton.UseVisualStyleBackColor = false;
            FileDialogButton.Click += FileDialogButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            tableLayoutPanel1.Controls.Add(MainFrame, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1642, 1024);
            tableLayoutPanel1.TabIndex = 12;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(FileDialogButton, 0, 4);
            tableLayoutPanel2.Controls.Add(StartButton, 0, 7);
            tableLayoutPanel2.Controls.Add(ResultsBox, 0, 6);
            tableLayoutPanel2.Controls.Add(GeneticAlgorithmGroup, 0, 5);
            tableLayoutPanel2.Controls.Add(FilePathText, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel2.Location = new Point(1199, 11);
            tableLayoutPanel2.Margin = new Padding(9, 11, 9, 11);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 8;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(434, 1002);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // FilePathText
            // 
            FilePathText.Dock = DockStyle.Fill;
            FilePathText.Location = new Point(6, 6);
            FilePathText.Margin = new Padding(6, 6, 59, 6);
            FilePathText.Name = "FilePathText";
            FilePathText.Size = new Size(500, 39);
            FilePathText.TabIndex = 12;
            // 
            // Window
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.PeachPuff;
            ClientSize = new Size(1642, 1024);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(6);
            Name = "Window";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "NumberLink";
            ((System.ComponentModel.ISupportInitialize)MainFrame).EndInit();
            GeneticAlgorithmGroup.ResumeLayout(false);
            GeneticAlgorithmGroup.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResultsBox.ResumeLayout(false);
            ResultsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox MainFrame;
        private GroupBox GeneticAlgorithmGroup;
        private Label GenLbl;
        private Label PopLbl;
        private MaskedTextBox FitIn;
        private Label FitLbl;
        private MaskedTextBox MutIn;
        private Label MutLbl;
        private MaskedTextBox GenIn;
        private MaskedTextBox PopIn;
        private GroupBox ResultsBox;
        private Button StartButton;
        private ErrorProvider ErrorProvider;
        private OpenFileDialog openFileDialog;
        private Button FileDialogButton;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox FilePathText;
        private TableLayoutPanel tableLayoutPanel3;
        private CheckBox checkBox1;
    }
}
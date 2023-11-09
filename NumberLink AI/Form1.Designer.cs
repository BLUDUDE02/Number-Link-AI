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
            preview1 = new PictureBox();
            preview2 = new PictureBox();
            preview3 = new PictureBox();
            preview4 = new PictureBox();
            MainFrame = new PictureBox();
            GeneticAlgorithmGroup = new GroupBox();
            FitIn = new MaskedTextBox();
            FitLbl = new Label();
            MutIn = new MaskedTextBox();
            MutLbl = new Label();
            GenIn = new MaskedTextBox();
            PopIn = new MaskedTextBox();
            GenLbl = new Label();
            PopLbl = new Label();
            ResultsBox = new GroupBox();
            ConnectionsLbl = new Label();
            AverageConnectionsHeader = new Label();
            BestScore = new Label();
            BestName = new Label();
            label4 = new Label();
            StartButton = new Button();
            ErrorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)preview1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MainFrame).BeginInit();
            GeneticAlgorithmGroup.SuspendLayout();
            ResultsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // preview1
            // 
            preview1.BackColor = Color.Black;
            preview1.Location = new Point(436, 12);
            preview1.Name = "preview1";
            preview1.Size = new Size(100, 100);
            preview1.TabIndex = 0;
            preview1.TabStop = false;
            preview1.Click += preview1_Click;
            // 
            // preview2
            // 
            preview2.BackColor = Color.Black;
            preview2.Location = new Point(436, 118);
            preview2.Name = "preview2";
            preview2.Size = new Size(100, 100);
            preview2.TabIndex = 1;
            preview2.TabStop = false;
            preview2.Click += preview2_Click;
            // 
            // preview3
            // 
            preview3.BackColor = Color.Black;
            preview3.Location = new Point(436, 224);
            preview3.Name = "preview3";
            preview3.Size = new Size(100, 100);
            preview3.TabIndex = 2;
            preview3.TabStop = false;
            preview3.Click += preview3_Click;
            // 
            // preview4
            // 
            preview4.BackColor = Color.Black;
            preview4.Location = new Point(436, 330);
            preview4.Name = "preview4";
            preview4.Size = new Size(100, 100);
            preview4.TabIndex = 3;
            preview4.TabStop = false;
            preview4.Click += preview4_Click;
            // 
            // MainFrame
            // 
            MainFrame.BackColor = Color.Black;
            MainFrame.Location = new Point(12, 12);
            MainFrame.Name = "MainFrame";
            MainFrame.Size = new Size(418, 418);
            MainFrame.TabIndex = 4;
            MainFrame.TabStop = false;
            // 
            // GeneticAlgorithmGroup
            // 
            GeneticAlgorithmGroup.BackColor = Color.Linen;
            GeneticAlgorithmGroup.Controls.Add(FitIn);
            GeneticAlgorithmGroup.Controls.Add(FitLbl);
            GeneticAlgorithmGroup.Controls.Add(MutIn);
            GeneticAlgorithmGroup.Controls.Add(MutLbl);
            GeneticAlgorithmGroup.Controls.Add(GenIn);
            GeneticAlgorithmGroup.Controls.Add(PopIn);
            GeneticAlgorithmGroup.Controls.Add(GenLbl);
            GeneticAlgorithmGroup.Controls.Add(PopLbl);
            GeneticAlgorithmGroup.ForeColor = Color.SaddleBrown;
            GeneticAlgorithmGroup.Location = new Point(542, 12);
            GeneticAlgorithmGroup.Name = "GeneticAlgorithmGroup";
            GeneticAlgorithmGroup.Size = new Size(150, 146);
            GeneticAlgorithmGroup.TabIndex = 5;
            GeneticAlgorithmGroup.TabStop = false;
            GeneticAlgorithmGroup.Text = "Parameters";
            // 
            // FitIn
            // 
            FitIn.BackColor = Color.Snow;
            FitIn.ForeColor = Color.SaddleBrown;
            FitIn.Location = new Point(84, 114);
            FitIn.Mask = "000";
            FitIn.Name = "FitIn";
            FitIn.RightToLeft = RightToLeft.Yes;
            FitIn.Size = new Size(40, 23);
            FitIn.TabIndex = 7;
            FitIn.Text = "10";
            // 
            // FitLbl
            // 
            FitLbl.AutoSize = true;
            FitLbl.Location = new Point(6, 119);
            FitLbl.Name = "FitLbl";
            FitLbl.Size = new Size(43, 15);
            FitLbl.TabIndex = 6;
            FitLbl.Text = "Fitness";
            // 
            // MutIn
            // 
            MutIn.BackColor = Color.Snow;
            MutIn.ForeColor = Color.SaddleBrown;
            MutIn.Location = new Point(84, 85);
            MutIn.Mask = "000";
            MutIn.Name = "MutIn";
            MutIn.RightToLeft = RightToLeft.Yes;
            MutIn.Size = new Size(40, 23);
            MutIn.TabIndex = 5;
            MutIn.Text = "10";
            // 
            // MutLbl
            // 
            MutLbl.AutoSize = true;
            MutLbl.Location = new Point(6, 90);
            MutLbl.Name = "MutLbl";
            MutLbl.Size = new Size(69, 15);
            MutLbl.TabIndex = 4;
            MutLbl.Text = "Mutation %";
            // 
            // GenIn
            // 
            GenIn.BackColor = Color.Snow;
            GenIn.ForeColor = Color.SaddleBrown;
            GenIn.Location = new Point(84, 56);
            GenIn.Mask = "000";
            GenIn.Name = "GenIn";
            GenIn.RightToLeft = RightToLeft.Yes;
            GenIn.Size = new Size(40, 23);
            GenIn.TabIndex = 3;
            GenIn.Text = "10";
            // 
            // PopIn
            // 
            PopIn.BackColor = Color.Snow;
            PopIn.ForeColor = Color.SaddleBrown;
            PopIn.Location = new Point(84, 27);
            PopIn.Mask = "000";
            PopIn.Name = "PopIn";
            PopIn.RightToLeft = RightToLeft.Yes;
            PopIn.Size = new Size(40, 23);
            PopIn.TabIndex = 2;
            PopIn.Text = "10";
            // 
            // GenLbl
            // 
            GenLbl.AutoSize = true;
            GenLbl.Location = new Point(6, 60);
            GenLbl.Name = "GenLbl";
            GenLbl.Size = new Size(70, 15);
            GenLbl.TabIndex = 1;
            GenLbl.Text = "Generations";
            // 
            // PopLbl
            // 
            PopLbl.AutoSize = true;
            PopLbl.Location = new Point(6, 30);
            PopLbl.Name = "PopLbl";
            PopLbl.Size = new Size(65, 15);
            PopLbl.TabIndex = 0;
            PopLbl.Text = "Population";
            // 
            // ResultsBox
            // 
            ResultsBox.BackColor = Color.Linen;
            ResultsBox.Controls.Add(ConnectionsLbl);
            ResultsBox.Controls.Add(AverageConnectionsHeader);
            ResultsBox.Controls.Add(BestScore);
            ResultsBox.Controls.Add(BestName);
            ResultsBox.Controls.Add(label4);
            ResultsBox.ForeColor = Color.SaddleBrown;
            ResultsBox.Location = new Point(542, 164);
            ResultsBox.Name = "ResultsBox";
            ResultsBox.Size = new Size(150, 148);
            ResultsBox.TabIndex = 8;
            ResultsBox.TabStop = false;
            ResultsBox.Text = "Results Overview";
            // 
            // ConnectionsLbl
            // 
            ConnectionsLbl.AutoSize = true;
            ConnectionsLbl.Location = new Point(6, 115);
            ConnectionsLbl.Name = "ConnectionsLbl";
            ConnectionsLbl.Size = new Size(74, 15);
            ConnectionsLbl.TabIndex = 12;
            ConnectionsLbl.Text = "Connections";
            // 
            // AverageConnectionsHeader
            // 
            AverageConnectionsHeader.AutoSize = true;
            AverageConnectionsHeader.Location = new Point(6, 100);
            AverageConnectionsHeader.Name = "AverageConnectionsHeader";
            AverageConnectionsHeader.Size = new Size(120, 15);
            AverageConnectionsHeader.TabIndex = 11;
            AverageConnectionsHeader.Text = "Average Connections";
            // 
            // BestScore
            // 
            BestScore.AutoSize = true;
            BestScore.Location = new Point(6, 60);
            BestScore.Name = "BestScore";
            BestScore.Size = new Size(77, 15);
            BestScore.TabIndex = 10;
            BestScore.Text = "Connections:";
            // 
            // BestName
            // 
            BestName.AutoSize = true;
            BestName.Location = new Point(6, 45);
            BestName.Name = "BestName";
            BestName.Size = new Size(45, 15);
            BestName.TabIndex = 9;
            BestName.Text = "Name: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 30);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 8;
            label4.Text = "Most Successful";
            // 
            // StartButton
            // 
            StartButton.BackColor = Color.Snow;
            StartButton.ForeColor = Color.SaddleBrown;
            StartButton.Location = new Point(542, 318);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(150, 111);
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
            // Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PeachPuff;
            ClientSize = new Size(704, 441);
            Controls.Add(StartButton);
            Controls.Add(ResultsBox);
            Controls.Add(GeneticAlgorithmGroup);
            Controls.Add(MainFrame);
            Controls.Add(preview4);
            Controls.Add(preview3);
            Controls.Add(preview2);
            Controls.Add(preview1);
            Name = "Window";
            Text = "NumberLink";
            ((System.ComponentModel.ISupportInitialize)preview1).EndInit();
            ((System.ComponentModel.ISupportInitialize)preview2).EndInit();
            ((System.ComponentModel.ISupportInitialize)preview3).EndInit();
            ((System.ComponentModel.ISupportInitialize)preview4).EndInit();
            ((System.ComponentModel.ISupportInitialize)MainFrame).EndInit();
            GeneticAlgorithmGroup.ResumeLayout(false);
            GeneticAlgorithmGroup.PerformLayout();
            ResultsBox.ResumeLayout(false);
            ResultsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox preview1;
        private PictureBox preview2;
        private PictureBox preview3;
        private PictureBox preview4;
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
        private Label label4;
        private Label AverageConnectionsHeader;
        private Label BestScore;
        private Label BestName;
        private Label ConnectionsLbl;
        private Button StartButton;
        private ErrorProvider ErrorProvider;
    }
}
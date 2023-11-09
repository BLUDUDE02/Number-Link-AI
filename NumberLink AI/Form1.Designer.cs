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
            preview1 = new PictureBox();
            preview2 = new PictureBox();
            preview3 = new PictureBox();
            preview4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)preview1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)preview4).BeginInit();
            SuspendLayout();
            // 
            // preview1
            // 
            preview1.BackColor = Color.Black;
            preview1.Location = new Point(688, 12);
            preview1.Name = "preview1";
            preview1.Size = new Size(100, 100);
            preview1.TabIndex = 0;
            preview1.TabStop = false;
            preview1.Click += preview1_Click;
            // 
            // preview2
            // 
            preview2.BackColor = Color.Black;
            preview2.Location = new Point(688, 118);
            preview2.Name = "preview2";
            preview2.Size = new Size(100, 100);
            preview2.TabIndex = 1;
            preview2.TabStop = false;
            preview2.Click += preview2_Click;
            // 
            // preview3
            // 
            preview3.BackColor = Color.Black;
            preview3.Location = new Point(688, 224);
            preview3.Name = "preview3";
            preview3.Size = new Size(100, 100);
            preview3.TabIndex = 2;
            preview3.TabStop = false;
            preview3.Click += preview3_Click;
            // 
            // preview4
            // 
            preview4.BackColor = Color.Black;
            preview4.Location = new Point(688, 330);
            preview4.Name = "preview4";
            preview4.Size = new Size(100, 100);
            preview4.TabIndex = 3;
            preview4.TabStop = false;
            preview4.Click += preview4_Click;
            // 
            // Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            ResumeLayout(false);
        }

        #endregion

        private PictureBox preview1;
        private PictureBox preview2;
        private PictureBox preview3;
        private PictureBox preview4;
    }
}
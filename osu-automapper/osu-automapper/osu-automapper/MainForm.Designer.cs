namespace osu_automapper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.waveViewer1 = new NAudio.Gui.WaveViewer();
            this.createRandomButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.blockerLabel1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.creatButton = new System.Windows.Forms.PictureBox();
            this.playButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.creatButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // waveViewer1
            // 
            this.waveViewer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(143)))));
            this.waveViewer1.ForeColor = System.Drawing.Color.Black;
            this.waveViewer1.Location = new System.Drawing.Point(436, 230);
            this.waveViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.waveViewer1.Name = "waveViewer1";
            this.waveViewer1.SamplesPerPixel = 128;
            this.waveViewer1.Size = new System.Drawing.Size(616, 224);
            this.waveViewer1.StartPosition = ((long)(0));
            this.waveViewer1.TabIndex = 0;
            this.waveViewer1.WaveStream = null;
            this.waveViewer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.waveViewer1_MouseDown);
            this.waveViewer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.waveViewer1_MouseMove);
            this.waveViewer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.waveViewer1_MouseUp);
            // 
            // createRandomButton
            // 
            this.createRandomButton.BackColor = System.Drawing.Color.White;
            this.createRandomButton.Location = new System.Drawing.Point(725, 453);
            this.createRandomButton.Margin = new System.Windows.Forms.Padding(5);
            this.createRandomButton.Name = "createRandomButton";
            this.createRandomButton.Size = new System.Drawing.Size(313, 34);
            this.createRandomButton.TabIndex = 4;
            this.createRandomButton.Text = "Create Random Beatmap";
            this.createRandomButton.UseVisualStyleBackColor = false;
            this.createRandomButton.Click += new System.EventHandler(this.createRandomButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 132);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(922, 91);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(505, 118);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pathLabel
            // 
            this.pathLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.pathLabel.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathLabel.ForeColor = System.Drawing.Color.White;
            this.pathLabel.Location = new System.Drawing.Point(53, 160);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(795, 34);
            this.pathLabel.TabIndex = 7;
            this.pathLabel.Text = "C://";
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // blockerLabel1
            // 
            this.blockerLabel1.Location = new System.Drawing.Point(9, 208);
            this.blockerLabel1.Name = "blockerLabel1";
            this.blockerLabel1.Size = new System.Drawing.Size(925, 25);
            this.blockerLabel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(922, 25);
            this.label1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 86);
            this.label2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(906, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 65);
            this.label3.TabIndex = 11;
            // 
            // creatButton
            // 
            this.creatButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.creatButton.Image = ((System.Drawing.Image)(resources.GetObject("creatButton.Image")));
            this.creatButton.Location = new System.Drawing.Point(25, 246);
            this.creatButton.Name = "creatButton";
            this.creatButton.Size = new System.Drawing.Size(194, 178);
            this.creatButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.creatButton.TabIndex = 12;
            this.creatButton.TabStop = false;
            this.creatButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // playButton
            // 
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.Location = new System.Drawing.Point(235, 246);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(194, 178);
            this.playButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playButton.TabIndex = 13;
            this.playButton.TabStop = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(143)))));
            this.ClientSize = new System.Drawing.Size(1052, 501);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.creatButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blockerLabel1);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.createRandomButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.waveViewer1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "osu! automapper";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.creatButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private NAudio.Gui.WaveViewer waveViewer1;
        private System.Windows.Forms.Button createRandomButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label blockerLabel1;
        private System.Windows.Forms.PictureBox creatButton;
        private System.Windows.Forms.PictureBox playButton;

    }
}


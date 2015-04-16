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
            this.pauseButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.waveViewer1 = new NAudio.Gui.WaveViewer();
            this.createRandomButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.blockerLabel1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pauseButton
            // 
            this.pauseButton.BackColor = System.Drawing.Color.White;
            this.pauseButton.Location = new System.Drawing.Point(691, 423);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(5);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(313, 38);
            this.pauseButton.TabIndex = 1;
            this.pauseButton.Text = "Pause/Play MP3";
            this.pauseButton.UseVisualStyleBackColor = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
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
            this.createRandomButton.Location = new System.Drawing.Point(691, 331);
            this.createRandomButton.Margin = new System.Windows.Forms.Padding(5);
            this.createRandomButton.Name = "createRandomButton";
            this.createRandomButton.Size = new System.Drawing.Size(313, 34);
            this.createRandomButton.TabIndex = 4;
            this.createRandomButton.Text = "Create Random Beatmap";
            this.createRandomButton.UseVisualStyleBackColor = false;
            this.createRandomButton.Click += new System.EventHandler(this.createRandomButton_Click);
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.White;
            this.createButton.Location = new System.Drawing.Point(691, 376);
            this.createButton.Margin = new System.Windows.Forms.Padding(5);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(313, 37);
            this.createButton.TabIndex = 3;
            this.createButton.Text = "Create Audio Beatmap";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
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
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
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
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(25, 246);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(194, 178);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(235, 246);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(194, 178);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(143)))));
            this.ClientSize = new System.Drawing.Size(1052, 501);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blockerLabel1);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.createRandomButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.pauseButton);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pauseButton;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private NAudio.Gui.WaveViewer waveViewer1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button createRandomButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label blockerLabel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;

    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NAudio.Wave;

namespace osu_automapper
{
	public partial class MainForm : Form
	{
		private bool draggingWaveForm = false;
		private Point prevMousePosition;

		private Beatmap beatmap;
		private AudioAnalyzer analyzer;
		private string mp3FilePath = "";
		private BlockAlignReductionStream stream = null;
		private DirectSoundOut output = null;

        //initialization
		public MainForm()
		{
			InitializeComponent();
		}

        //destroy the audio data
		private void DisposeWave()
		{
			if (output != null)
			{
				if (output.PlaybackState == PlaybackState.Playing) output.Stop();
				output.Dispose();
				output = null;
			}

			if (stream != null)
			{
				stream.Dispose();
				stream = null;
			}
		}

        //creates a random beatmap (no audio detection used)
		private void createRandomButton_Click(object sender, EventArgs e)
		{
			if (beatmap == null)
			{
				return;
			}

			beatmap.CreateRandomBeatmap();
		}

        //mouse drag helper
		private void waveViewer_MouseDown(object sender, MouseEventArgs e)
		{
			prevMousePosition = MousePosition;
			draggingWaveForm = true;
		}

        //mouse drag helper
		private void waveViewer_MouseMove(object sender, MouseEventArgs e)
		{
			if (!draggingWaveForm)
			{
				return;
			}

			Point currentMousePosition = MousePosition;
			Point wvp = waveViewer1.Location;
			var delta = currentMousePosition.X - prevMousePosition.X;
			wvp = new Point(MathHelper.Clamp(wvp.X + delta, int.MinValue, 0), wvp.Y);
			waveViewer1.Location = wvp;
			prevMousePosition = currentMousePosition;

			waveViewer1.Width = this.Location.X + this.Width - wvp.X;
		}

        //mouse drag helper
		private void waveViewer_MouseUp(object sender, MouseEventArgs e)
		{
			draggingWaveForm = false;
		}

        //shows open file dialog box
        private void openFileButton_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("IMAGE CICKED");

            var open = new OpenFileDialog();
            open.Filter = "OSU file (*.osu)|*.osu;";

            if (open.ShowDialog() != DialogResult.OK) { return; }
            Console.WriteLine("Opening .osb/.mp3 files...");

            // If path is good, lets get the file contents.     
            if (!open.CheckPathExists)
            {
                Console.WriteLine("Error: Path does not exist.");
                return;
            }

            string filename = open.FileName;
            this.beatmap = new Beatmap(filename);
            string path = Path.GetDirectoryName(filename);
            string mp3Name = this.beatmap.mp3Name;
            this.mp3FilePath = Path.Combine(path, mp3Name);
            this.pathLabel.Text = path;
            if (mp3Name == "" || path == "")
            {
                Console.WriteLine("Error: Could not find mp3 file.");
            }
            else
            {
                Console.WriteLine("complete.");

                WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(this.mp3FilePath));
                waveViewer1.WaveStream = pcm;
            }
        }

        //creates the beatmap using audio detection
        private void createButton_Click(object sender, EventArgs e)
        {
            if (this.mp3FilePath == "" || beatmap == null)
            {
                if (this.mp3FilePath == "")
                {
                    Console.WriteLine("Error:No MP3 selected.");
                }

                if (beatmap == null)
                {
                    Console.WriteLine("Error:No beatmap selected.");
                }

                return;
            }

            this.analyzer = new AudioAnalyzer(this.mp3FilePath, this.beatmap);
            this.beatmap.CreateRandomBeatmap(this.analyzer);
        }

        //play or pause the selected mp3
        private void playButton_Click(object sender, EventArgs e)
        {
            if (this.mp3FilePath == null || this.mp3FilePath == "")
                return;

            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Pause();
                    Console.WriteLine("Paused");
                }
                else if (output.PlaybackState == PlaybackState.Paused)
                {
                    output.Play();
                    Console.WriteLine("Played");
                }
            }

            if (output == null)
            {
                WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(this.mp3FilePath));
                if (pcm == null)
                    return;

                waveViewer1.WaveStream = pcm;
                stream = new BlockAlignReductionStream(pcm);
                output = new DirectSoundOut();
                output.Init(stream);
                output.Play();
            }
        }
	}
}

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

namespace osu_automapper
{
    public partial class MainForm : Form
    {
        bool draggingWaveForm = false;
        Point prevMousePosition;

        Beatmap beatmap;
        AudioAnalyzer analyzer;
        NAudio.Wave.WaveStream pcm;

        public MainForm()
        {
            InitializeComponent();
            Color toColor = Color.FromArgb(255, 200, 200, 200);
            //LoadColors(loadMP3Button, Color.White, toColor);
            //LoadColors(pauseButton, Color.White, toColor);
            //LoadColors(openOSUFile, Color.White, toColor);
        }

        public void LoadColors(Button button, Color fromColor, Color toColor)
        {
            Bitmap bmp = new Bitmap(button.Width, button.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Rectangle r = new Rectangle(0, 0, bmp.Width, bmp.Height);
                using (LinearGradientBrush br = new LinearGradientBrush(
                                                    r,
                                                    fromColor,
                                                    toColor,
                                                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(br, r);
                }
            }

            button.ForeColor = Color.Black;
            button.BackColor = Color.FromArgb(0, 0, 0, 0);
            button.BackgroundImage = bmp;
        }

        private NAudio.Wave.BlockAlignReductionStream stream = null;

        private NAudio.Wave.DirectSoundOut output = null;

        private void openMP3Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 file (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK)
                return;

            DisposeWave();

            Console.WriteLine("FileName is: " + open.FileName);
            pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(open.FileName));

            stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            output = new NAudio.Wave.DirectSoundOut();
            output.Init(stream);
            output.Play();

            pauseButton.Enabled = true;


        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
                else if (output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }

        private void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }

            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        private void openOSUFile_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Search for .osu File");

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "OSU file (*.osu)|*.osu;";
            if (open.ShowDialog() != DialogResult.OK) return;

            // If path is good, lets get the file contents.     
            if (!open.CheckPathExists)
            {
                Console.WriteLine("Error: .CheckPathExists == false");
                return;
            }

            this.beatmap = new Beatmap(open.FileName);
            this.beatmap.CreateRandomBeatmap();
        }



        private void createButton_Click(object sender, EventArgs e)
        {
            if (pcm == null || beatmap == null)
            {
                if (pcm == null)
                {
                    Console.WriteLine("Error:No MP3 selected.");
                }
                if (beatmap == null)
                {
                    Console.WriteLine("Error:No beatmap selected.");
                }
                return;
            }

            analyzer = new AudioAnalyzer(pcm, beatmap);
            beatmap.CreateRandomBeatmap(analyzer);
        }

        private void waveViewer1_MouseDown(object sender, MouseEventArgs e)
        {
            prevMousePosition = MousePosition;
            draggingWaveForm = true;
        }
        private void waveViewer1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingWaveForm)
                return;

            Point currentMousePosition = MousePosition;
            Point wvp = waveViewer1.Location;
            var delta = currentMousePosition.X - prevMousePosition.X;
            wvp = new Point(Clamp(wvp.X + delta, int.MinValue, 0), wvp.Y);
            waveViewer1.Location = wvp;
            prevMousePosition = currentMousePosition;

            waveViewer1.Width = this.Location.X + this.Width - wvp.X;
        }
        private void waveViewer1_MouseUp(object sender, MouseEventArgs e)
        {
            draggingWaveForm = false;
        }
        int Clamp(int value, int min, int max)
        {
            return value < min ? min : (value > max ? max : value);
        }
    }
}

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

            AudioAnalyzer analyzer = new AudioAnalyzer(open.FileName, waveViewer1);

            Console.WriteLine("FileName is: " + open.FileName);
            NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(open.FileName));
            
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

            Console.WriteLine("Opened .osu File");
            // If path is good, lets get the file contents.
            Beatmap instance;
            if (open.CheckPathExists) {
                string fileRawText = System.IO.File.ReadAllText(open.FileName);
                Console.WriteLine(open.FileName);

                string[] fileSplitText = fileRawText.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                Console.WriteLine(fileSplitText[4]);
                instance = new Beatmap(fileSplitText, open.FileName);
                instance.AnalyzeBeatmap();

                StreamWriter osu_file = new StreamWriter(open.FileName, true);
                int numCircles = 0;
                // Basic Beatmap Creation
                Random rnd = new Random(); // For random x and y
                for (float i = (float)instance.offset; i < instance.songLength; i += instance.mpb)
                {
                    int x = (int)rnd.Next(10, 500);
                    int y = (int)rnd.Next(10, 370);
                    string hitCircleString = instance.ReturnHitCircle(x, y, (int)i, 1, 0);
                    osu_file.WriteLine(hitCircleString);
                    numCircles++;
                }
                Console.WriteLine("Number of circles " + numCircles);
                osu_file.Close();
            }
            //Start Writing the Map
           /* StreamWriter osu_file = new StreamWriter(open.FileName, true);
            HitCircle hit1 = new HitCircle(1,2,3,4,5);
            osu_file.WriteLine(hit1.ToString());
            osu_file.Close();*/
        }

        private void openWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wave File (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK) 
                return;

            //chart1.Series.Add("wave");
            //chart1.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //chart1.Series["wave"].ChartArea = "ChartArea1";
            //NAudio.Wave.WaveChannel32 wave = new NAudio.Wave.WaveChannel32(new NAudio.Wave.Mp3FileReader(open.FileName));
            //BeginRead(wave);
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

        //This is NOT for nAudio, but for the built in Windows.Forms.DataVisulization.xxx
        //But it may be usful?
        /*private void BeginRead(NAudio.Wave.WaveChannel32 wave)
        {
            byte[] buffer = new byte[16384];
            int read = 0;

            while (wave.Position < wave.Length)
            {
                Console.WriteLine(wave.Position);
                read = wave.Read(buffer, 0, 16384);

                for (int i = 0; i < read / 4; i++)
                {
                    chart1.Series["wave"].Points.Add(BitConverter.ToSingle(buffer, i * 4));
                }
            }
        }*/

    }
}

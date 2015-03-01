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

namespace osu_automapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NAudio.Wave.BlockAlignReductionStream stream = null;

        private NAudio.Wave.DirectSoundOut output = null;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 file (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK) return;

            DisposeWave();

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

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
    }
}

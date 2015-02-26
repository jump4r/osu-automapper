using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            if (open.CheckPathExists) {
                string fileRawText = System.IO.File.ReadAllText(open.FileName);
                Console.WriteLine(open.FileName);

                string[] fileSplitText = fileRawText.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                Console.WriteLine(fileSplitText[4]);
                Beatmap instance = new Beatmap(fileSplitText);
                instance.AnalyzeBeatmap();
            }
        }
    }
}

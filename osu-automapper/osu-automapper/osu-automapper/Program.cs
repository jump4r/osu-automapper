using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osu_automapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Test");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        
       }
    }

    class Beatmap
    {
        // Beat map text
        // TODO: Replace string array with file?
        private string[] text;
        private string fileName;

        // Beatmap metadata.
        public int bpm { get; set; }
        public float mpb { get; set; } // Milliseconds per beat
        public float songLength { get; set; } // Length of song (in milliseconds).
        public int offset { get; set; } // Offset of the beatmap.
        
        public Beatmap(string[] text, string fileName)
        {
            // TODO: Complete member initialization
            this.text = text;
            this.fileName = fileName;
        }

        public void AnalyzeBeatmap()
        {
            for (int i = 0; i < text.Length; i++)
            {
                // Get the init timing points and the milleseconds per beat.
                if (text[i] == "[TimingPoints]")
                {
                    string[] initTimingPonits = text[i + 1].Split(',');
                    mpb = float.Parse(initTimingPonits[1]);
                    offset = int.Parse(initTimingPonits[0]);
                }

                else if (text[i] == "[General]")
                {
                    string mp3FilePath = GetMP3FilePath(fileName, i+1);
                    NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(mp3FilePath));
                    TimeSpan ts = pcm.TotalTime;
                    songLength = ((ts.Minutes * 60) + ts.Seconds) * 1000 + ts.Milliseconds;
                    Console.WriteLine("Total Song Length is: " + songLength);
                }
            }
        }

        // Gets the filepath for the mp3 file.
        private string GetMP3FilePath(string filePath, int lineIndex)
        {
            string[] path = filePath.Split('\\');

            // For Debug/Testing purposes. Repleace with general case
            string[] temp = text[lineIndex].Split(':');
            string mp3FileName = temp[1].Trim();

            path[path.Length - 1] = mp3FileName;
            Console.WriteLine(path[path.Length - 1]);
            string rtn = "";
            for (int i = 0; i < path.Length; i++)
            {
                rtn += path[i];
                if (i != path.Length - 1)
                  rtn += "\\";
            }

            Console.WriteLine("MP3 Path is: " + rtn);
            return rtn;
        }

        // Returns the string of a Hit Circle to be added to the beatmap.
        public string ReturnHitCircle(int x, int y, int time, int type, int hitSound)
        {
            HitCircle hc = new HitCircle(x, y, time, type, hitSound);
            return hc.ToString();
        }
    }

    class HitCircle
    {
        //constructor
        public HitCircle() { }
        public HitCircle(int a, int b, int c, int d, int e)
        {
            x = a;
            y = b;
            time = c;
            type = d;
            hitsound = e;
            
        }

        //properties
        public int x { get; set; }
        public int y { get; set; }
        public int time { get; set; }
        public int type { get; set; }
        public int hitsound { get; set; }

        //funciton 
        public override string ToString()
        {
            return x + "," + y + "," + time + "," + type + "," + hitsound + ",0:0:0:0:";
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
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

        public Beatmap(string fileName)
        {
            // TODO: Complete member initialization
            
            this.fileName = fileName;

            Console.WriteLine("Loading .osu file..." + fileName);
            string fileRawText = System.IO.File.ReadAllText(fileName);
            string[] fileSplitText = fileRawText.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);

            Console.WriteLine(fileSplitText[4]);

            this.text = fileSplitText;

            LoadBeatmapFromFile();
        }

        //TODO:
        public void GenerateBeatmapFile()
        {
            throw new NotImplementedException("TODO: Auto Generate a beatmap file from scratch. (headers, settings, etc.)");
        }

        public void LoadBeatmapFromFile()
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
                    string mp3FilePath = GetMP3FilePath(fileName, i + 1);
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

        // Creates a random beatmap. This APPENDS to the current file. 
        // TODO: Need to make sure we are not overwriting other data, and starting from the correct location.
        public void CreateRandomBeatmap()
        {
            if (!File.Exists(this.fileName))
            {
                Console.WriteLine("Error: File not found.(" + this.fileName + ")");
                return;
            }

            Console.WriteLine("Generating Random Beatmap. Appending to file..." + this.fileName);

            StreamWriter osu_file = new StreamWriter(this.fileName, true);
            int numCircles = 0;

            // Basic Beatmap Creation
            Random rnd = new Random(); // For random x and y
            for (float timestamp = (float)offset; timestamp < songLength; timestamp += mpb)
            {
                int x = (int)rnd.Next(10, 500);
                int y = (int)rnd.Next(10, 370);
                string hitCircleString = ReturnHitCircle(x, y, (int)timestamp, 1, 0);
                osu_file.WriteLine(hitCircleString);
                numCircles++;
            }

            Console.WriteLine("Number of circles " + numCircles);
            osu_file.Close();

            //Start Writing the Map
            /* StreamWriter osu_file = new StreamWriter(open.FileName, true);
             HitCircle hit1 = new HitCircle(1,2,3,4,5);
             osu_file.WriteLine(hit1.ToString());
             osu_file.Close();*/
        }

        // Returns the string of a Hit Circle to be added to the beatmap.
        public string ReturnHitCircle(int x, int y, int time, int type, int hitSound)
        {
            HitCircle hc = new HitCircle(x, y, time, type, hitSound);
            return hc.ToString();
        }
    }
}
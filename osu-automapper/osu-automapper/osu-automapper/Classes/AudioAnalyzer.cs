using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    struct PeakData
    {
        public int index;//The index that the beat 
        public int time;//Estimated time the peak occurs in ms
        public double value;//Amplitude in decibels

        //public int leftRange;
        //public int rightRange;

        public PeakData(int index, int time, double value)
        {
            this.index = index;
            this.time = time;
            this.value = value;
        }
    }

    class AudioAnalyzer
    {
        NAudio.Wave.WaveStream pcm;
        Beatmap beatmap;
        List<PeakData> peakData;
        //TODO: Add various beatmap settings to constructor
        public AudioAnalyzer(NAudio.Wave.WaveStream pcm, Beatmap beatmap)
        {
            //stream.
            //TODO: (1) Retrieve data from the wave stream. (WaveViewer is seemingly useless)
            //      (2) How do we know at what ms the data will play?
            //
            //      (3) Beat Detection. Create a list of beats from the audio data.
            //          -By comparing a peak to its surrounding peaks,
            //           we can determine if this is a good beat placement.
            //          -This can be done in combination with the bpm.
            //      (4) Slider Detection. Create a list of sliders from the audio data.
            //          -By calculating the slope between beats, we can determine if 
            //           its a good location for a slider.
            this.pcm = pcm;
            this.beatmap = beatmap;
            peakData = new List<PeakData>();       
        }

        public void Begin()
        {        
            int bpm = beatmap.bpm;
            int offset = beatmap.offset;//in milliseconds
            double offsetInSeconds = (double)offset / 1000.0;

            int bytesPerSecond = this.pcm.WaveFormat.Channels * 
                                 this.pcm.WaveFormat.SampleRate * 
                                 this.pcm.WaveFormat.BitsPerSample / 8; 
            //Convert BPM to bytesPerInterval
            //bps = bpm / 60            
            float bps = ((float)bpm) / 60f;
            int interval = (int)((float)bytesPerSecond * bps);

            Console.WriteLine("Debug:bmp/bps:" + bpm + "/" + bps);
            Console.WriteLine("Debug:byte interval:" + interval);                     
            
            int index = (int)(offsetInSeconds * bytesPerSecond);
            int ret = 0, current = index;
            if(this.pcm.Length <= index)
            {
                Console.WriteLine("Error:Offset to large.");
                return;
            }

            //Move to the offset before we begin.
            this.pcm.Position = index;

            byte[] buffer = new byte[interval];
            do
            {
                //TODO is 0 correct here? .Read is strange
                ret = this.pcm.Read(buffer, 0, interval);//.Read automatically increments stream buffer index
                
                CalculatePeak(current, current, buffer);

                current += interval;
            } while (ret != -1);     
        }

        //Creates PeakData from the array of pcm data
        private void CalculatePeak(int index, int time, byte[] buffer)
        {
            //For calculating peaks.            
            double sum = 0;
            for (var i = 0; i < buffer.Length; i = i + 2)
            {
                double sample = BitConverter.ToInt16(buffer, i) / 32768.0;
                sum += (sample * sample);
            }
            double rms = Math.Sqrt(sum / (buffer.Length / 2));
            var decibel = 20 * Math.Log10(rms);

            PeakData pd = new PeakData(index, time, decibel);
            peakData.Add(pd);
        }
    }
}

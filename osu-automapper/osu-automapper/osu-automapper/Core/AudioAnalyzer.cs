using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace osu_automapper
{
	public class AudioAnalyzer
	{
		//private WaveStream pcm;
        private AudioFileReader pcm;
		private Beatmap beatmap;
		private List<PeakData> peakData;

		private int bpm;
		private int offset;//in milliseconds
		private double offsetInSeconds;

		private int bytesPerSecond;

		//Convert BPM to bytesPerInterval
		//bps = bpm / 60            
		private float bps;
		private int interval;

		private int index;
		private int current;

		//TODO: Add various beatmap settings to constructor
		public AudioAnalyzer(string mp3path, /*WaveStream pcm,*/ Beatmap beatmap)
		{
            this.pcm = new AudioFileReader(mp3path);
			this.beatmap = beatmap;
			Setup();
            //CreatePeakData();
		}

		//Setups everything that CreatePeakData() and CreatePeakDataAt() will need.
		private void Setup()
		{
			peakData = new List<PeakData>();
			bpm = (int)(60 / (beatmap.mpb / 1000));

			offsetInSeconds = (double)offset / 1000.0;

			bytesPerSecond = this.pcm.WaveFormat.Channels *
								 this.pcm.WaveFormat.SampleRate *
								 this.pcm.WaveFormat.BitsPerSample / 8;

			//Convert BPM to bytesPerInterval
			//bps = bpm / 60            
			bps = ((float)bpm) / 60f;
			interval = (int)(((float)bytesPerSecond) * bps);

			Console.WriteLine("Debug:bmp/bps:" + bpm + "/" + bps);
			Console.WriteLine("Debug:byte interval:" + interval);

			index = (int)(offsetInSeconds * bytesPerSecond);
			current = index;
		}

		//Creates PeakData from the array of pcm data
		private PeakData CalculatePeak(int index, int time, byte[] buffer)
		{
			//For calculating peaks.            
			double sum = 0;
			for (var i = 0; i < buffer.Length; i = i + 2)
			{
                if (i + 1 < buffer.Length)
                {
                    double sample = BitConverter.ToInt16(buffer, i) / 32768.0;
                    sum += (sample * sample);
                    if(sample != 0)
                    {
                        //Console.WriteLine("NOT ZERO");
                     
                    }
                }
			}

			double rms = Math.Sqrt(sum / (double)(buffer.Length / 2));


			var decibel = 20 * Math.Log10(rms);
            if(double.IsInfinity(decibel))
            {
                decibel = double.MinValue;
            }
			//Console.Write("sum:" + sum);
			//Console.Write("rms:" + rms);
			//Console.WriteLine(decibel);
            Console.WriteLine("Decibal:" + decibel);
			return new PeakData(index, time, decibel);
		}

		/// <summary>
		/// This creates all the peak data at once, sampling at a constant interval
		/// calculated from bpm. The data is stored in List<PeakData> peakData.
		/// This is NOT flexible, peak data will be limited to the bpm interval.
		/// </summary>
		public List<PeakData> CreatePeakData()
		{
			Setup();
			
			peakData = new List<PeakData>();
			//this.pcm.Position = index;
            Console.WriteLine("INDEX: " + this.pcm.Position);
            Console.WriteLine("INTERVAL: " + interval);
            Console.WriteLine("LENGTH: " + this.pcm.Length);
			int ret = 0;
			do
			{
                interval = (int) (this.pcm.Length - this.pcm.Position < interval ? this.pcm.Length - this.pcm.Position : interval);
                byte[] buffer = new byte[interval];
                Console.WriteLine("Buff Index:" + this.pcm.Position);
                
				ret = this.pcm.Read(buffer, 0, interval);//.Read automatically increments stream buffer index
                
				peakData.Add(CalculatePeak(current, current, buffer));
                
				current += interval;

			} while (this.pcm.Position + interval < this.pcm.Length);

			return this.peakData;
		}

		//TODO: "range" is a very relative value. We will have to experiment with values
		//      to determine a good range. However, this value will most likely be dependant on the
		//      mp3 file, due to the possiblity of multiple channels.
		/// <summary>
		/// This moves the stream index to the current millisecond, and reads a range
		/// of values surrounding it for calculating a peak.
		/// This IS flexible, we can read bytes from any interval we want.
		/// </summary>
		/// <param name="currentMillisecond">The current millisecond in the song.</param>
		/// <param name="range">The range around the millisecond to take an average of.</param>
		public PeakData CreatePeakDataAt(int currentMillisecond, int range)
		{
			double offsetInSeconds = (double)currentMillisecond / 1000.0;
			int index = (int)(offsetInSeconds * bytesPerSecond);

            int size = range * 2;
			byte[] buffer = new byte[size];

			this.pcm.Position = index - range;
			int ret = this.pcm.Read(buffer, 0, buffer.Length);
			Console.WriteLine("Ret:" + ret);
			PeakData pd = CalculatePeak(index, currentMillisecond, buffer);
			//peakData.Add(pd);
			return pd;
		}

		//This function will create a set of PeakData that is based on amplitude thresh holds,
		//completely ignoring bpm.
		//
		//Dynamic Thresh Hold Creation: The required amplitude threshold will be relative to the 
		//                              current average amplitude at that point in the song.
		//Slope Detection: This will be used to detect possible slider placement and length.
		//                 e.g. a slope close to zero across several samples 
		//                      would suggest a flat, drawn out sound (I think?). 
		//                      However, this will never truely occur because most songs will
		//                      have multiple instruments playing at the same time.
		//                      So a drawn out violin sound would be surrounded by bass "wiggles"/waves.

		public void PerformRandomBeatDetection()
		{
			throw new NotImplementedException();
		}
	}
}

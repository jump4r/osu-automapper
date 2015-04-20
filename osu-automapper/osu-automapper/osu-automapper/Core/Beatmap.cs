using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NAudio.Wave;

namespace osu_automapper
{
    /// <summary>
    /// Declares the initial beatmap for generation. 
    /// </summary>
	public class Beatmap
	{
		public static AABB PlayField = new AABB(new Vector2(255f, 190f), new Vector2(245f, 180f));

		// Beat map text
		// TODO: Replace string array with file?z
		private string[] fileData;

		private string filePath;

		// Beatmap metadata.   
        public string mp3Name {get; set;}
		public int bpm { get; set; }
		public float mpb { get; set; } // Milliseconds per beat
		public float songLength { get; set; } // Length of song (in milliseconds).
		public int offset { get; set; } // Offset of the beatmap.

		// This is implying a 4/4 Time Signature. Any others might be out of scope
		private float currentBeat = 0;
		private int beatsPerMeasure = 4;
		private float sliderVelocity = 1.5f;
		private int sliderLengthPerBeat;

        // Used for combo resets
        private float comboChangeFlag = NoteDuration.Whole * 2; // Change combo every 2 measures
        private float currentComboLength = 0f;

		// Difficulty
		private float maxNoteDistance = 100;

		private int minSliderCurves = 2;
		private int maxSliderCurves = 4;

		//private char[] sliderTypes = new char[] { 'L', 'B', 'P' };

        /// <summary>
        /// Beatmap contsructor
        /// </summary>
        /// <param name="filePath"> Takes a filePath, can get all metadate from .osu file.</param>
		public Beatmap(string filePath)
		{
			// TODO: Complete member initialization

			this.filePath = filePath;

			Console.WriteLine("Loading .osu file..." + filePath);
			string fileRawText = File.ReadAllText(filePath);
			string[] fileSplitText = fileRawText.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);

			Console.WriteLine(fileSplitText[4]);

			this.fileData = fileSplitText;

			sliderLengthPerBeat = (int)(sliderVelocity * 100 / (beatsPerMeasure));


			LoadBeatmapFromFile();
		}

		//TODO:
		public void GenerateBeatmapFile()
		{
			throw new NotImplementedException("TODO: Auto Generate a beatmap file from scratch. (headers, settings, etc.)");
		}

		/// <summary>
		/// Parse .osu Beatmap file.
		/// </summary>
		public void LoadBeatmapFromFile()
		{
			for (int i = 0; i < fileData.Length; i++)
			{
				// Get the init timing points and the milleseconds per beat.
                switch(fileData[i])
                {
                    case "[TimingPoints]":

                        string[] initTimingPonits = fileData[i + 1].Split(',');
					    mpb = float.Parse(initTimingPonits[1]);
					    offset = int.Parse(initTimingPonits[0]);
                        break;
                    case "[General]":

                        string mp3FilePath = GetPath(fileData[i + 1], Path.GetDirectoryName(filePath));
					    WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(mp3FilePath));
                        TimeSpan ts = pcm.TotalTime;
					    songLength = ((ts.Minutes * 60) + ts.Seconds) * 1000 + ts.Milliseconds;
					    Console.WriteLine("Total Song Length is: " + songLength);
                        break;
                    case "[Difficulty]":

                        string[] sliderVelText = fileData[i + 5].Split(':');
					    sliderVelocity = float.Parse(sliderVelText[1]);
					    Console.WriteLine("Slider Velocity is: " + sliderVelocity);
                        break;
                    default:
                        break;
                }
			}
		}

		// Gets the filepath for the mp3 file.
		private string GetPath(string file, string directory)
		{
            this.mp3Name = file.Split(':')[1].Trim();
			return Path.Combine(directory, this.mp3Name);
		}

		// Creates a random beatmap. This APPENDS to the current file. 
		// TODO: Need to make sure we are not overwriting other data, and starting from the correct location.
		public void CreateRandomBeatmap()
		{
			if (!File.Exists(this.filePath))
			{
				Console.WriteLine("Error: File not found.(" + this.filePath + ")");
				return;
			}

			Console.WriteLine("Generating Random Beatmap. Appending to file..." + this.filePath);

			using (var osuFile = new StreamWriter(this.filePath, true))
			{
				int numCircles = 0;

				// Basic Beatmap Creation
				var prevPoint = Vector2.NegativeOne;

				float timestamp = (float)offset;
				// for (float timestamp = (float)offset; timestamp < songLength; timestamp += mpb)
				while (timestamp < songLength)
				{
					float x = RandomHelper.Range(Beatmap.PlayField.Left, Beatmap.PlayField.Right + 1);
					float y = RandomHelper.Range(Beatmap.PlayField.Top, Beatmap.PlayField.Bottom + 1);
                    bool newCombo = false;

                    // Determine if new Combo is needed
                    if (currentComboLength > comboChangeFlag)
                    {
                        newCombo = true;
                        currentComboLength = currentComboLength % comboChangeFlag;   
                    }
                    Console.WriteLine("currentBeat" + currentBeat);

                    /*
                    float getBeatType = RandomHelper.NextFloat;
                    if (getBeatType < 0.05f)
                    {
                        timestamp += AddTime(NoteDuration.Quarter);
                        currentBeat += NoteDuration.Quarter;
                        continue;
                    }
                    */

					if (currentBeat % beatsPerMeasure == 0)
					{
						// Generate a random slider. // Test Random Slider Durations
						var sliderType = EnumHelper.GetRandom<SliderCurveType>();
                        HitObjectType hitType = (newCombo) ? HitObjectType.SliderNewCombo : HitObjectType.Slider;

                        float sliderTimespan = (RandomHelper.NextFloat < 0.5f) ? NoteDuration.Quarter : NoteDuration.Eighth;
						string sliderData = GetSliderData(new Vector2(x, y), (int)timestamp, hitType,
												HitObjectSoundType.None, sliderType, 1, sliderVelocity, RandomHelper.Range(minSliderCurves, maxSliderCurves + 1), sliderTimespan);

						osuFile.WriteLine(sliderData);

						numCircles++;

						timestamp += AddTime(sliderTimespan * 2);
						currentBeat += sliderTimespan * 2;
                        currentComboLength += sliderTimespan * 2;

					}
					else
					{
                        HitObjectType hitType = (newCombo) ? HitObjectType.NormalNewCombo : HitObjectType.Normal;

                        // Test patterns!
                        if (RandomHelper.NextFloat < 0.12)
                        {
                            Triple triple = new Triple(PlayField.Center, (int)timestamp, HitObjectSoundType.None, prevPoint, mpb);
                            osuFile.WriteLine(triple.SerializeForOsu());

                            currentComboLength += triple.totalLength;
                            timestamp += AddTime(triple.totalLength);
                            currentBeat += triple.totalLength;

                            prevPoint = PlayField.Center;
                        }

                        else
                        {
                            string circleData = GetHitCircleData(new Vector2(x, y), (int)timestamp, hitType,
                                                    HitObjectSoundType.None, prevPoint);

                            osuFile.WriteLine(circleData);

                            numCircles++;

                            currentComboLength += NoteDuration.Eighth;
                            timestamp += AddTime(NoteDuration.Eighth);
                            currentBeat += NoteDuration.Eighth;

                            prevPoint = new Vector2(x, y);
                        }
					}

                    // New Combo
                    
				}

				Console.WriteLine("Number of circles " + numCircles);
			}
		}

		public void CreateRandomBeatmap(AudioAnalyzer analyzer)
		{
			if (!File.Exists(this.filePath))
			{
				Console.WriteLine("Error: File not found.(" + this.filePath + ")");
				return;
			}

			Console.WriteLine("Generating Random Beatmap. Appending to file..." + this.filePath);

			using (var osuFile = new StreamWriter(this.filePath, true))
			{
				int numCircles = 0;

				// Basic Beatmap Creation
				var prevPoint = PlayField.GetRandomPointInside();

				float timestamp = (float)offset;

				// for (float timestamp = (float)offset; timestamp < songLength; timestamp += mpb)
				while (timestamp < songLength)
				{
					Vector2 pos = Vector2.Zero;
                    float x = RandomHelper.Range(Beatmap.PlayField.Left, Beatmap.PlayField.Right + 1);
                    float y = RandomHelper.Range(Beatmap.PlayField.Top, Beatmap.PlayField.Bottom + 1);
                    bool newCombo = false;

					//Gets a point on a circle whose center lies at the previous point.
					do
					{
						float radius = RandomHelper.Range(30f, maxNoteDistance);//<---- This could be a function of bpm, i.e. time-distance relation between beats

						pos = prevPoint + RandomHelper.OnCircle(prevPoint, radius);
					} while (!PlayField.Contains(pos));


					////EXAMPLE USE CASE
                    double threshold = Double.Parse("-10");
					var peakData = analyzer.CreatePeakDataAt((int)timestamp, 10000);
					Console.WriteLine(peakData.ToString());
					if (peakData.value < threshold)
					{
						//Continue without adding a beat here if no sound was detected.
						timestamp += AddTime(NoteDuration.Half);
						continue;
					}
					////END EXAMPLE
                    // Determine if new Combo is needed
                    if (currentComboLength > comboChangeFlag)
                    {
                        newCombo = true;
                        currentComboLength = currentComboLength % comboChangeFlag;
                    }

					if (currentBeat % beatsPerMeasure == 0)
					{
						// Generate a random slider.
						var sliderType = EnumHelper.GetRandom<SliderCurveType>(); //sliderTypes[rnd.Next(0, 3)];
                        float sliderTimespan = (RandomHelper.NextFloat < 0.5f) ? NoteDuration.Quarter : NoteDuration.Eighth;
						string sliderData = GetSliderData(pos, (int)timestamp, HitObjectType.Slider,
												HitObjectSoundType.None, sliderType, 1, sliderVelocity, RandomHelper.Range(minSliderCurves, maxSliderCurves + 1), sliderTimespan);

						osuFile.WriteLine(sliderData);

						numCircles++;

						timestamp += AddTime(sliderTimespan * 2);
						currentBeat += sliderTimespan * 2;
                        currentComboLength += sliderTimespan * 2;
					}
                    else
                    {
                        HitObjectType hitType = (newCombo) ? HitObjectType.NormalNewCombo : HitObjectType.Normal;

                        // Test patterns!
                        if (RandomHelper.NextFloat < 0.12)
                        {
                            Triple triple = new Triple(PlayField.Center, (int)timestamp, HitObjectSoundType.None, prevPoint, mpb);
                            osuFile.WriteLine(triple.SerializeForOsu());

                            currentComboLength += triple.totalLength;
                            timestamp += AddTime(triple.totalLength);
                            currentBeat += triple.totalLength;

                            prevPoint = PlayField.Center;
                        }

                        else
                        {
                            string circleData = GetHitCircleData(new Vector2(x, y), (int)timestamp, hitType,
                                                    HitObjectSoundType.None, prevPoint);

                            osuFile.WriteLine(circleData);

                            numCircles++;

                            currentComboLength += NoteDuration.Eighth;
                            timestamp += AddTime(NoteDuration.Eighth);
                            currentBeat += NoteDuration.Eighth;

                            prevPoint = new Vector2(x, y);
                        }
                    }
				}

				Console.WriteLine("Number of circles " + numCircles);
			}
		}

		private float AddTime(float noteDuration)
		{
			return noteDuration * mpb;
		}

		// Returns the string of a Hit Circle to be added to the beatmap.
		public string GetHitCircleData(Vector2 position, int time, HitObjectType hitType, HitObjectSoundType hitSound, Vector2 prevPoint)
		{
			var hc = new HitCircle(position, time, hitType, hitSound, prevPoint, maxNoteDistance);

			return hc.SerializeForOsu();
		}

		public string GetSliderData(Vector2 startPosition, int time, HitObjectType hitType,
			HitObjectSoundType hitsound, SliderCurveType sliderType, int repeat, float sliderVelocity, int numCurves, float timeSpan)
		{
			// Length will determine how long a slider will go on for
			int len = (int)(sliderVelocity * 100 * timeSpan);
			var hs = new HitSlider(startPosition, time, hitType, hitsound, sliderType, repeat, sliderVelocity, numCurves, len);

			return hs.SerializeForOsu();
		}
	}
}

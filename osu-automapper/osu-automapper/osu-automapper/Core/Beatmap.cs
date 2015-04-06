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
	public class Beatmap
	{
		public static AABB PlayField = new AABB(new Vector2(255f, 190f), new Vector2(245f, 180f));

		// Beat map text
		// TODO: Replace string array with file?
		private string[] fileData;
		private string filePath;

		// Beatmap metadata.
		public int bpm { get; set; }
		public float mpb { get; set; } // Milliseconds per beat
		public float songLength { get; set; } // Length of song (in milliseconds).
		public int offset { get; set; } // Offset of the beatmap.

		// This is implying a 4/4 Time Signature. Any others might be out of scope
		private int currentBeat = 0;
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

				if (fileData[i] == "[TimingPoints]")
				{
					string[] initTimingPonits = fileData[i + 1].Split(',');
					mpb = float.Parse(initTimingPonits[1]);
					offset = int.Parse(initTimingPonits[0]);
				}
				else if (fileData[i] == "[General]")
				{
					string mp3FilePath = GetAudioFilePath(fileData[i + 1], Path.GetDirectoryName(filePath));

					WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(mp3FilePath));
					TimeSpan ts = pcm.TotalTime;

					songLength = ((ts.Minutes * 60) + ts.Seconds) * 1000 + ts.Milliseconds;

					Console.WriteLine("Total Song Length is: " + songLength);
				}
				else if (fileData[i] == "[Difficulty]")
				{
					string[] sliderVelText = fileData[i + 5].Split(':');
					sliderVelocity = float.Parse(sliderVelText[1]);

					Console.WriteLine("Slider Velocity is: " + sliderVelocity);
				}
			}
		}

		// Gets the filepath for the mp3 file.
		private string GetAudioFilePath(string audioFileNameEntry, string directory)
		{
			var path = Path.Combine(directory, audioFileNameEntry.Split(':')[1].Trim());

			Console.WriteLine("MP3 Path is: " + path);

			return path;
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

					if (currentBeat % beatsPerMeasure == 0)
					{
						// Generate a random slider.
						var sliderType = EnumHelper.GetRandom<SliderCurveType>();
                        HitObjectType hitType = (newCombo) ? HitObjectType.SliderNewCombo : HitObjectType.Slider;
                        

						string sliderData = GetSliderData(new Vector2(x, y), (int)timestamp, hitType,
												HitObjectSoundType.None, sliderType, 1, sliderVelocity, RandomHelper.Range(minSliderCurves, maxSliderCurves + 1));

						osuFile.WriteLine(sliderData);

						numCircles++;

						timestamp += AddTime(NoteDuration.Half);
						currentBeat += (int)NoteDuration.Half;
                        currentComboLength += NoteDuration.Half;

					}
					else
					{
                        HitObjectType hitType = (newCombo) ? HitObjectType.NormalNewCombo : HitObjectType.Normal;

						string circleData = GetHitCircleData(new Vector2(x, y), (int)timestamp, hitType,
												HitObjectSoundType.None, prevPoint);

						osuFile.WriteLine(circleData);

						numCircles++;

                        currentComboLength += NoteDuration.Half;
						timestamp += AddTime(NoteDuration.Quarter);
						currentBeat += (int)NoteDuration.Quarter;

						prevPoint = new Vector2(x, y);
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

					//Gets a point on a circle whose center lies at the previous point.
					do
					{
						float radius = RandomHelper.Range(30f, maxNoteDistance);//<---- This could be a function of bpm, i.e. time-distance relation between beats

						pos = prevPoint + RandomHelper.OnCircle(prevPoint, radius);
					} while (!PlayField.Contains(pos));

					//@ANDREW: Look at AudioAnalyzer.CreatePeakDataAt(), and AudioAnalyzer.CreatePeakData().
					//         If you want to use music notes as you do here, use .CreatePeakDataAt().
					//         If you want to collect all the peak data at once,
					//         use .CreatePeakData() <--- but don't actually use this yet, it won't work right.
					// 
					//TODO: Now we need to consult the analyzer as we go.
					//      Since the time interval changes (halfnote vs quarter, etc), we have to 
					//      update the analyzer at the same interval as "timestamp"

					//@Andrew: It can be used like this. 
					//         Important Note: PeakData.value is in DECIBELS.
					//                         So threshold should also relate to DECIBELS.
					//       


					////EXAMPLE (NOT TESTED):
					//double threshold = Double.Parse("1.0E-40");
					//var peakData = analyzer.CreatePeakDataAt((int)timestamp, 450);
					//Console.WriteLine(peakData.ToString());
					//if (peakData.value < threshold)
					//{
					//	//Continue without adding a beat here if no sound was detected.
					//	timestamp += AddTime(NoteDuration.Half);
					//	continue;
					//}
					////END EXAMPLE

					if (currentBeat % beatsPerMeasure == 0)
					{
						// Generate a random slider.
						var sliderType = EnumHelper.GetRandom<SliderCurveType>(); //sliderTypes[rnd.Next(0, 3)];
						string sliderData = GetSliderData(pos, (int)timestamp, HitObjectType.Slider,
												HitObjectSoundType.None, sliderType, 1, sliderVelocity, RandomHelper.Range(minSliderCurves, maxSliderCurves + 1));

						osuFile.WriteLine(sliderData);

						numCircles++;

						timestamp += AddTime(NoteDuration.Half);
						currentBeat += (int)NoteDuration.Half;
					}
					else
					{
						string circleData = GetHitCircleData(pos, (int)timestamp, HitObjectType.Normal,
												HitObjectSoundType.None, prevPoint);

						osuFile.WriteLine(circleData);

						numCircles++;

						timestamp += AddTime(NoteDuration.Quarter);
						currentBeat += (int)NoteDuration.Quarter;

						prevPoint = pos;
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
			HitObjectSoundType hitsound, SliderCurveType sliderType, int repeat, float sliderVelocity, int numCurves)
		{
			// Length will determine how long a slider will go on for
			int len = (int)(sliderVelocity * 100);
			var hs = new HitSlider(startPosition, time, hitType, hitsound, sliderType, repeat, sliderVelocity, numCurves, len);

			return hs.SerializeForOsu();
		}
	}
}

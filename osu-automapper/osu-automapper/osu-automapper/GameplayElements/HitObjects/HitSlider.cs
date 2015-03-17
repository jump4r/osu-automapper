using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace osu_automapper
{
	class HitSlider : HitObject
	{
		// Properties

		// public List<Point> curvePoints { get; set; }
		private List<Vector2> curvePoints = new List<Vector2>();

		public SliderCurveType SliderType { get; set; }
		public int Repeat { get; set; }
		public int Length { get; set; }
		public float Velocity { get; set; }
		public int NumCurves { get; set; }

		private Vector2 startPosition = Vector2.Zero;
		// Slider Construtor
		public HitSlider() { }
		public HitSlider(Vector2 startPosition, int time, HitObjectType hitType, HitObjectSoundType hitsound,
			SliderCurveType sliderType, int repeat, float velocity, int numCurves, int length)
		{
			this.startPosition = startPosition;
			this.Position = startPosition;
			this.Time = time;

			this.HitSound = (int)hitsound;
			this.SliderType = sliderType;
			this.HitType = (int)hitType;

			this.Repeat = repeat;
			this.Velocity = velocity;
			this.Length = length;
			this.NumCurves = numCurves;



			ConstructSlider();
		}

		private void ConstructSlider()
		{
			switch (SliderType)
			{
				case SliderCurveType.Bezier: // Bezier Spline
					GenerateBezierSlider();
					break;
				case SliderCurveType.Linear: // Linear Slider
					GenerateLinearSlider();
					break;
				case SliderCurveType.PSpline: // P-spline
					SliderType = SliderCurveType.Bezier;
					GenerateBezierSlider();
					break;

			}
		}

		private void GenerateLinearSlider()
		{
			float angle = validAngles[RandomHelper.Range(0, validAngles.Length)];
			float x = Position.X + (float)Math.Cos(angle) * Length;
			float y = Position.X + (float)Math.Sin(angle) * Length;
			Vector2 endPoint = startPosition + new Vector2(x, y);

			// Add the slider
			curvePoints.Add(endPoint);
		}

		private void GenerateBezierSlider()
		{
			int angleIndex = RandomHelper.Range(0, validAngles.Length);
			Console.WriteLine("NEW Angle Here:");
			Console.WriteLine("Start Point - x: " + Position.X + ", y: " + Position.Y);

			for (int i = 0; i < NumCurves; i++)
			{
				curvePoints.Add(AddBezierSliderPoint(angleIndex));
			}
		}

		private Vector2 AddBezierSliderPoint(int angleIndex)
		{
			int offset = RandomHelper.Range(-1, 2);
			angleIndex = angleIndex + offset;

			if (angleIndex < 0)
			{
				angleIndex += validAngles.Length;
			}
			else if (angleIndex >= validAngles.Length)
			{
				angleIndex %= validAngles.Length;
			}

			var segmentLength = (float)Length / NumCurves;

			float angle = validAngles[angleIndex];
			float x = Position.X + (float)Math.Cos(angle) * segmentLength;
			float y = Position.Y + (float)Math.Sin(angle) * segmentLength;

			var oldPos = Position;
			Position = new Vector2(x, y);

			var playField = Beatmap.PlayField;

			if (!playField.Contains(Position))
			{
				Vector2 heading = Position - oldPos;
				Vector2 edgeDir = playField.FindOverlapEdge(Position);

				Position = Vector2.Project(heading, edgeDir) + oldPos;

				// Hack for corners. Proper collision solving required if you want consistency with distances.
				Position = playField.Clamp(Position);
			}

			Console.WriteLine("New Point - x: " + Position.X + ", y: " + Position.Y);

			return Position;
		}

		public override string SerializeForOsu()
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0},{1},{2},{3},{4},{5}|", MathHelper.RoundToInt(startPosition.X), MathHelper.RoundToInt(startPosition.Y),
				Time, ((int)HitType), ((int)HitSound), SliderType.GetSliderChar());

			int i;
			for (i = 0; i < curvePoints.Count - 1; i++)
			{
				builder.AppendFormat("{0}:{1}|", MathHelper.RoundToInt(curvePoints[i].X), MathHelper.RoundToInt(curvePoints[i].Y));
			}

			builder.AppendFormat("{0}:{1},{2},{3}", MathHelper.RoundToInt(curvePoints[i].X), MathHelper.RoundToInt(curvePoints[i].Y), Repeat, Length);

			return builder.ToString();
		}
	}

}

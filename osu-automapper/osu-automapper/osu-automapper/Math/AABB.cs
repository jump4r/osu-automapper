using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    /// <summary>
    /// Definition of the playfield in which the beatmap is generated in.
    /// </summary>
	public class AABB
	{
		public Vector2 Center { get; set; }
		public Vector2 HalfExtents { get; set; }

		public Vector2 BottomLeft
		{
			get { return new Vector2(Center.X - HalfExtents.X, Center.Y + HalfExtents.Y); }
		}

		public Vector2 BottomRight
		{
			get { return new Vector2(Center.X + HalfExtents.X, Center.Y + HalfExtents.Y); }
		}

		public Vector2 TopLeft
		{
			get { return new Vector2(Center.X - HalfExtents.X, Center.Y - HalfExtents.Y); }
		}

		public Vector2 TopRight
		{
			get { return new Vector2(Center.X + HalfExtents.X, Center.Y - HalfExtents.Y); }
		}

		public float Left
		{
			get { return Center.X - HalfExtents.X; }
		}

		public float Right
		{
			get { return Center.X + HalfExtents.X; }
		}

		public float Top
		{
			get { return Center.Y - HalfExtents.Y; }
		}

		public float Bottom
		{
			get { return Center.Y + HalfExtents.Y; }
		}

		public Vector2 Min
		{
			get { return TopLeft; }
		}

		public Vector2 Max
		{
			get { return BottomRight; }
		}

		// Assumes Y is down
		public AABB(Vector2 center, Vector2 halfExtents)
		{
			this.Center = center;
			this.HalfExtents = halfExtents;

			if (HalfExtents.X < 0 || HalfExtents.Y < 0)
			{
				throw new Exception("AABB cannot have negative half extents!");
			}
		}

		// Inclusive bounds check
		public bool Contains(Vector2 pt)
		{
			var min = Min;
			var max = Max;

			if (pt.X >= min.X && pt.X <= Max.X &&
				pt.Y >= min.Y && pt.Y <= Max.Y)
			{
				return true;
			}

			return false;
		}

		public Vector2 FindOverlapEdge(Vector2 pt)
		{
			Vector2 min = Min;
			Vector2 max = Max;
			float x = 0f;
			float y = 0f;

			if (pt.X < min.X) { y = -1; }
			else if (pt.X > max.X) { y = 1; }

			if (pt.Y < min.Y) { x = 1; }
			else if (pt.Y > max.Y) { x = -1; }

			return new Vector2(x, y).Normalized;
		}

		public Vector2 GetRandomPointInside()
		{
			float x = RandomHelper.Range(-HalfExtents.X, HalfExtents.X);
			float y = RandomHelper.Range(-HalfExtents.Y, HalfExtents.Y);

			return new Vector2(Center.X + x, Center.Y + y);
		}

		public Vector2 Clamp(Vector2 pt)
		{
			return pt.Clamp(Min, Max);
		}
	}
}

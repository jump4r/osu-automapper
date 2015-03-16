using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
	public struct Vector2
	{
		public static Vector2 Zero = new Vector2(0f, 0f);
		public static Vector2 One = new Vector2(1f, 1f);
		public static Vector2 NegativeOne = new Vector2(-1, -1);
		public static Vector2 Up = new Vector2(0, -1);
		public static Vector2 Down = new Vector2(0, 1);
		public static Vector2 Left = new Vector2(-1, 0);
		public static Vector2 Right = new Vector2(1, 0);

		public float X;
		public float Y;

		public float Magnitude
		{
			get { return (float)Math.Sqrt(X * X + Y * Y); }
		}

		public float SqrMagnitude
		{
			get { return (X * X + Y * Y); }
		}

		public Vector2 Normalized
		{
			get
			{
				float length = Magnitude;

				if (length > float.Epsilon)
				{
					return new Vector2(X / length, Y / length);
				}
				else
				{
					return this;
				}
			}
		}

		public Vector2 Perpendicular
		{
			get { return new Vector2(-Y, X); }
		}

		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2(Vector2 v)
		{
			this.X = v.X;
			this.Y = v.Y;
		}

		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return (lhs.X == rhs.X) && (lhs.Y == rhs.Y);
		}

		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

		public static Vector2 operator -(Vector2 v)
		{
			return new Vector2(-v.X, -v.Y);
		}

		public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
		}

		public static Vector2 operator *(Vector2 v, float f)
		{
			return new Vector2(v.X * f, v.Y * f);
		}

		public static Vector2 operator *(float f, Vector2 v)
		{
			return new Vector2(v.X * f, v.Y * f);
		}

		public static Vector2 operator /(Vector2 v, float f)
		{
			return new Vector2(v.X / f, v.Y / f);
		}

		public static Vector2 operator /(float f, Vector2 v)
		{
			return new Vector2(v.X / f, v.Y / f);
		}

		public static Vector2 Abs(Vector2 v)
		{
			return new Vector2((float)Math.Abs(v.X), (float)Math.Abs(v.Y));
		}

		public static float AngleBetween(Vector2 lhs, Vector2 rhs)
		{
			return (float)Math.Atan2(rhs.Y - lhs.Y, rhs.X - lhs.X);
		}

		public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max)
		{
			return new Vector2(MathHelper.Clamp(v.X, min.X, max.X), MathHelper.Clamp(v.Y, min.Y, max.Y));
		}

		public static float Distance(Vector2 lhs, Vector2 rhs)
		{
			return (rhs - lhs).Magnitude;
		}

		public static float DistanceSqr(Vector2 lhs, Vector2 rhs)
		{
			return (rhs - lhs).SqrMagnitude;
		}

		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.X * rhs.X + lhs.Y * rhs.Y;
		}

		public static float Cross(Vector2 lhs, Vector2 rhs)
		{
			return lhs.X * rhs.Y - lhs.Y * rhs.X;
		}

		public static Vector2 Project(Vector2 v, Vector2 onNormal)
		{
			return v.Dot(onNormal) * onNormal;
		}

		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			return inDirection + (2f * inDirection.Dot(inNormal) * -inDirection);
		}

		public float AngleBetween(Vector2 v2)
		{
			return (float)Math.Atan2(v2.Y - Y, v2.X - X);
		}

		public Vector2 Clamp(Vector2 min, Vector2 max)
		{
			return new Vector2(MathHelper.Clamp(X, min.X, max.X), MathHelper.Clamp(Y, min.Y, max.Y));
		}

		public float Distance(Vector2 v2)
		{
			return (v2 - this).Magnitude;
		}

		public float DistanceSqr(Vector2 v2)
		{
			return (v2 - this).SqrMagnitude;
		}

		public float Dot(Vector2 v2)
		{
			return X * v2.X + Y * v2.Y;
		}

		public float Cross(Vector2 v2)
		{
			return X * v2.Y - Y * v2.X;
		}

		public void Normalize()
		{
			float length = Magnitude;

			if (length > float.Epsilon)
			{
				X /= length;
				Y /= length;
			}
		}

		public Vector2 Reflect(Vector2 inNormal)
		{
			return this + (2f * this.Dot(inNormal) * -this);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector2)) { return false; }

			var v = (Vector2)obj;

			return (v.X == X) && (v.Y == Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 37 + X.GetHashCode();
				hash = hash * 37 * Y.GetHashCode();
				return hash;
			}
		}
	}
}

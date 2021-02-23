using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panther
{
    public struct AABB// : PositionedObject
    {
        #region Fields
        public float X;
        public float Y;
        public float Width;
        public float Height;
        #endregion
        #region Properties
        public float Left 
        {
            get { return X - (Width / 2); }
        }

        public float Right
        { 
            get { return X + (Width / 2); }
        }

        public float Top
        {
            get { return Y + (Height / 2); }
        }

        public float Bottom
        {
            get { return Y + (Height / 2); }
        }
        #endregion
        #region Constructor
        public AABB(float width, float height)
        {
            Width = width;
            Height = height;
            X = 0;
            Y = 0;
        }

        public AABB(float x, float y, float width, float height)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }
        #endregion
        #region Public Methods
        public static bool operator ==(AABB a, AABB b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(AABB a, AABB b)
        {
            return !(a == b);
        }

        public bool Contains(float x, float y)
        {
            return ((((X <= x) && (x < (X + Width))) && (Y <= y)) && (y < (Y + Height)));
        }

        public bool Contains(Vector2 value)
        {
            return Contains(new PointFloat(value.X, value.Y));
        }

        public bool Contains(Vector3 value)
        {
            return Contains(new Vector2(value.X, value.Y));
        }

        public bool Contains(PointFloat value)
        {
            return (((value.X > (X - Width / 2))) && (value.Y > (Y - Height / 2)) &&
                (value.X < (X + Width / 2)) && (value.Y < (Y + Height / 2)));
        }

        public bool Contains(AABB value)
        {
            return ((((X <= value.X) && ((value.X + value.Width / 2) <= (X + Width / 2))) && (Y <= value.Y)) &&
                ((value.Y + value.Height / 2) <= (Y + Height / 2)));
        }

        public void Offset(PointFloat offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public void Inflate(float horizontalValue, float verticalValue)
        {
            X -= horizontalValue;
            Y -= verticalValue;
            Width += horizontalValue * 2;
            Height += verticalValue * 2;
        }

        public bool Equals(AABB other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is AABB) ? this == ((AABB)obj) : false;
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Width:{2} Height:{3}}}", X, Y, Width, Height);
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y ^ (int)Width ^ (int)Height;
        }

        public bool Intersects(AABB other)
        {
            return !(other.Left > Right || other.Right < Left || other.Top > Bottom || other.Bottom < Top);
        }


        public void Intersects(ref AABB value, out bool result)
        {
            result = !(value.Left > Right || value.Right < Left || value.Top > Bottom || value.Bottom < Top);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}

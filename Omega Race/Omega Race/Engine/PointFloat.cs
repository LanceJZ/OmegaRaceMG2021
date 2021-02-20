using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panther
{
    public struct PointFloat
    {
        #region Fields
        private static PointFloat zeroPoint = new PointFloat();
        public float X;
        public float Y;
        #endregion
        #region Properties
        public static PointFloat Zero
        {
            get { return zeroPoint; }
        }
        #endregion
        #region Constructors

        public PointFloat(float x, float y)
        {
            X = x;
            Y = y;
        }
        #endregion
        #region Public Methods
        public static bool operator ==(PointFloat a, PointFloat b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PointFloat a, PointFloat b)
        {
            return !a.Equals(b);
        }

        public bool Equals(PointFloat other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        public override bool Equals(object obj)
        {
            return (obj is Point) ? Equals((Point)obj) : false;
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y;
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panther;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omega_Race.Entities
{
    public class BorderBounce : VectorModel
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public BorderBounce(Game game, Camera camera) : base(game, camera)
        {

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            base.LoadContent();

        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
        #endregion
        #region Public Methods
        #endregion
        #region Protected Methods
        protected void HitBoundry()
        {
            if (PO.OffScreenTopBottom())
            {
                PO.Velocity.Y = BoundryBounce(Velocity.Y);
                MoveFromBoundryY();
                return;
            }
            else if (PO.OffScreenSide())
            {
                PO.Velocity.X = BoundryBounce(Velocity.X);
                MoveFromBoundryX();
                return;
            }

            float upperY = Main.instance.InsideUpperLeft.Y;
            float lowerY = Main.instance.InsideLowerRight.Y;
            float leftX = Main.instance.InsideUpperLeft.X;
            float rightX = Main.instance.InsideLowerRight.X;

            if (Y - PO.Radius < upperY && Y + PO.Radius > lowerY)
            {
                if (X - PO.Radius < rightX && X + PO.Radius > leftX)
                {
                    float buffer = 0.75f;

                    if (Y - PO.Radius * buffer < upperY && Y + PO.Radius * buffer > lowerY)
                    {
                        PO.Velocity.X = BoundryBounce(Velocity.X);
                        MoveFromBoundryX();
                    }
                    else
                    {
                        PO.Velocity.Y = BoundryBounce(Velocity.Y);
                        MoveFromBoundryY();
                    }

                    if (Y - PO.Radius * 1.1f < upperY && Y + PO.Radius * 1.1f > lowerY)
                    {
                        PO.Velocity.Y = BoundryBounce(Velocity.Y);
                        MoveFromBoundryY();
                    }
                }
            }
        }
        #endregion
        #region Private Methods
        void MoveFromBoundryX()
        {
            PO.Position.X += PO.Velocity.X * 0.1f;
        }

        void MoveFromBoundryY()
        {
            PO.Position.Y += PO.Velocity.Y * 0.1f;
        }

        float BoundryBounce(float velocity)
        {
            Acceleration = Vector3.Zero;
            velocity *= -1;
            velocity *= 0.9f;

            return velocity;
        }
        #endregion
    }
}

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
        protected void CheckInsideBoundry()
        {
            if (Main.instance.InsideTopCollusion.Contains(Position))
            {
                PO.Velocity.Y = BoundryBounce(Velocity.Y);
                MoveFromBoundryY();
            }

            if (Main.instance.InsideBottomCollusion.Contains(Position))
            {
                PO.Velocity.Y = BoundryBounce(Velocity.Y);
                MoveFromBoundryY();
            }

            if (Main.instance.InsideLeftCollusion.Contains(Position))
            {
                PO.Velocity.X = BoundryBounce(Velocity.X);
                MoveFromBoundryX();
            }

            if (Main.instance.InsideRightCollusion.Contains(Position))
            {
                PO.Velocity.X = BoundryBounce(Velocity.X);
                MoveFromBoundryX();
            }
        }

        protected void CheckOutsideBoundry()
        {
            if (Main.instance.OutsideTopCollusion.Contains(Position))
            {
                PO.Velocity.Y = BoundryBounce(Velocity.Y);
                MoveFromBoundryY();
                return;
            }

            if (Main.instance.OutsideBottomCollusion.Contains(Position))
            {
                PO.Velocity.Y = BoundryBounce(Velocity.Y);
                MoveFromBoundryY();
                return;
            }

            if (Main.instance.OutsideLeftCollusion.Contains(Position))
            {
                PO.Velocity.X = BoundryBounce(Velocity.X);
                MoveFromBoundryX();
                return;
            }

            if (Main.instance.OutsideRightCollusion.Contains(Position))
            {
                PO.Velocity.X = BoundryBounce(Velocity.X);
                MoveFromBoundryX();
                return;
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

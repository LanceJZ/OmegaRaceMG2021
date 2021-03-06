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
    public class DroidShip : Enemy
    {
        //After the first wave, they slowly move on a track.
        #region Fields
        protected Vector3[] droidPath; // 0=Top Left, 1=Top Right, 2=Bottom Right, 3=Bottom Left.
        protected bool firstWave = false;
        protected bool clockwise = false;
        protected bool command = false;
        #endregion
        #region Properties
        public bool FirstWave { get => firstWave; set => firstWave = value; }
        public bool Clockwise { get => clockwise; set => clockwise = value; }
        public bool Command { get => command; }
        public Vector3[] DroidPath { get => droidPath; set => droidPath = value; }
        #endregion
        #region Constructor
        public DroidShip(Game game, Camera camera) : base(game, camera)
        {

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();

            points = 1000;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void BeginRun()
        {
            command = false;
        }

        public override void Spawn(Vector3 position)
        {
            base.Spawn(position);

        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!firstWave)
            {
                FollowPath();
            }
        }
        #endregion
        #region Public Methods
        #endregion
        #region Protected Methods
        protected override void Terminate()
        {
            base.Terminate();

            Main.instance.TheEnemies.DroidCount();
        }

        protected void FollowPath() // 0=Top Left, 1=Top Right, 2=Bottom Right, 3=Bottom Left.
        {
            if (droidPath == null)
                return;

            if (clockwise)
            {
                if (X < droidPath[3].X && Y == droidPath[3].Y)
                {
                    PO.Velocity.Y = Velocity.X * -1;
                    PO.Velocity.X = 0;
                    X = droidPath[3].X;
                }

                if (Y > droidPath[0].Y && X == droidPath[0].X)
                {
                    PO.Velocity.X = Velocity.Y;
                    PO.Velocity.Y = 0;
                    Y = droidPath[0].Y;
                }

                if (X > droidPath[1].X && Y == droidPath[1].Y)
                {
                    PO.Velocity.Y = Velocity.X * -1;
                    PO.Velocity.X = 0;
                    X = droidPath[1].X;
                }

                if (Y < droidPath[2].Y && X == droidPath[2].X)
                {
                    PO.Velocity.X = Velocity.Y;
                    PO.Velocity.Y = 0;
                    Y = droidPath[2].Y;
                }
            }
            else // 0=Top Left, 1=Top Right, 2=Bottom Right, 3=Bottom Left.
            {
                if (X > droidPath[2].X && Y == droidPath[2].Y)
                {
                    PO.Velocity.Y = Velocity.X;
                    PO.Velocity.X = 0;
                    X = droidPath[2].X;
                }

                if (Y > droidPath[1].Y && X == droidPath[1].X)
                {
                    PO.Velocity.X = Velocity.Y * -1;
                    PO.Velocity.Y = 0;
                    Y = droidPath[1].Y;
                }

                if (X < droidPath[0].X && Y == droidPath[0].Y)
                {
                    PO.Velocity.Y = Velocity.X;
                    PO.Velocity.X = 0;
                    X = droidPath[0].X;
                }

                if (Y < droidPath[3].Y && X == droidPath[3].X)
                {
                    PO.Velocity.X = Velocity.Y * -1;
                    PO.Velocity.Y = 0;
                    Y = droidPath[3].Y;
                }
            }
        }
        #endregion
        #region Private Methods
        #endregion
    }
}

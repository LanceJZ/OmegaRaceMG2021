#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panther;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace Omega_Race.Entities
{
    public class Player : BorderBounce
    {
        #region Fields
        Camera cameraRef;
        VectorModel flame;
        Timer flameTimer;
        Color color = new Color(190, 190, 255);
        float thrustAmount = 26.666f;
        float deceleration = 0.0666f;
        float maxVelocity = 20.666f;
        List<Shot> shotList = new List<Shot>();
        
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Player(Game game, Camera camera) : base(game, camera)
        {
            cameraRef = camera;
            flame = new VectorModel(game, camera);
            flameTimer = new Timer(game, 0.015f);
            //Enabled = false;

            for (int i = 0; i < 4; i++)
            {
                shotList.Add(new Shot(game, camera));
            }

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            LoadVectorModel("PlayerShip", color);
            flame.LoadVectorModel("PlayerFlame", color);

            base.LoadContent();
        }

        public void BeginRun()
        {
            foreach (Shot shot in shotList)
            {
                shot.BeginRun();
            }

            flame.AddAsChildOf(this);

            Y = Core.ScreenHeight / 1.2f;
            X = -Core.ScreenWidth / 1.2f;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HitBoundry();
            GetKeys();
        }
        #endregion
        #region Public
        #endregion
        #region Private

        void GetKeys()
        {
            float rotationAmound = MathHelper.Pi;

            if (Core.KeyDown(Keys.W) || Core.KeyDown(Keys.Up))
            {
                ThrustOn();
            }
            else
            {
                ThrustOff();
            }

            if (Core.KeyDown(Keys.A) || Core.KeyDown(Keys.Left))
            {
                PO.RotationVelocity.Z = rotationAmound;
            }
            else if (Core.KeyDown(Keys.D) || Core.KeyDown(Keys.Right))
            {
                PO.RotationVelocity.Z = -rotationAmound;
            }
            else
            {
                PO.RotationVelocity.Z = 0;
            }

            if (Core.KeyPressed(Keys.LeftControl) || Core.KeyPressed(Keys.Space))
            {
                Fire();
            }
        }

        void ThrustOn()
        {
            if (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) < maxVelocity)
            {
                //if (thrustSound.State != SoundState.Playing)
                //{
                //    thrustSound.Play();
                //}

                Acceleration = Core.VelocityFromAngleZ(Rotation.Z, thrustAmount);

                if (flameTimer.Elapsed)
                {
                    flame.Enabled = !flame.Enabled;
                    flameTimer.Reset();
                }

                flame.PO.UpdateChild();
                flame.UpdateMatrix();
            }
            else
            {
                Acceleration = -Velocity * deceleration * 5;
            }
        }

        void ThrustOff()
        {
            Acceleration = -Velocity * deceleration;
            //thrustSound.Stop();
            flame.Enabled = false;
        }

        void Fire()
        {
            Vector3 dir = Core.VelocityFromAngleZ(Rotation.Z, 36.66f);
            Vector3 offset = Core.VelocityFromAngleZ(Rotation.Z, PO.Radius);

            foreach (Shot shot in shotList)
            {
                if (!shot.Enabled)
                {
                    //fireSound.Play(0.25f, 0, 0);
                    shot.Spawn(Position + offset, dir + (Velocity * 0.75f), 1.25f);
                    shot.Rotation = Rotation;
                    break;
                }
            }
        }
        #endregion
    }
}

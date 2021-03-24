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
    public class Enemy : VectorModel
    {
        #region Fields
        protected int points;
        protected Color color = new Color(180, 180, 255);
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Enemy(Game game, Camera camera) : base(game, camera)
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

        public virtual void BeginRun()
        {
            Enabled = true;
            UpdateMatrix();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckCollision();
        }
        #endregion
        #region Public Methods
        #endregion
        #region Protected Methods
        virtual protected void CheckCollision()
        {
            foreach (Shot shot in Main.instance.ThePlayer.Shots)
            {
                if (shot.Enabled)
                {
                    if (CirclesIntersect(shot))
                    {
                        shot.Enabled = false;
                        Terminate();
                    }
                }
            }

            if (Main.instance.ThePlayer.Enabled)
            {
                if (CirclesIntersect(Main.instance.ThePlayer))
                {
                    Terminate();
                    Main.instance.ThePlayer.Reset();
                    Main.instance.TheEnemies.ResetWave();
                }
            }
        }

        virtual protected void Terminate()
        {
            Enabled = false;
            Main.instance.PlayerScore(points);
        }

        protected void Explode()
        {

        }
        #endregion
        #region Protected Methods
        protected void Fire(Shot shot)
        {
            float angle = AimedFire();
            Vector3 dir = Core.VelocityFromAngleZ(angle, 16.66f);
            Vector3 offset = Core.VelocityFromAngleZ(angle, PO.Radius);

            //fireSound.Play(0.25f, 0, 0);
            shot.Spawn(Position + offset, new Vector3(0, 0, angle), dir, 1.25f);
        }
        protected float AimedFire()
        {
            float percentChance = 0.25f - (Main.instance.Score * 0.00001f);

            if (percentChance < 0)
            {
                percentChance = 0;
            }

            return PO.AngleFromVectorsZ(Main.instance.ThePlayer.Position) +
                Core.RandomMinMax(-percentChance, percentChance);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}

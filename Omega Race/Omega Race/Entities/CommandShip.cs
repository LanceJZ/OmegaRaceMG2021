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
    public class CommandShip : DroidShip
    {
        //Drops mines every few seconds.
        #region Fields
        VectorModel inside;
        Shot shot;
        Timer shotTimer;
        Color color = new Color(180, 180, 255);
        bool leader = true;
        #endregion
        #region Properties
        public bool Leader { set => leader = value; }
        #endregion
        #region Constructor
        public CommandShip(Game game, Camera camera) : base(game, camera)
        {
            inside = new VectorModel(game, camera);
            shot = new Shot(game, camera);
            shotTimer = new Timer(game, 3);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();
            points = 1500;
            inside.AddAsChildOf(this, false, false);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            LoadVectorModel("DroidShip", color);
            inside.LoadVectorModel("InsideEnemy", color);
        }

        public new void BeginRun()
        {
            base.BeginRun();
            shot.BeginRun();
            command = true;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (leader)
            {
                LeadGaurd();
            }
            else
            {
                RearGaurd();
            }

            CheckCollision();
        }
        #endregion
        #region Public Methods
        #endregion
        #region Private Methods
        void LeadGaurd()
        {
            if (shotTimer.Elapsed)
            {
                shotTimer.Reset();
                Fire();
            }
        }

        void RearGaurd()
        {

        }

        protected override void CheckCollision()
        {
            base.CheckCollision();

            if (shot.Enabled)
            {
                if (shot.CirclesIntersect(Main.instance.ThePlayer))
                {
                    shot.Enabled = false;
                    Main.instance.ThePlayer.Reset();
                }
            }
        }
        void Fire()
        {
            float angle = AimedFire();
            Vector3 dir = Core.VelocityFromAngleZ(angle, 16.66f);
            Vector3 offset = Core.VelocityFromAngleZ(angle, PO.Radius);

            //fireSound.Play(0.25f, 0, 0);
            shot.Spawn(Position + offset, new Vector3(0, 0, angle), dir, 1.25f);
        }
        #endregion
        float AimedFire()
        {
            float percentChance = 0.25f - (Main.instance.Score * 0.00001f);

            if (percentChance < 0)
            {
                percentChance = 0;
            }

            return PO.AngleFromVectorsZ(Main.instance.ThePlayer.Position) +
                Core.RandomMinMax(-percentChance, percentChance);
        }

    }
}

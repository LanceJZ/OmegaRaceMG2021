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
    public class DeathShip : Enemy
    {
        //Drops mines every few seconds, chases player.
        #region Fields
        BorderBounce borderBounce;
        Shot shot;
        Timer shotTimer;
        VectorModel[] blades = new VectorModel[2];

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public DeathShip(Game game, Camera camera) : base(game, camera)
        {
            blades[0] = new VectorModel(game, camera);
            blades[1] = new VectorModel(game, camera);
            borderBounce = new BorderBounce(game, camera);
            shot = new Shot(game, camera);
            shotTimer = new Timer(game, 2);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();

            blades[1].PO.Rotation.Z = MathF.PI / 2;

            foreach(VectorModel blade in blades)
            {
                blade.AddAsChildOf(this);
                blade.PO.RotationVelocity.Z = 10.75f;
            }

            borderBounce.Moveable = false;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            LoadVectorModel("PhotonMine", color);

            foreach (VectorModel blade in blades)
            {
                blade.LoadVectorModel("DeathShipBlade", color);
            }
        }

        public override void BeginRun()
        {
            base.BeginRun();

            Enabled = true;
            shot.BeginRun();
            UpdateMatrix();
            Velocity = Vector3.Zero;

            foreach (VectorModel blade in blades)
            {
                blade.UpdateMatrix();
            }
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            borderBounce.Position = Position;
            borderBounce.Velocity = Velocity;

            if (shotTimer.Elapsed)
            {
                shotTimer.Reset();
                Fire(shot);
            }

        }
        #endregion
        #region Public Methods
        #endregion
        #region Protected Methods
        protected override void Terminate()
        {
            base.Terminate();

        }
        #endregion
        #region Private Methods
        void ChasePlayer()
        {

        }

        #endregion
    }
}

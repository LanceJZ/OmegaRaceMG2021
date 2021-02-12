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
    public class DeathShip : VectorModel
    {
        #region Fields
        Camera cameraRef;
        VectorModel[] blades = new VectorModel[2];
        Color color = new Color(190, 190, 255);

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public DeathShip(Game game, Camera camera) : base(game, camera)
        {
            cameraRef = camera;

            blades[0] = new VectorModel(game, camera);
            blades[1] = new VectorModel(game, camera);
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

        public void BeginRun()
        {
            Y = -Core.ScreenHeight / 1.75f;
            X = Core.ScreenWidth / 1.2f;

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
        #region Private Methods
        #endregion
    }
}

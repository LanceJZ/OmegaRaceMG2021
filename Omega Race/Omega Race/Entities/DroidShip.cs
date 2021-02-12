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
    public class DroidShip : VectorModel
    {
        #region Fields
        Camera cameraRef;
        Color color = new Color(190, 190, 255);
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public DroidShip(Game game, Camera camera) : base(game, camera)
        {
            cameraRef = camera;

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();
            PO.RotationVelocity.Z = Core.RandomMinMax(1, 2.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            LoadVectorModel("DroidShip", color);
        }

        public void BeginRun()
        {
            Y = -Core.ScreenHeight / 1.2f;
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

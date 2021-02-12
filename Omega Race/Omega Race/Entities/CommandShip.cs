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
    public class CommandShip : VectorModel
    {
        #region Fields
        Camera cameraRef;
        VectorModel inside;
        Color color = new Color(190, 190, 255);
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public CommandShip(Game game, Camera camera) : base(game, camera)
        {
            cameraRef = camera;
            inside = new VectorModel(game, camera);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();
            inside.AddAsChildOf(this, false, false);

            PO.RotationVelocity.Z = Core.RandomMinMax(1, 2.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            LoadVectorModel("DroidShip", color);
            inside.LoadVectorModel("InsideEnemy", color);
        }

        public void BeginRun()
        {
            Y = -Core.ScreenHeight / 1.2f;
            X = Core.ScreenWidth / 1.4f;

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

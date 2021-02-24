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
    public class Border : VectorModel
    {
        #region Fields
        Timer flickerTimer;
        Timer flashOnTimer;
        Timer flashOffTimer;
        bool flickering = false;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Border(Game game, Camera camera) : base(game, camera)
        {
            flickerTimer = new Timer(game, 0.5f);
            flashOnTimer = new Timer(game, 0.15f);
            flashOffTimer = new Timer(game, 0.1f);

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

        public void BeginRun()
        {
            DefuseColor = new Vector3(0.5f, 0.5f, 0.5f);
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (flickering)
            {
                if (flickerTimer.Elapsed)
                {
                    flickering = false;
                    Normal();
                    return;
                }

                if (flashOnTimer.Elapsed)
                {
                    flashOnTimer.Reset();
                    Bright();
                    return;
                }

                if (flashOffTimer.Elapsed)
                {
                    flashOffTimer.Reset();
                    Normal();
                }
            }
        }
        #endregion
        #region Public Methods
        public void Trigger()
        {
            flickering = true;
            flickerTimer.Reset();
        }
        #endregion
        #region Private Methods
        void Normal()
        {
            DefuseColor = new Vector3(0.5f, 0.5f, 0.5f);
        }

        void Bright()
        {
            DefuseColor = new Vector3(1, 1, 1);
        }
        #endregion
    }
}

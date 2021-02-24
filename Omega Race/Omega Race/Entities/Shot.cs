using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
using Panther;

namespace Omega_Race.Entities
{
    public class Shot : VectorModel
    {
        #region Fields
        Camera cameraRef;
        Timer life;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Shot(Game game, Camera camera) : base(game, camera)
        {
            cameraRef = camera;
            life = new Timer(game);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();
            Enabled = false;
            LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
        }

        public void BeginRun()
        {
            Vector3[] lineVertex = { new Vector3(-0.3f, 0, 0), new Vector3(0.3f, 0, 0) };
            InitializePoints(lineVertex, "Shot");
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (life.Elapsed)
            {
                Enabled = false;
            }

            if (Main.instance.TheBorders.InsideTopCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.TopInside.Trigger();
            }

            if (Main.instance.TheBorders.InsideBottomCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.BottomInside.Trigger();
            }

            if (Main.instance.TheBorders.InsideLeftCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.LeftInside.Trigger();
            }

            if (Main.instance.TheBorders.InsideRightCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.RightInside.Trigger();
            }

            if (Main.instance.TheBorders.OutsideTopCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.TopOutside.Trigger();
            }

            if (Main.instance.TheBorders.OutsideBottomCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.BottomOutside.Trigger();
            }

            if (Main.instance.TheBorders.OutsideLeftCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.LeftOutside.Trigger();
            }

            if (Main.instance.TheBorders.OutsideRightCollision.Contains(Position))
            {
                Enabled = false;
                Main.instance.TheBorders.RightOutside.Trigger();
            }
        }
        #endregion
        #region Public Methods
        public void Spawn(Vector3 position, Vector3 rotation, Vector3 velocity, float timer)
        {
            Spawn(position, velocity);
            Rotation = rotation;
            UpdateMatrix();
            life.Reset(timer);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}

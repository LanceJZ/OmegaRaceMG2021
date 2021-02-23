using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panther;
using Omega_Race.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omega_Race
{
    public class BorderController : GameComponent
    {
        #region Fields
        Border topInsideLine;
        Border bottomInsideLine;
        Border leftInsideLine;
        Border rightInsideLine;
        Border topOutsideLine;
        Border bottomOutsideLine;
        Border leftOutsideLine;
        Border rightOutsideLine;
        #endregion
        #region Properties
        //public Vector2 InsideUpperLeft { get => insideUpperLeft; }
        //public Vector2 InsideLowerRight { get => insideLowerRight; }
        public AABB InsideTopCollision { get => topInsideLine.PO.BoundingBox; }
        public AABB InsideBottomCollision { get => bottomInsideLine.PO.BoundingBox; }
        public AABB InsideLeftCollision { get => leftInsideLine.PO.BoundingBox; }
        public AABB InsideRightCollision { get => rightInsideLine.PO.BoundingBox; }
        public AABB OutsideTopCollision { get => topOutsideLine.PO.BoundingBox; }
        public AABB OutsideBottomCollision { get => bottomOutsideLine.PO.BoundingBox; }
        public AABB OutsideLeftCollision { get => leftOutsideLine.PO.BoundingBox; }
        public AABB OutsideRightCollision { get => rightOutsideLine.PO.BoundingBox; }

        #endregion
        #region Constructor
        public BorderController(Game game, Camera camera) : base(game)
        {
            topInsideLine = new Border(Game, camera);
            bottomInsideLine = new Border(Game, camera);
            leftInsideLine = new Border(Game, camera);
            rightInsideLine = new Border(Game, camera);
            topOutsideLine = new Border(Game, camera);
            bottomOutsideLine = new Border(Game, camera);
            leftOutsideLine = new Border(Game, camera);
            rightOutsideLine = new Border(Game, camera);

            game.Components.Add(this);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {
            base.Initialize();

        }

        public void LoadContent()
        {

        }

        public void BeginRun()
        {
            float size = 1f;

            topOutsideLine.Y = Core.ScreenHeight / 1.01f;
            bottomOutsideLine.Y = -Core.ScreenHeight / 1.01f;
            leftOutsideLine.X = -Core.ScreenWidth / 1.01f;
            rightOutsideLine.X = Core.ScreenWidth / 1.01f;
            leftInsideLine.X = -(Core.ScreenWidth / 1.6f) + size;
            rightInsideLine.X = (Core.ScreenWidth / 1.6f) - size;
            topInsideLine.Y = (Core.ScreenHeight / 4) - size;
            bottomInsideLine.Y = -(Core.ScreenHeight / 4) + size;

            float horzLineSize = leftInsideLine.X;
            float vertLineSize = topInsideLine.Y;
            Vector3[] horzLineVertex = { new Vector3(horzLineSize, 0, 0), new Vector3(-horzLineSize, 0, 0) };
            Vector3[] vertLineVertex = { new Vector3(0, vertLineSize, 0), new Vector3(0, -vertLineSize, 0) };
            topInsideLine.InitializePoints(horzLineVertex, Color.Gray, "Top Inside Line");
            bottomInsideLine.InitializePoints(horzLineVertex, Color.Gray, "Bottom Inside Line");
            leftInsideLine.InitializePoints(vertLineVertex, Color.Gray, "Left Inside Line");
            rightInsideLine.InitializePoints(vertLineVertex, Color.Gray, "Right Inside Line");
            Vector3[] outsideHorzLineVertix = { new Vector3(-Core.ScreenWidth / 1.01f, 0, 0),
                new Vector3(Core.ScreenWidth / 1.01f, 0, 0) };
            Vector3[] outsideVertLineVertex = { new Vector3(0, Core.ScreenHeight / 1.01f, 0),
                new Vector3(0, -Core.ScreenHeight / 1.01f, 0) };
            topOutsideLine.InitializePoints(outsideHorzLineVertix, Color.Gray, "Top Line");
            bottomOutsideLine.InitializePoints(outsideHorzLineVertix, Color.Gray, "Bottom Line");
            leftOutsideLine.InitializePoints(outsideVertLineVertex, Color.Gray, "Left Line");
            rightOutsideLine.InitializePoints(outsideVertLineVertex, Color.Gray, "Right Line");

            leftInsideLine.PO.BoundingBox = new AABB(size, (topInsideLine.Y * 2) + size);
            rightInsideLine.PO.BoundingBox = new AABB(size, (topInsideLine.Y * 2) + size);
            topInsideLine.PO.BoundingBox = new AABB((rightInsideLine.X * 2) + size, size);
            bottomInsideLine.PO.BoundingBox = new AABB((rightInsideLine.X * 2) + size, size);
            leftOutsideLine.PO.BoundingBox = new AABB(size, Core.ScreenHeight * 2);
            rightOutsideLine.PO.BoundingBox = new AABB(size, Core.ScreenWidth * 2);
            topOutsideLine.PO.BoundingBox = new AABB(Core.ScreenWidth * 2, size);
            bottomOutsideLine.PO.BoundingBox = new AABB(Core.ScreenWidth * 2, size);
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
        #endregion
        #region Public Methods
        public void OutsideTopHit()
        {

        }

        public void OutsideRightHit()
        {

        }

        public void OutsideBottomHit()
        {

        }

        public void OutsideLeftHit()
        {

        }

        public void InsideTopHit()
        {

        }

        public void InsideRightHit()
        {

        }

        public void InsideBottomHit()
        {

        }

        public void InsideLeftHit()
        {

        }
        #endregion
        #region Private Methods
        #endregion
    }
}

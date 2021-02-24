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
        public AABB InsideTopCollision { get => topInsideLine.PO.BoundingBox; }
        public AABB InsideBottomCollision { get => bottomInsideLine.PO.BoundingBox; }
        public AABB InsideLeftCollision { get => leftInsideLine.PO.BoundingBox; }
        public AABB InsideRightCollision { get => rightInsideLine.PO.BoundingBox; }
        public AABB OutsideTopCollision { get => topOutsideLine.PO.BoundingBox; }
        public AABB OutsideBottomCollision { get => bottomOutsideLine.PO.BoundingBox; }
        public AABB OutsideLeftCollision { get => leftOutsideLine.PO.BoundingBox; }
        public AABB OutsideRightCollision { get => rightOutsideLine.PO.BoundingBox; }
        public Border TopInside { get => topInsideLine; }
        public Border BottomInside { get => bottomInsideLine; }
        public Border LeftInside { get => leftInsideLine; }
        public Border RightInside { get => rightInsideLine; }
        public Border TopOutside { get => topOutsideLine; }
        public Border BottomOutside { get => bottomOutsideLine; }
        public Border LeftOutside { get => leftOutsideLine; }
        public Border RightOutside { get => rightOutsideLine; }
        #endregion
        #region Constructor
        // TODO: Make blinking borders, when hit it blinks.
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
            Vector3[] outsideHorzLineVertix = { new Vector3(-Core.ScreenWidth / 1.01f, 0, 0),
                new Vector3(Core.ScreenWidth / 1.01f, 0, 0) };
            Vector3[] outsideVertLineVertex = { new Vector3(0, Core.ScreenHeight / 1.01f, 0),
                new Vector3(0, -Core.ScreenHeight / 1.01f, 0) };

            topInsideLine.InitializePoints(horzLineVertex, "Top Inside Line");
            bottomInsideLine.InitializePoints(horzLineVertex, "Bottom Inside Line");
            leftInsideLine.InitializePoints(vertLineVertex, "Left Inside Line");
            rightInsideLine.InitializePoints(vertLineVertex, "Right Inside Line");
            topOutsideLine.InitializePoints(outsideHorzLineVertix, "Top Line");
            bottomOutsideLine.InitializePoints(outsideHorzLineVertix, "Bottom Line");
            leftOutsideLine.InitializePoints(outsideVertLineVertex, "Left Line");
            rightOutsideLine.InitializePoints(outsideVertLineVertex, "Right Line");

            topInsideLine.BeginRun();
            bottomInsideLine.BeginRun();
            leftInsideLine.BeginRun();
            rightInsideLine.BeginRun();
            topOutsideLine.BeginRun();
            bottomOutsideLine.BeginRun();
            leftOutsideLine.BeginRun();
            rightOutsideLine.BeginRun();

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
        #endregion
        #region Private Methods
        #endregion
    }
}

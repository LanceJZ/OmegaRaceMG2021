using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panther;
using System;
using System.Collections.Generic;
using System.Linq;
using Omega_Race.Entities;

namespace Omega_Race
{
    public class EnemyController : GameComponent
    {
        #region Fields
        Camera cameraRef;
        List<DroidShip> droids;
        CommandShip leadCommand;
        CommandShip rearCommand;
        DeathShip death;
        Vector3[] droidmodelFile;
        Color color = new Color(180, 180, 255);
        float count = 5;
        bool clockwise = false;
        #endregion
        #region Properties
        #endregion
        #region Constructor
        public EnemyController(Game game, Camera camera) : base(game)
        {
            cameraRef = camera;

            droids = new List<DroidShip>();
            leadCommand = new CommandShip(game, camera);
            rearCommand = new CommandShip(game, camera);
            death = new DeathShip(game, camera);

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
            FileIO fileIO = new FileIO();

            droidmodelFile = fileIO.ReadVectorModelFile("DroidShip");
        }

        public void BeginRun()
        {
            leadCommand.BeginRun();
            leadCommand.Leader = true;
            rearCommand.BeginRun(); //Takes five seconds after wave start to appear. Drops mines. Turns into Death ship.
            rearCommand.Enabled = false;
            death.BeginRun();
            death.Enabled = false;

        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
        #endregion
        #region Public Methods
        public void DroidCount()
        {
            bool allGone = true;

            foreach (DroidShip droid in droids)
            {
                if (droid.Enabled)
                {
                    allGone = false;
                }
            }

            if (leadCommand.Enabled)
            {
                allGone = false;
            }

            if (rearCommand.Enabled)
            {
                allGone = false;
            }

            if (allGone)
            {
                if (count < 9)
                    count += 2;

                NewWave();
            }
            else if (!leadCommand.Enabled)
            {
                CommandShipRespawn();
            }

        }

        public void CommandShipRespawn()
        {
            DroidShip thisDroid = null;

            foreach(DroidShip droidShip in droids)
            {
                if (droidShip.Enabled)
                {
                    thisDroid = droidShip;
                    break;
                }
            }

            if (thisDroid == null)
            {
                return;
            }

            thisDroid.Enabled = false;
            leadCommand.Spawn(thisDroid.Position);
            leadCommand.DroidPath = thisDroid.DroidPath;
            leadCommand.Velocity = thisDroid.Velocity * 2;

            if (clockwise)
            {
                leadCommand.PO.RotationVelocity.Z = Core.RandomMinMax(2f, 3.5f);

                if (thisDroid.FirstWave)
                {
                    leadCommand.PO.Velocity.X = -2;
                }
            }
            else
            {
                leadCommand.PO.RotationVelocity.Z = Core.RandomMinMax(-3.5f, -2f);

                if (thisDroid.FirstWave)
                {
                    leadCommand.PO.Velocity.X = 2;
                }
            }
        }

        public void NewWave()
        {
            //Starts with five, adds two each wave. Eleven is the max droids.
            //One command ship at start. Two command ships after first wave. Second is from a drone a few seconds after.
            //The second command ship in the back turns into the Death ship after a time. It follows the droids.
            //Every five levels you get a bonus of 5000 pts.
            List<Vector3> spawnLocations = new List<Vector3>();
            float edgeX = Core.ScreenWidth / 1.6f - 0.95f;
            float inedgeY = Core.ScreenHeight / 4;
            float outedgeY = Core.ScreenHeight - 1.15f;

            int side = Core.RandomMinMax(1, 4);
            leadCommand.Y = -Core.ScreenHeight / 2.75f;

            leadCommand.BeginRun();

            for (int i = 0; i < count; i++)
            {
                spawnLocations.Add(new Vector3(0, Core.RandomMinMax(-outedgeY, -inedgeY), 0));


                if (side < 2)
                {
                    spawnLocations[i] = new Vector3(-(i * 3.05f) + edgeX, spawnLocations[i].Y, 0);
                    clockwise = false;
                    leadCommand.X = Core.ScreenWidth / 1.45f;
                    leadCommand.PO.Velocity.X = 2;
                    leadCommand.PO.RotationVelocity.Z = Core.RandomMinMax(-3.5f, -2f);
                    Main.instance.ThePlayer.Reset(false);
                }
                else
                {
                    spawnLocations[i] = new Vector3((i * 3.05f) + -edgeX, spawnLocations[i].Y, 0);
                    clockwise = true;
                    leadCommand.X = -Core.ScreenWidth / 1.45f;
                    leadCommand.PO.Velocity.X = -2;
                    leadCommand.PO.RotationVelocity.Z = Core.RandomMinMax(2f, 3.5f);
                    Main.instance.ThePlayer.Reset(true);
                }
            }

            leadCommand.DroidPath = MakePath(leadCommand.Position);
            leadCommand.Clockwise = clockwise;

            foreach (Vector3 location in spawnLocations)
            {
                bool firstWave = false;
                if (count == 5)
                {
                    firstWave = true;
                }

                SpawnDroid(location, clockwise, firstWave);
            }

            foreach(Shot shot in Main.instance.ThePlayer.Shots)
            {
                shot.Enabled = false;
            }
        }
        #endregion
        #region Private Methods
        void SpawnDroid(Vector3 position, bool clockwise, bool firstWave)
        {
            bool spawnNewDroid = true;
            int droid = droids.Count;

            for (int i = 0; i < droid; i++)
            {
                if (!droids[i].Enabled)
                {
                    spawnNewDroid = false;
                    droid = i;
                    break;
                }
            }

            if (spawnNewDroid)
            {
                droids.Add(new DroidShip(Game, cameraRef));
                droids[droid].InitializePoints(droidmodelFile, color, "Droid");
            }

            droids[droid].BeginRun();

            if (clockwise && !firstWave)
            {
                droids[droid].PO.Velocity.X = -1;
            }
            else if (!firstWave)
            {
                droids[droid].PO.Velocity.X = 1;
            }

            if (clockwise)
            {
                droids[droid].PO.RotationVelocity.Z = Core.RandomMinMax(1f, 2.5f);
            }
            else
            {
                droids[droid].PO.RotationVelocity.Z = Core.RandomMinMax(-2.5f, -1f);
            }
            droids[droid].DroidPath = MakePath(position);
            droids[droid].Position = position;
            droids[droid].Clockwise = clockwise;
            droids[droid].FirstWave = firstWave;
            droids[droid].UpdateMatrix();
        }

        Vector3[] MakePath(Vector3 position)
        {
            // 0=Top Left, 1=Top Right, 2=Bottom Right, 3=Bottom Left.
            Vector3[] path = new Vector3[4];

            float a = position.Y / 3f;
            float b = Core.ScreenWidth - 1.15f;
            float c = b - (a * -1);

            float pathX = c;
            float pathY = position.Y;

            path[0] = new Vector3(-pathX, pathY * -1, 0);
            path[1] = new Vector3(pathX, pathY * -1, 0);
            path[2] = new Vector3(pathX, pathY, 0);
            path[3] = new Vector3(-pathX, pathY, 0);

            return path;
        }
        #endregion
    }
}

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
        List<PhotonMine> photonMinesList;
        List<VaporMine> vaporMinesList;
        Vector3[] photonModelFile;
        Vector3[] vaporModelFile;
        VectorModel inside;
        Shot shot;
        Timer shotTimer;
        Timer photonMineTimer;
        Timer vaporMineTimer;
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
            photonMineTimer = new Timer(game);
            vaporMineTimer = new Timer(game);
            photonMinesList = new List<PhotonMine>();
            vaporMinesList = new List<VaporMine>();
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

            FileIO fileIO = new FileIO();
            photonModelFile = fileIO.ReadVectorModelFile("PhotonMine");
            vaporModelFile = fileIO.ReadVectorModelFile("VaporMine");
        }

        public new void BeginRun()
        {
            base.BeginRun();

            Enabled = true;
            shot.BeginRun();
            inside.UpdateMatrix();
            UpdateMatrix();
            command = true;
            Velocity = Vector3.Zero;
            photonMineTimer.Reset(8);
            vaporMineTimer.Reset(15);
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
        }
        #endregion
        #region Public Methods
        #endregion
        #region Protected Methods
        protected override void CheckCollision()
        {
            base.CheckCollision();

            if (shot.Enabled)
            {
                if (shot.CirclesIntersect(Main.instance.ThePlayer))
                {
                    shot.Enabled = false;
                    Main.instance.ThePlayer.Reset();
                    Main.instance.TheEnemies.ResetWave();
                }
            }
        }
        #endregion
        #region Private Methods
        void SpawnPhotonMine()
        {
            bool spawnNewMine = true;
            int mine = photonMinesList.Count;

            for(int i = 0; i < mine; i++)
            {
                if (!photonMinesList[i].Enabled)
                {
                    spawnNewMine = false;
                    mine = i;
                    break;
                }
            }

            if (spawnNewMine)
            {
                photonMinesList.Add(new PhotonMine(Game, TheCamera));
                photonMinesList.Last().InitializePoints(photonModelFile, color, "Photon");
                photonMinesList.First().BeginRun();
            }

            photonMinesList[mine].Spawn(Position);
        }

        void SpawnVaporMine()
        {
            bool spawnNewMine = true;
            int mine = vaporMinesList.Count;

            for (int i = 0; i < mine; i++)
            {
                if (!vaporMinesList[i].Enabled)
                {
                    spawnNewMine = false;
                    mine = i;
                    break;
                }
            }

            if (spawnNewMine)
            {
                vaporMinesList.Add(new VaporMine(Game, TheCamera));
                vaporMinesList.Last().InitializePoints(vaporModelFile, color, "Vapor");
                vaporMinesList.First().BeginRun();
            }

            vaporMinesList[mine].Spawn(Position);
        }

        void LeadGaurd()
        {
            if (shotTimer.Elapsed)
            {
                shotTimer.Reset();
                Fire(shot);
            }
        }

        void RearGaurd()
        {
            if (photonMineTimer.Elapsed)
            {
                photonMineTimer.Reset();
                SpawnPhotonMine();
            }

            if (vaporMineTimer.Elapsed)
            {
                vaporMineTimer.Reset();
                SpawnVaporMine();
            }
        }
        #endregion
    }
}
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
        DroidShip droid;
        CommandShip command;
        DeathShip death;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public EnemyController(Game game, Camera camera) : base(game)
        {
            cameraRef = camera;

            droid = new DroidShip(game, camera);
            command = new CommandShip(game, camera);
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

        }

        public void BeginRun()
        {
            droid.BeginRun();
            command.BeginRun();
            death.BeginRun();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum GameState
    {
        Running,
        GameOver
    }
    internal class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;

        public static void Start()
        {
            GameObjectManager.Start();
            SoundManager.InitSound();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            SoundManager.PlayStart();
        }

        public static void Update()
        {
            //FPS
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();
            if(gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }else if(gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        public static void GameOverUpdate()
        {
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver, x, y);
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }
    }
}

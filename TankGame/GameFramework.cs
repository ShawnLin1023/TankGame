using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    internal class GameFramework
    {
        public static Graphics g;

        public static void Start()
        {
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
        }

        public static void Update()
        {
            //FPS
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();
            GameObjectManager.Update();
        }

        
    }
}

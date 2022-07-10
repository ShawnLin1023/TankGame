using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame.Properties;

namespace TankGame
{
    internal class GameObjectManager
    {
        
        private static List<NotMovething> wallList = new List<NotMovething>();
        private static List<NotMovething> steelList = new List<NotMovething>();
        private static NotMovething boss;
        private static MyTank myTank;

        private static List<EnemyTank> tankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();

        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];

        public static void Start()
        {
            points[0].X = 0;
            points[0].Y = 0;
            points[1].X = 7 * 30;
            points[1].Y = 0;
            points[2].X = 14 * 30;
            points[2].Y = 0;
        }

        public static void Update()
        {
            foreach (NotMovething wall in wallList)
            {
                wall.Update();
            }
            foreach (NotMovething wall in steelList)
            {
                wall.Update();
            }
            foreach(EnemyTank tank in tankList)
            {
                tank.Update();
            }
            CheckAndDestroyBullet();
            for(int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            /*foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }*/
            boss.Update();
            myTank.Update();
            EnemyBorn();
        }

        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 5, dir, tag);
            bulletList.Add(bullet);
            
        }

        private static void CheckAndDestroyBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach(Bullet bullet in bulletList)
            {
                if(bullet.IsDestroy == true)
                {
                    needToDestroy.Add(bullet);
                }
            }
            foreach(Bullet bullet in needToDestroy)
            {
                bulletList.Remove(bullet);
            }
        }

        public static void DestroyWall(NotMovething wall)
        {
            wallList.Remove(wall);
        }

        public static void DestroyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }

        private static void EnemyBorn()
        {
            enemyBornCount++;
            if(enemyBornCount < enemyBornSpeed)
            {
                return;
            }
            Random rd = new Random();
            int index = rd.Next(0, 3);
            Point position = points[index];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y);
                    break;
            }
            enemyBornCount = 0;
        }

        private static void CreateEnemyTank1(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            tankList.Add(tank);
        }

        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            tankList.Add(tank);
        }

        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            foreach(NotMovething wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }

        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            foreach (NotMovething steelWall in steelList)
            {
                if (steelWall.GetRectangle().IntersectsWith(rt))
                {
                    return steelWall;
                }
            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach(EnemyTank tank in tankList)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }
            return null;
        }

        /*public static void DrawMap()
        {
            foreach(NotMovething wall in wallList)
            {
                wall.DrawSelf();
            }
            foreach (NotMovething wall in steelList)
            {
                wall.DrawSelf();
            }
            boss.DrawSelf();
        }

        public static void DrawMyTank()
        {
            myTank.DrawSelf();
        }*/

        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x, y, 2);
        }
        
        public static void CreateMap()
        {
            CreateWall(1, 1, 5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 4, 2, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(0, 7, 1, Resources.steel, steelList);
            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);
            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 9, 3, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);
            CreateWall(9, 9, 3, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);

            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);

            CreateBoss(7, 14, Resources.Boss);
        }

        private static void CreateBoss(int x, int y, Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMovething(xPosition, yPosition, img);
        }

        private static void CreateWall(int x, int y, int count, Image img, List<NotMovething> wallList)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                NotMovething wall1 = new NotMovething(xPosition, i, img);
                NotMovething wall2 = new NotMovething(xPosition + 15, i, img);
                wallList.Add(wall1);
                wallList.Add(wall2);
            }
        }

        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }

        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }
    }
}

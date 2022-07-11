using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame.Properties;

namespace TankGame
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    internal class Bullet : Movething
    {
        public Tag tag { get; set; }

        public bool IsDestroy { get; set; }
        public Bullet(int x, int y, int speed, Direction dir, Tag tag)
        {
            IsDestroy = false;
            this.Speed = speed;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.tag = tag;
            this.X = x - Width / 2;
            this.Y = y - Height / 2;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
        }

        private void MoveCheck()
        {
            #region 檢查有沒有超過視窗邊界
            if (Dir == Direction.Up)
            {
                if (Y + Height / 2 + 3 <= 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2 - 3 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width / 2 + 3 < 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width / 2 - 3 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            #endregion

            //檢查有沒有和其他物件發生碰撞

            Rectangle rect = GetRectangle();

            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Width = 3;
            rect.Height = 3;

            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;

            NotMovething wall = null;
            if ( (wall=GameObjectManager.IsCollidedWall(rect)) != null)
            {
                IsDestroy = true;
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundManager.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                IsDestroy = true;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                GameFramework.ChangeToGameOver();
                IsDestroy = true;
                SoundManager.PlayBlast();
                return;
            }
            if (tag == Tag.MyTank)
            {
                EnemyTank tank = null;
                if( (tank = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestroy = true;
                    GameObjectManager.DestroyTank(tank);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManager.PlayHit();
                    return;
                }
            }else if (tag == Tag.EnemyTank)
            {
                MyTank myTank = null;
                if( (myTank = GameObjectManager.IsCollidedMyTank(rect)) != null)
                {
                    IsDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    myTank.TakeDamage();
                    SoundManager.PlayBlast();
                    return;
                }
            }
        }

        private void Move()
        {

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
    }
}

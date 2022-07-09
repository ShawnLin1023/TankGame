using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankGame.Properties;

namespace TankGame
{
    internal class MyTank:Movething
    {
        public bool IsMoving { get; set; }
        public MyTank(int x, int y, int speed)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up;
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
            if(Dir == Direction.Up)
            {
                if(Y-Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }else if(Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoving = false;
                    return;
                }
            }else if(Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoving = false;
                    return;
                }
            }
            #endregion

            //檢查有沒有和其他物件發生碰撞

            Rectangle rect = GetRectangle();

            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            if(GameObjectManager.IsCollidedWall( rect ) != null)
            {
                IsMoving = false;
                return ;
            }
            if (GameObjectManager.IsCollidedSteel( rect ) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect))
            {
                IsMoving = false;
                return;
            }
        }

        private void Move()
        {
            if (IsMoving == false) return;

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

        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
            }
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }
    }
}

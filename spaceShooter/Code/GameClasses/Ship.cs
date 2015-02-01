using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Ship : ProtoGameObject {
        private Vector2f speedVector, thrustVector;
        private float orientation = 0, thrustLevel = 0, xGravity = .2f, yGravity = .2f;
        private Vector2f maximumSpeed = new Vector2f(35, 35);
        private List<Bullet> bulletList = new List<Bullet>();

        public Ship(Vector2f _startPosition, Vector2f _size, Texture _texture)
            : base(_startPosition, _size, _texture) {
            speedVector = new Vector2f();
            thrustVector = new Vector2f();
            Sprite.Origin = new Vector2f((this.Center.X * 1f) - _startPosition.X, this.Center.Y - _startPosition.Y);
        }

        public override void draw(){
            Controller.View.Center = Sprite.Position + 1 * Sprite.Origin;
            Controller.Window.SetView(Controller.View);
            Controller.Window.Draw(Sprite);
        }

        public override void update() {
            base.update();
            movement();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                fireBullet();
        }

        private void fireBullet() {
            bulletList.Add(new Bullet(Sprite.Position, orientation, 0, 0, Globals.bulletTextures[0]));
        }

        private void movement(){
            //basic idea: we have a certain inherent "gravity" slowing down the ship. if the engine thrust is increased,
            //the ship will be sped up in that direction. later on, it should be possible to "drift".
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                if (thrustLevel < 100)
                    thrustLevel += 1f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                if (thrustLevel > 1)
                    thrustLevel -= 2f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                thrustLevel = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                orientation -= 4;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                orientation += 4;

            if (orientation > 359)
                orientation -= 360;
            if (orientation < 0)
                orientation += 360;

            Console.WriteLine(Sprite.Position + "  " + Sprite.Origin);

            Sprite.Rotation = orientation;

            //slow down ship b/c gravity
            if (speedVector.X >= xGravity)
                speedVector.X -= xGravity;
            if (speedVector.X <= -xGravity)
                speedVector.X += xGravity;

            if (speedVector.Y >= yGravity)
                speedVector.Y -= yGravity;
            if (speedVector.Y <= -yGravity)
                speedVector.Y += yGravity;

            //calculate thrust angel
            thrustVector.Y = (float)Math.Sin(Math.PI * orientation / 180);
            thrustVector.X = (float)Math.Cos(Math.PI * orientation / 180);
            Vector2f multipliedThrust = (thrustVector * thrustLevel) / 2.5f;

            //add thrust to speed
            if (Math.Abs(speedVector.X + multipliedThrust.X) > maximumSpeed.X)
                speedVector.X = maximumSpeed.X * Math.Sign(multipliedThrust.X);
            else
                speedVector.X += multipliedThrust.X;

            if (Math.Abs(speedVector.Y + multipliedThrust.Y) > maximumSpeed.Y)
                speedVector.Y = maximumSpeed.Y * Math.Sign(multipliedThrust.Y);
            else
                speedVector.Y += multipliedThrust.Y;

            if (Math.Abs(speedVector.X) < 0.15f)
                speedVector.X = 0;
            if (Math.Abs(speedVector.Y) < 0.15f)
                speedVector.Y = 0;

            Sprite.Position += speedVector;
        }
    }
}

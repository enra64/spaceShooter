using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Ship : ProtoGameObject {
        private Vector2f thrustVector;
        private float xGravity = .3f, yGravity = .3f;
        public float Orientation { get; set; }
        public float InverseOrientation {
            get {
                if (Orientation >= 180)
                    return Orientation - 180;
                else
                    return Orientation + 180;
            }
        }
        public float Thrust { get; private set; }
        public Vector2f SpeedVector;
        private Vector2f maximumSpeed = new Vector2f(35, 35);
        public List<Bullet> bulletList = new List<Bullet>();
        private Stopwatch bulletWatch = new Stopwatch();

        public Ship(Vector2f _startPosition, Vector2f _size, Texture _texture)
            : base(_startPosition, _size, _texture) {
            SpeedVector = new Vector2f();
            thrustVector = new Vector2f();
            Sprite.Origin = new Vector2f((this.GlobalCenter.X * 1f) - _startPosition.X, this.GlobalCenter.Y - _startPosition.Y);
            bulletWatch.Start();
        }

        public override void draw(){
            Controller.View.Center = Sprite.Position + 1 * Sprite.Origin;
            Controller.Window.SetView(Controller.View);
            foreach (Bullet b in bulletList)
                b.draw();
            Controller.Window.Draw(Sprite);
        }

        public override void update() {
            base.update();
            movement();
            bulletControl();
        }

        private void bulletControl() {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) {
                if (bulletWatch.ElapsedMilliseconds > 200) {
                    Console.WriteLine("firing");
                    bulletList.Add(new Bullet(Sprite.Position, Orientation, 0, 0, Globals.bulletTextures[0]));
                    bulletWatch.Restart();
                }
            }
            
            foreach (Bullet b in bulletList)
                b.update();
        }

        private void movement(){
            //basic idea: we have a certain inherent "gravity" slowing down the ship. if the engine thrust is increased,
            //the ship will be sped up in that direction. later on, it should be possible to "drift".
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                if (Thrust < 100)
                    Thrust += 1f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                if (Thrust > 1)
                    Thrust -= 2f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                Thrust = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                Orientation -= 5;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                Orientation += 5;

            if (Orientation > 359)
                Orientation -= 360;
            if (Orientation < 0)
                Orientation += 360;

            Console.WriteLine(Sprite.Position + "  " + Sprite.Origin);

            Sprite.Rotation = Orientation;

            //slow down ship b/c gravity
            if (SpeedVector.X >= xGravity)
                SpeedVector.X -= xGravity;
            if (SpeedVector.X <= -xGravity)
                SpeedVector.X += xGravity;

            if (SpeedVector.Y >= yGravity)
                SpeedVector.Y -= yGravity;
            if (SpeedVector.Y <= -yGravity)
                SpeedVector.Y += yGravity;

            //calculate thrust angel
            thrustVector.Y = (float)Math.Sin(Math.PI * Orientation / 180);
            thrustVector.X = (float)Math.Cos(Math.PI * Orientation / 180);
            Vector2f multipliedThrust = (thrustVector * Thrust) / 4f;

            //add thrust to speed
            if (Math.Abs(SpeedVector.X + multipliedThrust.X) > maximumSpeed.X)
                SpeedVector.X = maximumSpeed.X * Math.Sign(multipliedThrust.X);
            else
                SpeedVector.X += multipliedThrust.X;

            if (Math.Abs(SpeedVector.Y + multipliedThrust.Y) > maximumSpeed.Y)
                SpeedVector.Y = maximumSpeed.Y * Math.Sign(multipliedThrust.Y);
            else
                SpeedVector.Y += multipliedThrust.Y;

            if (Math.Abs(SpeedVector.X) < 0.15f)
                SpeedVector.X = 0;
            if (Math.Abs(SpeedVector.Y) < 0.15f)
                SpeedVector.Y = 0;

            Sprite.Position += SpeedVector;
        }
    }
}

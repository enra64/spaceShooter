using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Ship : ProtoGameObject{
        private Vector2f speedVector, thrustVector;
        private float orientation = 0, thrustLevel = 0, xGravity = .8f, yGravity = .8f;

        public Ship(Vector2f _startPosition, Vector2f _size, Texture _texture)
            : base(_startPosition, _size, _texture) {
                speedVector = new Vector2f();
                thrustVector = new Vector2f();
                Sprite.Origin = this.Center;
        }

        public override void draw() {
            Controller.Window.Draw(Sprite);
        }

        public override void update() {
            base.update();
            //basic idea: we have a certain inherent "gravity" slowing down the ship. if the engine thrust is increased,
            //the ship will be sped up in that direction. later on, it should be possible to "drift".
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) {
                if (thrustLevel < 100)
                    thrustLevel+= 1;
                Console.WriteLine(thrustLevel);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl)) {
                if (thrustLevel > 0)
                    thrustLevel -= 1;
                Console.WriteLine(thrustLevel);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                thrustLevel = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    orientation -= 2;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    orientation += 2;

            if (orientation > 359)
                orientation -= 360;
            if (orientation < 0)
                orientation += 360;

            
            Sprite.Rotation = orientation;

            if (speedVector.X >= xGravity)
                speedVector.X -= xGravity;
            if (speedVector.Y >= yGravity)
                speedVector.Y -= yGravity;

            if (speedVector.Y < .5f)
                speedVector.Y = 0;
            if (speedVector.X < .5f)
                speedVector.X = 0;

            thrustVector.Y = (float)Math.Sin(Math.PI * orientation / 180);
            thrustVector.X = (float)Math.Cos(Math.PI * orientation / 180);
            speedVector = speedVector + thrustVector * thrustLevel / 10;

            Sprite.Position = Sprite.Position + speedVector;

            Console.WriteLine(thrustVector+", resulting movementVector: " + speedVector);
        }
    }
}

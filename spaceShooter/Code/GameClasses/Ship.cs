﻿using SFML.Graphics;
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

        public Ship(Vector2f _startPosition, Vector2f _size, Texture _texture)
            : base(_startPosition, _size, _texture) {
            speedVector = new Vector2f();
            thrustVector = new Vector2f();
            Sprite.Origin = new Vector2f(100, 100); //this.Center;
        }

        public override void draw() {
            Controller.View.Center = Center;
            Controller.Window.SetView(Controller.View);
            Controller.Window.Draw(Sprite);
        }

        public override void update() {
            base.update();
            //basic idea: we have a certain inherent "gravity" slowing down the ship. if the engine thrust is increased,
            //the ship will be sped up in that direction. later on, it should be possible to "drift".
            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                if (thrustLevel < 100)
                    thrustLevel += 1f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                if (thrustLevel > 0)
                    thrustLevel -= 1f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                thrustLevel = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                orientation -= 3;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                orientation += 3;

            if (orientation > 359)
                orientation -= 360;
            if (orientation < 0)
                orientation += 360;


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


            //speedVector += multipliedThrust;

            Console.Write(multipliedThrust + "  ");

            //limit ship speed to thrust
            //problem: speed in x is given >0; thrust is zero, gets increased, so speed > thrust -> speed = thrust
            //resolution: if thrust is just being increased, do not do this
            //sv: 20, tv: 0,01
            /*
            if (Math.Abs(speedVector.X) > Math.Abs(multipliedThrust.X) && multipliedThrust.X != 0)
                speedVector.X = multipliedThrust.X;
            if (Math.Abs(speedVector.Y) > Math.Abs(multipliedThrust.Y) && multipliedThrust.Y != 0)
                speedVector.Y = multipliedThrust.Y;
            */

            Console.WriteLine(speedVector);

            if (Math.Abs(speedVector.X) < 0.15f)
                speedVector.X = 0;
            if (Math.Abs(speedVector.Y) < 0.15f)
                speedVector.Y = 0;

            Sprite.Position += speedVector;
        }
    }
}

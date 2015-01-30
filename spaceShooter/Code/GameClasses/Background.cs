﻿using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private RenderTexture bgRenderTexture = new RenderTexture(6144, 6144);
        Game gameReference;

        Sprite[] bgSprites = new Sprite[9];
        FloatRect[] bgRects = new FloatRect[9];
        Boolean[] bgDraw = new Boolean[9];
        /// <summary>
        /// right top left bottom
        /// </summary>
        Boolean[] bgMoved = new Boolean[4];
        RectangleShape[] testShapes = new RectangleShape[9];

        public enum moveDirection {
            none,
            left,
            down,
            right,
            up
        };

        public Background(Game _ref) {
            List<Sprite> starList = new List<Sprite>();
            Random r = new Random();
            Vector2u textureSize = Globals.starTexture.Size;
            gameReference = _ref;

            for (int i = 0; i < 10000; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                float nextScale = (float)r.Next(10) / 15f;
                starList[i].Scale = new Vector2f(nextScale, nextScale);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(200));
                starList[i].Position = new Vector2f(r.Next((int)bgRenderTexture.Size.X), r.Next((int)bgRenderTexture.Size.Y));
            }
            bgRenderTexture.Clear();
            foreach (Sprite s in starList)
                bgRenderTexture.Draw(s);

            //create bg tiles
            for (int i = 0; i < bgSprites.Length; i++) {
                bgSprites[i] = new Sprite(bgRenderTexture.Texture);
                //set the sprite positions to a 3x3 grid
                int x = (i % 3) - 1;
                int y = (i / 3) - 1;
                bgSprites[i].Position = new Vector2f(x * bgRenderTexture.Size.X, y * bgRenderTexture.Size.Y);
                bgRects[i] = bgSprites[i].GetGlobalBounds();
                testShapes[i] = new RectangleShape(new Vector2f(bgRects[i].Width, bgRects[i].Height));
                testShapes[i].Position = new Vector2f(bgRects[i].Left, bgRects[i].Top);
                if (x == 0 ^ y == 0)
                    testShapes[i].FillColor = Color.White;
                else
                    testShapes[i].FillColor = Color.Green;
            }
        }

        public void update() {
            View calcView = Controller.Window.GetView();
            FloatRect overlap, calcPort = new FloatRect(calcView.Center.X - 1366 /*calcView.Size.X*/ / 2, calcView.Center.Y - 768 /*calcView.Size.Y*/ / 2, 1366, 768);

            //move the sprite if the view is moving off of it
            calcPort.Intersects(bgRects[4], out overlap);

            if (overlap.Height == calcPort.Height) {
                bgMoved[1] = false;
                bgMoved[3] = false;
            }
            if (overlap.Width == calcPort.Width) {
                bgMoved[0] = false;
                bgMoved[2] = false;
            }

            if (overlap.Left <= bgRects[4].Left && !bgMoved[0])
                moveBackground(moveDirection.left);

            if (overlap.Top <= bgRects[4].Top && !bgMoved[3])
                moveBackground(moveDirection.up);


            if (overlap.Left + calcPort.Width > bgRects[4].Left + bgRects[4].Width && !bgMoved[2])
                moveBackground(moveDirection.right);

            if (overlap.Top + calcPort.Height > bgRects[4].Top + bgRects[4].Height && !bgMoved[1])
                moveBackground(moveDirection.down);

            bool doRepos = true;
            //draw only if it would actually be seen
            for (int i = 0; i < bgRects.Length; i++) {
                if (calcPort.Intersects(bgRects[i])){
                    bgDraw[i] = true;
                    doRepos = false;
                }
                else
                    bgDraw[i] = false;
            }
            if (doRepos)
                reposition(gameReference.myShip.Center);
        }

        public void moveBackground(moveDirection whichDirection) {
            //recalculate bgrects, move all in one direction
            Vector2f change = new Vector2f();
            switch (whichDirection) {
                case moveDirection.left:
                    change.X -= bgRenderTexture.Size.X;
                    bgMoved[2] = true;
                    Console.WriteLine("left");
                    break;
                case moveDirection.right:
                    change.X += bgRenderTexture.Size.X;
                    bgMoved[0] = true;
                    Console.WriteLine("right");
                    break;
                case moveDirection.up:
                    change.Y -= bgRenderTexture.Size.Y;
                    bgMoved[1] = true;
                    Console.WriteLine("top");
                    break;
                case moveDirection.down:
                    change.Y += bgRenderTexture.Size.Y;
                    bgMoved[3] = true;
                    Console.WriteLine("bottom");
                    break;
            }
            //move sprites
            for (int i = 0; i < bgSprites.Length; i++) {
                bgSprites[i].Position += change;
                bgRects[i] = bgSprites[i].GetGlobalBounds();
                testShapes[i].Position += change;
            }
        }

        private void reposition(Vector2f ship) {
            Console.WriteLine("repositioning");
            for (int i = 0; i < bgSprites.Length; i++) {
                //set the sprite positions to a 3x3 grid
                int x = (i % 3) - 1;
                int y = (i / 3) - 1;
                bgSprites[i].Position = new Vector2f(ship.X - (bgRenderTexture.Size.X / 2) + x * bgRenderTexture.Size.X,
                    ship.Y - (bgRenderTexture.Size.Y / 2) + y * bgRenderTexture.Size.Y);
                bgRects[i] = bgSprites[i].GetGlobalBounds();
            }
        }

        internal void draw() {
            for (int i = 0; i < bgRects.Length; i++)
                if(bgDraw[i])
                    Controller.Window.Draw(bgSprites[i]);
        }
    }
}

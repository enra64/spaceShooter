using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private RenderTexture bgRenderTexture = new RenderTexture(4096, 4096);

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

        public Background() {
            List<Sprite> starList = new List<Sprite>();
            Random r = new Random();
            Vector2u textureSize = Globals.starTexture.Size;

            for (int i = 0; i < 10000; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                float nextScale = (float)r.Next(10) / 15f;
                starList[i].Scale = new Vector2f(nextScale, nextScale);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
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
                if (x != 0 && y != 0)
                    testShapes[i].FillColor = Color.Green;
                if (x == 0 ^ y == 0)
                    testShapes[i].FillColor = Color.White;
                if (x == 0 && y == 0)
                    testShapes[i].FillColor = Color.Green;
            }
        }

        public void update() {
            View calcView = Controller.Window.GetView();
            FloatRect calcPort = new FloatRect(calcView.Center.X - calcView.Size.X / 2, calcView.Center.Y - calcView.Size.Y / 2, calcView.Size.X, calcView.Size.Y);
            FloatRect overlap;
            //move the sprite if the view is moving off of it
            calcPort.Intersects(bgRects[4], out overlap);
            
            //Console.WriteLine(overlap);

            //totally legit
            if (overlap.Left <= 0) {
                //if we just jumped right, do not move
                if (bgMoved[0] == false && bgMoved[0] == false)
                    moveBackground(moveDirection.left);
                //except if full intersection with main tile is reached
                else if (overlap.Width == Controller.Window.Size.X){
                    moveBackground(moveDirection.left);
                    bgMoved[0] = false;
                }
            }
            calcPort.Intersects(bgRects[4], out overlap);
            if (overlap.Left >= bgRenderTexture.Size.X - Controller.Window.Size.X) {
                if (bgMoved[2] == false && bgMoved[2] == false)
                    moveBackground(moveDirection.right);
                else if (overlap.Width == Controller.Window.Size.X) {
                    moveBackground(moveDirection.right);
                    bgMoved[2] = false;
                }
            }

            calcPort.Intersects(bgRects[4], out overlap);
            if (overlap.Top <= 0) {
                if (bgMoved[3] == false && bgMoved[1] == false)
                    moveBackground(moveDirection.up);
                else if (overlap.Width == Controller.Window.Size.Y) {
                    moveBackground(moveDirection.up);
                    bgMoved[3] = false;
                }
            }
            calcPort.Intersects(bgRects[4], out overlap);
            if (overlap.Top >= bgRenderTexture.Size.Y - Controller.Window.Size.Y) {
                if (bgMoved[1] == false && bgMoved[3] == false)
                    moveBackground(moveDirection.down);
                else if (overlap.Width == Controller.Window.Size.Y) {
                    moveBackground(moveDirection.down);
                    bgMoved[1] = false;
                }
            }
            calcPort.Intersects(bgRects[4], out overlap);

            //draw only if it would actually be seen
            for (int i = 0; i < bgRects.Length; i++) {
                if (calcPort.Intersects(bgRects[i]))
                    bgDraw[i] = true;
                else
                    bgDraw[i] = false;
            }
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

        internal void draw() {
            for (int i = 0; i < bgRects.Length; i++)
                //if(bgDraw[i])
                Controller.Window.Draw(testShapes[i]);
        }
    }
}

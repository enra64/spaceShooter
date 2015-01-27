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
        private Sprite bgSprite;

        Sprite[] bgSprites = new Sprite[9];
        FloatRect[] bgRects = new FloatRect[9];
        Boolean[] bgDraw = new Boolean[9];
        RectangleShape[] testShapes = new RectangleShape[9];
        Vector2u textureSize;

        private enum moveDirection {
            none,
            topleft,
            left,
            bottomleft,
            bottom,
            bottomright,
            right,
            topright,
            top
        };

        public Background() {
            List<Sprite> starList = new List<Sprite>();
            Random r = new Random();
            textureSize = Globals.starTexture.Size;

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
            
            Console.WriteLine(overlap);

            //totally legit
            if (overlap.Left <= 0)
                moveBackground(moveDirection.left);
            if (overlap.Left >= bgRenderTexture.Size.X - 100)
                moveBackground(moveDirection.right);

            if (overlap.Top <= 0 && overlap.Height < Controller.Window.Size.Y)
                moveBackground(moveDirection.top);
            if (overlap.Top >= bgRenderTexture.Size.X - 100 && overlap.Height < Controller.Window.Size.Y)
                moveBackground(moveDirection.bottom);
            

            //draw only if it would actually be seen
            for (int i = 0; i < bgRects.Length; i++) {
                if (calcPort.Intersects(bgRects[i]))
                    bgDraw[i] = true;
                else
                    bgDraw[i] = false;
            }
        }

        private void moveBackground(moveDirection whichDirection) {
            //recalculate bgrects, move all in one direction
            Vector2f change = new Vector2f();
            switch (whichDirection) {
                case moveDirection.left:
                    change.X -= textureSize.X;
                    break;
                case moveDirection.right:
                    change.X += textureSize.X;
                    break;
                case moveDirection.top:
                    change.Y -= textureSize.Y;
                    break;
                case moveDirection.bottom:
                    change.Y -= textureSize.Y;
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

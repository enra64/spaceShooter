using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private RenderTexture bgRenderTexture = new RenderTexture(8192, 8192);
        private Sprite bgSprite;

        Sprite[] bgSprites = new Sprite[9];
        FloatRect[] bgRects = new FloatRect[9];
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
            Vector2u textureSize = Globals.starTexture.Size;

            for (int i = 0; i < 10000; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                float nextScale = (float)r.Next(10) / 15f;
                starList[i].Scale = new Vector2f(nextScale, nextScale);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                starList[i].Position = new Vector2f(r.Next(8000), r.Next(8000));
            }
            bgRenderTexture.Clear();
            foreach (Sprite s in starList)
                bgRenderTexture.Draw(s);
            for (uint i = 0; i < bgSprites.Length; i++) {
                bgSprites[i] = new Sprite(bgRenderTexture.Texture);
                //set the sprite positions to a 3x3 grid
                bgSprites[i].Position = new Vector2f((float) (i * textureSize.X - textureSize.X), (float) (i%3 * textureSize.Y - textureSize.Y));
                bgRects[i] = bgSprites[i].GetGlobalBounds();
            }
        }

        public void update(){
            View calcView = Controller.Window.GetView();
            FloatRect calcPort= new FloatRect(calcView.Center.X - calcView.Size.X / 2, calcView.Center.Y - calcView.Size.Y / 2, calcView.Size.X, calcView.Size.Y);
            FloatRect overlap;
            //move the sprite if the view is moving off of it
            bgRects[4].Intersects(calcPort, out overlap);

            for (int i = 0; i < bgRects.Length; i++) {
                if (bgRects[i].Intersects(overlap)) {
                    /*
                     0 1 2    0 1 2    0 0 0
                     3 4 5  %:0 1 2  /:1 1 1
                     6 7 8    0 1 2    2 2 2
                     */
                    switch (i % 3){
                        case 0:
                            moveBackground(moveDirection.left);
                            break;
                        case 2:
                            moveBackground(moveDirection.right);
                            break;
                    }
                    switch (i / 3){
                        case 0:
                            moveBackground(moveDirection.top);
                            break;
                        case 2:
                            moveBackground(moveDirection.bottom);
                            break;
                    }
                }
            }
            
        }

        private void moveBackground(moveDirection whichDirection) {
            //recalculate bgrects, move all in one direction
            foreach()
        }

        //idea: draw onto a rendertexture, draw that behind everything
        //if we are moving right, draw new stars on the left and delete the old ones
        internal void draw() {
            Controller.Window.Draw(bgSprites[4]);
        }
    }
}

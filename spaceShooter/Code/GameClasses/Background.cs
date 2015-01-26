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
            bgSprite = new Sprite(bgRenderTexture.Texture);
            bgSprite.Position = new Vector2f(2, 3);
        }

        public void update(){
            View calcView = Controller.Window.GetView();
            calcView.Viewport = new FloatRect(calcView.Center.X - calcView.Size.X / 2, calcView.Center.Y - calcView.Size.Y / 2, calcView.Size.X, calcView.Size.Y);
            //move the sprite if the view is moving off of it
            if (calcView.Viewport.Left + calcView.Viewport.Width > bgSprite.Position.X + bgRenderTexture.Texture.Size.X)
                bgSprite.Position = new Vector2f(bgSprite.Position.X + bgRenderTexture.Texture.Size.X - calcView.Viewport.Width, bgSprite.Position.Y);
            if (calcView.Viewport.Top + calcView.Viewport.Height > bgSprite.Position.Y + bgRenderTexture.Texture.Size.Y)
                bgSprite.Position = new Vector2f(bgSprite.Position.X, bgSprite.Position.Y + bgRenderTexture.Texture.Size.Y - calcView.Viewport.Height);

            if (calcView.Viewport.Left < bgSprite.Position.X)
                bgSprite.Position = new Vector2f(calcView.Viewport.Left - bgRenderTexture.Texture.Size.X, bgSprite.Position.Y);
            if (calcView.Viewport.Top + calcView.Viewport.Height > bgSprite.Position.Y + bgRenderTexture.Texture.Size.Y)
                bgSprite.Position = new Vector2f(bgSprite.Position.X, calcView.Viewport.Top - bgRenderTexture.Texture.Size.Y);
        }

        //idea: draw onto a rendertexture, draw that behind everything
        //if we are moving right, draw new stars on the left and delete the old ones
        internal void draw() {
            Controller.Window.Draw(bgSprite);
        }
    }
}

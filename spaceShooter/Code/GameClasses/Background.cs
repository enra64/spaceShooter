using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private List<Sprite> starList = new List<Sprite>();
        private RenderTexture background;
        private Random r = new Random();
        private Sprite bgSprite;

        public Background() {
            background = new RenderTexture(Controller.Window.Size.X, Controller.Window.Size.Y);
            for (int i = 0; i < 100; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                starList[i].Scale = new Vector2f(r.Next(0, 10) / 5, r.Next(0, 10) / 5);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                starList[i].Position = new Vector2f(r.Next((int)Controller.Window.Size.X), r.Next((int)Controller.Window.Size.Y));
            }
        }

        public void update(){
            background.Clear();
            foreach (Sprite s in starList)
                background.Draw(s);
            background.Display();
            bgSprite = new Sprite(background.Texture);
            //bgSprite.Position = Controller.View.Center;
        }

        //idea: draw onto a rendertexture, draw that behind everything
        //if we are moving right, draw new stars on the left and delete the old ones
        public void draw(){
            if(bgSprite != null)
                Controller.Window.Draw(bgSprite);
        }
    }
}

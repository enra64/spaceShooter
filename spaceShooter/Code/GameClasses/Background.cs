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
        private Random r = new Random();
        private Vector2u textureSize;



        public Background() {
            textureSize = Globals.starTexture.Size;
            for (int i = 0; i < 10000; i++) {
                starList.Add(new Sprite(Globals.starTexture));
                float nextScale = (float)r.Next(10) / 15f;
                starList[i].Scale = new Vector2f(nextScale, nextScale);
                starList[i].Color = new Color((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                starList[i].Position = new Vector2f(r.Next(30000), r.Next(20000));
            }
        }

        public void update(){
        }

        //idea: draw onto a rendertexture, draw that behind everything
        //if we are moving right, draw new stars on the left and delete the old ones
        public void draw(){
            foreach(Sprite s in starList)
                Controller.Window.Draw(s);
        }
    }
}

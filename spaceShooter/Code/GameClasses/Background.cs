using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Background {
        private List<Sprite> starList = new List<Sprite>();
        private RenderTexture background;

        public Background() {
            background = new RenderTexture(Controller.Window.Size.X, Controller.Window.Size.Y);
        }

        public void update(){

        }

        //idea: draw onto a rendertexture, draw that behind everything
        public void draw(){
        }
    }
}

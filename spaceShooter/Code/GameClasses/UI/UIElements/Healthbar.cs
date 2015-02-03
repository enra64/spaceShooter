using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses.UI {
    class Healthbar : ProtoUIObject{
        private RectangleShape r;

        public Healthbar(Vector2f _size, Vector2f _pos, Game _ref, String _tag)
            : base(_size, _pos, _ref, _tag) {
            r = new RectangleShape(_size);
            r.Position = _pos;
            r.FillColor = Color.Red;
        }


        public override void draw(View _uiView) {
            Controller.Window.SetView(_uiView);
            Controller.Window.Draw(r);
        }

        public override void update() {
            //update position if necessary
            r.Position = Position;

            //maximum size
            float xNew = Size.X;
            if (xNew > 30)
                xNew = 30;
            r.Size = new Vector2f(xNew, (reference.myShip.Health / 100f) * Size.Y);
        }
    }
}

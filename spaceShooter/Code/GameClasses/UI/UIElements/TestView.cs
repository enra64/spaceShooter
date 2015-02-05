using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses.UI {
    class TestView : ProtoUIObject{
        private RectangleShape r;

        public TestView(Vector2f _size, Vector2f _pos, Game _ref, String _tag) : base(_size, _pos, _ref, _tag){
            r = new RectangleShape(_size);
            r.Position = _pos;
            r.FillColor = Color.Green;
        }


        public override void draw() {
            Controller.Window.Draw(r);
        }

        public override void update() {
            r.Position = Position;
            r.Size = Size;
        }
    }
}

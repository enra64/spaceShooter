using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses.UI {
    class Container : ProtoUIObject{
        private List<ProtoUIObject> containList = new List<ProtoUIObject>();
        private bool vertical;

        public Container(Vector2f _size, Vector2f _pos, Game _ref, String _tag, bool stacksVertically)
            : base(_size, _pos, _ref, _tag) {
                vertical = stacksVertically;
        }

        public void Add(ProtoUIObject p) {
            //arrange views on top of each other
            if (vertical) {
                p.Size = new Vector2f(Size.X, Size.Y / (containList.Count + 1));
                p.Position = new Vector2f(0, p.Size.Y * containList.Count);
            }
            //arrange views next to each other
            else {
                p.Size = new Vector2f(Size.X / (containList.Count + 1), Size.Y);
                p.Position = new Vector2f(p.Size.X * containList.Count, 0);
            }
            containList.Add(p);
        }

        public ProtoUIObject Get(int itemID) {
            return containList[itemID];
        }

        public ProtoUIObject Get(String itemTag) {
            return containList.Find(x => x.Tag == itemTag);
        }

        public override void draw(View _uiView) {
            foreach (ProtoUIObject p in containList)
                if (p != null)
                    p.draw(_uiView);
        }

        public override void update() {
            foreach (ProtoUIObject p in containList)
                if (p != null)
                    p.update();
        }
    }
}

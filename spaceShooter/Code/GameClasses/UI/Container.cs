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
            Vector2f _size, _positionDelta;
            containList.Add(p);
            //arrange views on top of each other
            if (vertical) {
                _size = new Vector2f(Size.X, Size.Y / (containList.Count));
                _positionDelta = new Vector2f(0, _size.Y);
            }
            //arrange views next to each other
            else {
                _size = new Vector2f(Size.X / (containList.Count), Size.Y);
                _positionDelta = new Vector2f(_size.X, 0);
            }
            //renew all positions
            for (int i = 0; i < containList.Count; i++) {
                containList[i].Position = _positionDelta * i;
                containList[i].Size = _size;
            }
        }

        public ProtoUIObject Get(int itemID) {
            return containList[itemID];
        }

        public ProtoUIObject Get(String itemTag) {
            return containList.Find(x => x.Tag == itemTag);
        }

        /// <summary>
        /// Draws everything in this container; does _not_ handle views
        /// </summary>
        public override void draw() {
            foreach (ProtoUIObject p in containList)
                if (p != null)
                    p.draw();
        }

        public override void update() {
            foreach (ProtoUIObject p in containList)
                if (p != null)
                    p.update();
        }
    }
}

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
        public String Tag { get; set; }
        public Container(Vector2f _size, Game _ref, String _tag)
            : base(_size, _ref, _tag) {
        }

        public void Add(ProtoUIObject p) {
            containList.Add(p);
        }

        public ProtoUIObject Get(int itemID) {
            return containList[itemID];
        }

        public ProtoUIObject Get(String itemTag) {
            return containList.Find(x => x.Tag == itemTag);
        }

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

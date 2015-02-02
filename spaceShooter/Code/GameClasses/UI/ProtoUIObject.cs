using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses.UI {
    abstract class ProtoUIObject {
        public Vector2f Size { get; set; }
        public String Tag { get; set; }

        public ProtoUIObject(Vector2f _size, Game _ref, String _tag) {
            Tag = _tag;
        }

        public abstract void draw();
        public abstract void update();
    }
}

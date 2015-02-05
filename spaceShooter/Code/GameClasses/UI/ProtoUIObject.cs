using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.Gamestates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses.UI {
    abstract class ProtoUIObject {
        public Vector2f Size, Position;
        public String Tag { get; set; }
        internal Game reference;

        public ProtoUIObject(Vector2f _size, Vector2f _pos, Game _ref, String _tag) {
            Tag = _tag;
            Size = _size;
            Position = _pos;
            reference = _ref;
        }

        public abstract void draw();
        public abstract void update();
    }
}

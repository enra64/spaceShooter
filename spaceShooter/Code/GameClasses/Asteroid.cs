using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Asteroid : ProtoGameObject{
        public int Health { get; set; }
        private Vector2f direction;
        private float rotation;

        public Asteroid(Vector2f _pos, Vector2f _dir, float _rotation, Texture _tex)
            : base(_pos, _tex.Size, _tex) {
                direction = _dir;
                rotation = _rotation;
                Sprite.Origin = LocalCenter;
        }

        public override void update() {
            base.update();
            Sprite.Position += direction;
            Sprite.Rotation += rotation;
        }
    }
}

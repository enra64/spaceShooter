using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.GameClasses {
    class Asteroid : ProtoGameObject{
        public Vector2f Direction;
        public int ID { get; private set; }
        public Boolean CollisionHandled { get; set; }
        private float rotation;

        public Asteroid(Vector2f _pos, Vector2f _dir, float _rotation, Texture _tex, int _id)
            : base(_pos, _tex.Size, _tex) {
                Direction = _dir;
                rotation = _rotation;
                Sprite.Origin = LocalCenter;
                Health = 100;
                ID = _id;
                CollisionHandled = false;
        }

        public override void update() {
            base.update();
            Sprite.Position += Direction;
            if (Direction.X > 2)
                Direction.X -= .4f;
            if (Direction.Y > 2)
                Direction.Y -= .4f;
            Sprite.Rotation += rotation;
        }
    }
}
